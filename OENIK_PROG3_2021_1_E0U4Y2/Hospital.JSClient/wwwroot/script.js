let Clinics = [];
let connection = null;

let clinicIdToUpdate = -1;

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

    connection.on("ClinicCreated", (user, message) => {
        getdata();
    });

    connection.on("ClinicDeleted", (user, message) => {
        getdata();
    });

    connection.on("ClinicUpdated", (user, message) => {
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
    await fetch('http://localhost:43747/Clinic')
        .then(x => x.json())
        .then(y => {
            Clinics = y;
            //console.log(Clinics);
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
    Clinics.forEach(t => {
        document.getElementById('results').innerHTML +=
            "<tr><td>" + t.clinicId + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="remove(${t.clinicId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.clinicId})">Update</button>`
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
    fetch('http://localhost:43747/Clinic/' + id, {
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
    document.getElementById('clinicaddresstoupdate').value = Clinics.find(t => t['clinicId'] == id)['address']
    document.getElementById('updateformdiv').style.display = 'flex';
    clinicIdToUpdate = id;
}

function create() {
    let name = document.getElementById('clinicname').value;
    fetch('http://localhost:43747/Clinic', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name })
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

    let clinicIdtoup = clinicIdToUpdate;
    let nametoup = Clinics.find(t => t['clinicId'] == clinicIdToUpdate)['name'];
    let companytoup = Clinics.find(t => t['clinicId'] == clinicIdToUpdate)['company'];
    let officehourstoup = Clinics.find(t => t['clinicId'] == clinicIdToUpdate)['officehours'];
    let phonetoup = Clinics.find(t => t['clinicId'] == clinicIdToUpdate)['phone'];
    let addresstoup = document.getElementById('clinicaddresstoupdate').value;

    fetch('http://localhost:43747/Clinic', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { clinicId: clinicIdtoup, name: nametoup, company: companytoup, officehours: officehourstoup, phone: phonetoup, address: addresstoup }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}