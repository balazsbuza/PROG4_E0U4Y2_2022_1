// <copyright file="PatientTreatmentLogicTests.cs" company="PlaceholderCompany">
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
    /// Tests for Patient and Treatment logic.
    /// </summary>
    [TestFixture]
    public class PatientTreatmentLogicTests
    {
        /// <summary>
        /// Tests for patient and last treatmenttime.
        /// </summary>
        [Test]
        public void TestGetPatientLastTreatment()
        {
            Mock<IPatientRepository> mockedPRepo = new Mock<IPatientRepository>(
                MockBehavior.Loose);

            Mock<ITreatmentRepository> mockedTRepo = new Mock<ITreatmentRepository>(
               MockBehavior.Loose);

            List<PatientLastTreatment> lastTreatment = new List<PatientLastTreatment>()
            {
                new PatientLastTreatment() { PatientName = "Brian Steele", TreatmentTime = new DateTime(1994, 05, 05) },
                new PatientLastTreatment() { PatientName = "Kathryn Walker", TreatmentTime = new DateTime(1996, 07, 07) },
                new PatientLastTreatment() { PatientName = "Kurt Jones", TreatmentTime = new DateTime(1997, 08, 08) },
                new PatientLastTreatment() { PatientName = "Ricky Hunter", TreatmentTime = new DateTime(1995, 06, 06) },
            };

            List<PatientLastTreatment> expectedLastTreatment = new List<PatientLastTreatment>() { lastTreatment[0], lastTreatment[1], lastTreatment[2], lastTreatment[3] };

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

            List<Treatment> treatments = new List<Treatment>()
            {
                new Treatment() { TreatmentId = 1, PatientId = 1, Treatmenttime = new DateTime(1994, 1, 5), Description = "Checkup" },
                new Treatment() { TreatmentId = 2, PatientId = 2, Treatmenttime = new DateTime(1995, 2, 6), Description = "Blood test" },
                new Treatment() { TreatmentId = 3, PatientId = 3, Treatmenttime = new DateTime(1996, 3, 7), Description = "DNA test" },
                new Treatment() { TreatmentId = 4, PatientId = 4, Treatmenttime = new DateTime(1997, 4, 8), Description = "Regular test" },
                new Treatment() { TreatmentId = 5, PatientId = 1, Treatmenttime = new DateTime(1994, 2, 5), Description = "Biopsy" },
                new Treatment() { TreatmentId = 6, PatientId = 2, Treatmenttime = new DateTime(1995, 3, 6), Description = "Autopsy" },
                new Treatment() { TreatmentId = 7, PatientId = 3, Treatmenttime = new DateTime(1996, 4, 7), Description = "Endoscopy" },
                new Treatment() { TreatmentId = 8, PatientId = 4, Treatmenttime = new DateTime(1997, 5, 8), Description = "Liver function test" },
                new Treatment() { TreatmentId = 9, PatientId = 1, Treatmenttime = new DateTime(1994, 5, 5), Description = "Checkup" },
                new Treatment() { TreatmentId = 10, PatientId = 2, Treatmenttime = new DateTime(1995, 6, 6), Description = "Blood test" },
                new Treatment() { TreatmentId = 11, PatientId = 3, Treatmenttime = new DateTime(1996, 7, 7), Description = "DNA test" },
                new Treatment() { TreatmentId = 12, PatientId = 4, Treatmenttime = new DateTime(1997, 8, 8), Description = "Kidney function test" },
            };

            mockedPRepo.Setup(repo => repo.GetAll()).Returns(patients.AsQueryable());
            mockedTRepo.Setup(repo => repo.GetAll()).Returns(treatments.AsQueryable());

            PatientTreatmentLogic ptlogic = new PatientTreatmentLogic(mockedPRepo.Object, mockedTRepo.Object);

            var result = ptlogic.GetPatientLastTreatment();

            Assert.That(result, Is.EquivalentTo(expectedLastTreatment));
            mockedPRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedPRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedTRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedTRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Tests for GetAllTreatment.
        /// </summary>
        [Test]
        public void TestGetAllTreatment()
        {
            Mock<IPatientRepository> mockedPRepo = new Mock<IPatientRepository>(
                MockBehavior.Loose);

            Mock<ITreatmentRepository> mockedTRepo = new Mock<ITreatmentRepository>(
               MockBehavior.Loose);

            List<Treatment> treatments = new List<Treatment>()
            {
                new Treatment() { TreatmentId = 1, PatientId = 1, Treatmenttime = new DateTime(1994, 1, 5), Description = "Checkup" },
                new Treatment() { TreatmentId = 2, PatientId = 2, Treatmenttime = new DateTime(1995, 2, 6), Description = "Blood test" },
                new Treatment() { TreatmentId = 3, PatientId = 3, Treatmenttime = new DateTime(1996, 3, 7), Description = "DNA test" },
                new Treatment() { TreatmentId = 4, PatientId = 4, Treatmenttime = new DateTime(1997, 4, 8), Description = "Regular test" },
                new Treatment() { TreatmentId = 5, PatientId = 1, Treatmenttime = new DateTime(1994, 2, 5), Description = "Biopsy" },
                new Treatment() { TreatmentId = 6, PatientId = 2, Treatmenttime = new DateTime(1995, 3, 6), Description = "Autopsy" },
                new Treatment() { TreatmentId = 7, PatientId = 3, Treatmenttime = new DateTime(1996, 4, 7), Description = "Endoscopy" },
                new Treatment() { TreatmentId = 8, PatientId = 4, Treatmenttime = new DateTime(1997, 5, 8), Description = "Liver function test" },
                new Treatment() { TreatmentId = 9, PatientId = 1, Treatmenttime = new DateTime(1994, 5, 5), Description = "Checkup" },
                new Treatment() { TreatmentId = 10, PatientId = 2, Treatmenttime = new DateTime(1995, 6, 6), Description = "Blood test" },
                new Treatment() { TreatmentId = 11, PatientId = 3, Treatmenttime = new DateTime(1996, 7, 7), Description = "DNA test" },
                new Treatment() { TreatmentId = 12, PatientId = 4, Treatmenttime = new DateTime(1997, 8, 8), Description = "Kidney function test" },
            };

            mockedTRepo.Setup(repo => repo.GetAll()).Returns(treatments.AsQueryable());
            PatientTreatmentLogic ptlogic = new PatientTreatmentLogic(mockedPRepo.Object, mockedTRepo.Object);

            var result = ptlogic.GetAllTreatments();

            mockedTRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedTRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Tests for GetOnePatient logic.
        /// </summary>
        [Test]
        public void TestGetOnePatient()
        {
            Mock<IPatientRepository> mockedPRepo = new Mock<IPatientRepository>(
                MockBehavior.Loose);

            Mock<ITreatmentRepository> mockedTRepo = new Mock<ITreatmentRepository>(
               MockBehavior.Loose);

            List<Patient> patients = new List<Patient>()
            {
                new Patient() { PatientId = 1, Name = "Brian Steele", Dateofbirth = new DateTime(1980, 1, 1), Gender = "M", Nameofmother = "Stella Flowers", Disease = "Chikungunya" },
                new Patient() { PatientId = 2, Name = "Ricky Hunter", Dateofbirth = new DateTime(1981, 2, 2), Gender = "F", Nameofmother = "Bonnie Doyle", Disease = "Keratitis" },
                new Patient() { PatientId = 3, Name = "Kathryn Walker", Dateofbirth = new DateTime(1982, 3, 3), Gender = "O", Nameofmother = "Sharon Hall", Disease = "Campylobacteriosis" },
                new Patient() { PatientId = 4, Name = "Kurt Jones", Dateofbirth = new DateTime(1983, 4, 4), Gender = "M", Nameofmother = "Beth Harper", Disease = "Alopecia areata" },
                new Patient() { PatientId = 5, Name = "Katherine Morgan", Dateofbirth = new DateTime(1984, 5, 5), Gender = "F", Nameofmother = "Iris Powers", Disease = "Rheumatism" },
                new Patient() { PatientId = 6, Name = "Emanuel Jennings", Dateofbirth = new DateTime(1985, 6, 6), Gender = "O", Nameofmother = "Louise Terry", Disease = "Lymphoma" },
                new Patient() { PatientId = 7, Name = "Leeroy Jenkins", Dateofbirth = new DateTime(1986, 7, 7), Gender = "M", Nameofmother = "Tracy Dennis", Disease = "Schistosomiasis" },
                new Patient() { PatientId = 8, Name = "Clark Wise", Dateofbirth = new DateTime(1987, 8, 8), Gender = "F", Nameofmother = "Casey Erickson", Disease = "Non-gonococcal urethritis" },
            };

            Patient expectedPatient = patients[0];
            mockedPRepo.Setup(repo => repo.GetOne(It.IsAny<int>())).Returns(expectedPatient);
            PatientTreatmentLogic ptlogic = new PatientTreatmentLogic(mockedPRepo.Object, mockedTRepo.Object);

            var result = ptlogic.GetOnePatient(1);

            mockedPRepo.Verify(repo => repo.GetOne(1), Times.Once);
        }
    }
}
