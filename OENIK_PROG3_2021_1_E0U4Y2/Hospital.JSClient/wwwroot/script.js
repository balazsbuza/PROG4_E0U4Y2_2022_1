let Patients = [];
let connection = null;

let patientIdToUpdate = -1;

let noncrud_1 = [];
let noncrud_2 = [];
let noncrud_3 = [];

getdata();
getnodata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:43747/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PatientCreated", (user, message) => {
        getdata();
        getnondata_2();
    });

    connection.on("PatientDeleted", (user, message) => {
        getdata();
        getnondata_2();
    });

    connection.on("PatientUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:43747/Patient')
        .then(x => x.json())
        .then(y => {
            Patients = y;
            //console.log(Patients);
            display();
        });
}

function getnodata() {
    getnondata_1();
    getnondata_2();
    getnondata_3();
}

async function getnondata_1() {
    await fetch('http://localhost:43747/Stat/DoctorWorkAddress')
        .then(x => x.json())
        .then(y => {
            noncrud_1 = y;
            noncrud_1_d();
        });
}

async function getnondata_2() {
    await fetch('http://localhost:43747/Stat/ClinicGender')
        .then(x => x.json())
        .then(y => {
            noncrud_2 = y;
            noncrud_2_d();
        });
}

async function getnondata_3() {
    await fetch('http://localhost:43747/Stat/DoctorOfficeHours')
        .then(x => x.json())
        .then(y => {
            noncrud_3 = y;
            noncrud_3_d();
        });
}

function display() {
    document.getElementById('results').innerHTML = "";
    Patients.forEach(t => {
        document.getElementById('results').innerHTML +=
            "<tr><td>" + t.patientId + "</td><td>" +
         t.name + "</td><td>" +
         t.disease + "</td><td>" +
        `<button type="button" onclick="remove(${t.patientId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.patientId})">Update</button>`
            + "</td></tr>";
    });
}

function noncrud_1_d() {
    document.getElementById('results_1').innerHTML = "";
    noncrud_1.forEach(t => {
        document.getElementById('results_1').innerHTML += "<tr><td>"
        + t.numberOfDoctors + "</td><td>"
        + t.clinicAddress + "</td>";
    });
}

function noncrud_2_d() {
    document.getElementById('results_2').innerHTML = "";
    noncrud_2.forEach(t => {
        document.getElementById('results_2').innerHTML += "<tr><td>"
            + t.clinicName + "</td><td>"
            + t.numberofM + "</td><td>"
            + t.numberofF + "</td><td>"
            + t.numberofO + "</td>";
    });
}

function noncrud_3_d() {
    document.getElementById('results_3').innerHTML = "";
    noncrud_3.forEach(t => {
        document.getElementById('results_3').innerHTML += "<tr><td>"
            + t.numberOfDoctors + "</td><td>"
            + t.officeHours + "</td>";
    });
}

function remove(id) {
    fetch('http://localhost:43747/Patient/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function showupdate(id) {
    document.getElementById('patientdiseasetoupdate').value = Patients.find(t => t['patientId'] == id)['disease']
    document.getElementById('updateformdiv').style.display = 'flex';
    patientIdToUpdate = id;
}

function create() {
    let name = document.getElementById('patientname').value;
    let gender = document.getElementById('patientgender').value;
    let doctorid = document.getElementById('doctorid').value;
    fetch('http://localhost:43747/Patient', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, doctorId: doctorid, gender: gender})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';

    let patientIdtoup = patientIdToUpdate;
    let nametoup = Patients.find(t => t['patientId'] == patientIdToUpdate)['name'];
    let gendertoup = Patients.find(t => t['patientId'] == patientIdToUpdate)['gender'];
    let datofbirthtoup = Patients.find(t => t['patientId'] == patientIdToUpdate)['datofbirth'];
    let nameofmothertoup = Patients.find(t => t['patientId'] == patientIdToUpdate)['nameofmother'];
    //let doctortoup = Patients.find(t => t['patientId'] == patientIdToUpdate)['doctor'];
    let diseasetoup = document.getElementById('patientdiseasetoupdate').value;

    fetch('http://localhost:43747/Patient', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { patientId: patientIdtoup, name: nametoup, gender: gendertoup, datofbirth: datofbirthtoup, nameofmother: nameofmothertoup, disease: diseasetoup }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}