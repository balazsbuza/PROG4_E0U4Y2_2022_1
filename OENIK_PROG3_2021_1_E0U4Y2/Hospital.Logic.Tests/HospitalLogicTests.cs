// <copyright file="HospitalLogicTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Hospital.Data.Tables;
    using Hospital.Repository.Repositories;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Hospital logic.
    /// </summary>
    [TestFixture]
    public class HospitalLogicTests
    {
        /// <summary>
        /// Tests for clinics and number of patients by gender.
        /// </summary>
        [Test]
        public void TestGetClinicGender()
        {
            Mock<IClinicRepository> mockedCRepo = new Mock<IClinicRepository>(
               MockBehavior.Loose);

            Mock<IDoctorRepository> mockedDRepo = new Mock<IDoctorRepository>(
               MockBehavior.Loose);

            Mock<IPatientRepository> mockedPRepo = new Mock<IPatientRepository>(
                    MockBehavior.Loose);

            List<ClinicGender> genders = new List<ClinicGender>()
            {
                new ClinicGender() { ClinicName = "Clinicfit", NumberofM = 2, NumberofF = 1, NumberofO = 1 },
                new ClinicGender() { ClinicName = "Neoclinic", NumberofM = 0, NumberofF = 1, NumberofO = 1 },
                new ClinicGender() { ClinicName = "Wellnest", NumberofM = 1, NumberofF = 1, NumberofO = 0 },
            };

            List<ClinicGender> expectedGenders = new List<ClinicGender>() { genders[0], genders[1], genders[2] };

            List<Clinic> clinics = new List<Clinic>()
            {
                new Clinic() { ClinicId = 1, Address = "First street", Company = "Wellnest comp", Name = "Wellnest", Officehours = "8:00-16:00", Phone = 1112223 },
                new Clinic() { ClinicId = 2, Address = "Second avenue", Company = "Neoclinic comp", Name = "Neoclinic", Officehours = "7:00-16:00", Phone = 2223334 },
                new Clinic() { ClinicId = 3, Address = "Third road", Company = "Clinicfit comp", Name = "Clinicfit", Officehours = "8:00-15:00", Phone = 3334445 },
            };

            List<Doctor> doctors = new List<Doctor>()
            {
                new Doctor() { DoctorId = 1, Name = "Gedeon Umar", ClinicId = 1, Dateofbirth = new DateTime(1980, 1, 1), Degree = "Master of Medicine", Sealnumber = 55321, Specialization = "Cardiology" },
                new Doctor() { DoctorId = 2, Name = "Eliza Remigio", ClinicId = 2, Dateofbirth = new DateTime(1981, 2, 2), Degree = "Master of Surgery", Sealnumber = 44321, Specialization = "Family Medicine" },
                new Doctor() { DoctorId = 3, Name = "Larisa Branislav", ClinicId = 3, Dateofbirth = new DateTime(1982, 3, 3), Degree = "Doctor of Medicine", Sealnumber = 33321, Specialization = "Hematology" },
                new Doctor() { DoctorId = 4, Name = "Vavrinec Daireann", ClinicId = 3, Dateofbirth = new DateTime(1983, 4, 4), Degree = "Doctor of Osteopathic Medicine", Sealnumber = 22321, Specialization = "Medical genetics" },
                new Doctor() { DoctorId = 5, Name = "Rhagouel Ríoghnach", ClinicId = 2, Dateofbirth = new DateTime(1984, 5, 5), Degree = "Bachelor of Medicine", Sealnumber = 11321, Specialization = "Neurology" },
            };

            List<Patient> patients = new List<Patient>()
            {
                new Patient() { PatientId = 1, Name = "Brian Steele", Dateofbirth = new DateTime(1980, 1, 1), DoctorId = 1, Gender = "M", Nameofmother = "Stella Flowers", Disease = "Chikungunya" },
                new Patient() { PatientId = 2, Name = "Ricky Hunter", Dateofbirth = new DateTime(1981, 2, 2), DoctorId = 2, Gender = "F", Nameofmother = "Bonnie Doyle", Disease = "Keratitis" },
                new Patient() { PatientId = 3, Name = "Kathryn Walker", Dateofbirth = new DateTime(1982, 3, 3), DoctorId = 3, Gender = "O", Nameofmother = "Sharon Hall", Disease = "Campylobacteriosis" },
                new Patient() { PatientId = 4, Name = "Kurt Jones", Dateofbirth = new DateTime(1983, 4, 4), DoctorId = 4, Gender = "M", Nameofmother = "Beth Harper", Disease = "Alopecia areata" },
                new Patient() { PatientId = 5, Name = "Katherine Morgan", Dateofbirth = new DateTime(1984, 5, 5), DoctorId = 1, Gender = "F", Nameofmother = "Iris Powers", Disease = "Rheumatism" },
                new Patient() { PatientId = 6, Name = "Emanuel Jennings", Dateofbirth = new DateTime(1985, 6, 6), DoctorId = 2, Gender = "O", Nameofmother = "Louise Terry", Disease = "Lymphoma" },
                new Patient() { PatientId = 7, Name = "Leeroy Jenkins", Dateofbirth = new DateTime(1986, 7, 7), DoctorId = 3, Gender = "M", Nameofmother = "Tracy Dennis", Disease = "Schistosomiasis" },
                new Patient() { PatientId = 8, Name = "Clark Wise", Dateofbirth = new DateTime(1987, 8, 8), DoctorId = 4, Gender = "F", Nameofmother = "Casey Erickson", Disease = "Non-gonococcal urethritis" },
            };

            mockedCRepo.Setup(repo => repo.GetAll()).Returns(clinics.AsQueryable());
            mockedDRepo.Setup(repo => repo.GetAll()).Returns(doctors.AsQueryable());
            mockedPRepo.Setup(repo => repo.GetAll()).Returns(patients.AsQueryable());

            HospitalLogicBase hlogic = new HospitalLogicBase(mockedCRepo.Object, mockedDRepo.Object, mockedPRepo.Object);

            var result = hlogic.GetClinicGender();

            Assert.That(result, Is.EquivalentTo(expectedGenders));
            mockedCRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedCRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedDRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedDRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedPRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedPRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }
    }
}
