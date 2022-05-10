// <copyright file="ClinicDoctorLogicTests.cs" company="PlaceholderCompany">
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
    /// Tests for Clinic and Doctor logic.
    /// </summary>
    [TestFixture]
    public class ClinicDoctorLogicTests
    {
        /// <summary>
        /// Tests for clinics and doctor by count.
        /// </summary>
        [Test]
        public void TestGetDoctorWorkAddress()
        {
            Mock<IClinicRepository> mockedCRepo = new Mock<IClinicRepository>(
               MockBehavior.Loose);

            Mock<IDoctorRepository> mockedDRepo = new Mock<IDoctorRepository>(
               MockBehavior.Loose);

            List<DoctorWorkaddress> workAddress = new List<DoctorWorkaddress>()
            {
                new DoctorWorkaddress() { ClinicAddress = "First street", NumberOfDoctors = 1 },
                new DoctorWorkaddress() { ClinicAddress = "Second avenue", NumberOfDoctors = 2 },
                new DoctorWorkaddress() { ClinicAddress = "Third road", NumberOfDoctors = 2 },
            };

            List<Clinic> clinics = new List<Clinic>()
            {
                new Clinic() { ClinicId = 1, Address = "First street", Company = "Wellnest comp", Name = "Wellnest", Officehours = "8:00-16:00", Phone = 1112223 },
                new Clinic() { ClinicId = 2, Address = "Second avenue", Company = "Neoclinic comp", Name = "Neoclinic", Officehours = "7:00-16:00", Phone = 2223334 },
                new Clinic() { ClinicId = 3, Address = "Third road", Company = "Clinicfit comp", Name = "Clinicfit", Officehours = "8:00-15:00", Phone = 3334445 },
            };

            List<Doctor> doctors = new List<Doctor>()
            {
                //new Doctor() { DoctorId = 1, Name = "Gedeon Umar", ClinicId = 1, Dateofbirth = new DateTime(1980, 1, 1), Degree = "Master of Medicine", Sealnumber = 55321, Specialization = "Cardiology" },
                //new Doctor() { DoctorId = 2, Name = "Eliza Remigio", ClinicId = 2, Dateofbirth = new DateTime(1981, 2, 2), Degree = "Master of Surgery", Sealnumber = 44321, Specialization = "Family Medicine" },
                //new Doctor() { DoctorId = 3, Name = "Larisa Branislav", ClinicId = 3, Dateofbirth = new DateTime(1982, 3, 3), Degree = "Doctor of Medicine", Sealnumber = 33321, Specialization = "Hematology" },
                //new Doctor() { DoctorId = 4, Name = "Vavrinec Daireann", ClinicId = 3, Dateofbirth = new DateTime(1983, 4, 4), Degree = "Doctor of Osteopathic Medicine", Sealnumber = 22321, Specialization = "Medical genetics" },
                //new Doctor() { DoctorId = 5, Name = "Rhagouel Ríoghnach", ClinicId = 2, Dateofbirth = new DateTime(1984, 5, 5), Degree = "Bachelor of Medicine", Sealnumber = 11321, Specialization = "Neurology" },
            };

            mockedCRepo.Setup(repo => repo.GetAll()).Returns(clinics.AsQueryable());
            mockedDRepo.Setup(repo => repo.GetAll()).Returns(doctors.AsQueryable());

            ClinicDoctorLogic cdlogic = new ClinicDoctorLogic(mockedCRepo.Object, mockedDRepo.Object);

            var result = cdlogic.GetDoctorWorkAddress();

            Assert.That(result, Is.EquivalentTo(workAddress));
            mockedCRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedCRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedDRepo.Verify(repo => repo.GetAll(), Times.Exactly(1));
            mockedDRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Tests for adding a clinic.
        /// </summary>
        [Test]
        public void TestAddClinic()
        {
            Mock<IClinicRepository> mockedCRepo = new Mock<IClinicRepository>(
               MockBehavior.Loose);

            Mock<IDoctorRepository> mockedDRepo = new Mock<IDoctorRepository>(
               MockBehavior.Loose);

            Clinic exClinic = new Clinic() { Address = "Fourth street", Company = "Fourth comp", Name = "Fourth", Officehours = "8:00-16:00", Phone = 1112223 };
            mockedCRepo.Setup(repo => repo.Add(It.IsAny<Clinic>())).Returns(4);

            ClinicDoctorLogic cdlogic = new ClinicDoctorLogic(mockedCRepo.Object, mockedDRepo.Object);

            var result = cdlogic.AddOneClinic(exClinic);

            mockedCRepo.Verify(repo => repo.Add(It.IsAny<Clinic>()), Times.Once);
            mockedCRepo.Verify(repo => repo.Add(exClinic), Times.Once);
        }

        /// <summary>
        /// Tests for changing doctor degree.
        /// </summary>
        [Test]
        public void TestChangeDoctor()
        {
            Mock<IClinicRepository> mockedCRepo = new Mock<IClinicRepository>(
               MockBehavior.Loose);

            Mock<IDoctorRepository> mockedDRepo = new Mock<IDoctorRepository>(
               MockBehavior.Loose);

            mockedDRepo.Setup(repo => repo.ChangeDegree(1, "TestDegree"));

            ClinicDoctorLogic cdlogic = new ClinicDoctorLogic(mockedCRepo.Object, mockedDRepo.Object);
            //cdlogic.ChangeOneDoctorDegree(1, "TestDegree");

            mockedDRepo.Verify(repo => repo.ChangeDegree(1, "TestDegree"), Times.Exactly(1));
        }

        /// <summary>
        /// Tests for removing a doctor.
        /// </summary>
        [Test]
        public void TestRemoveDoctor()
        {
            Mock<IClinicRepository> mockedCRepo = new Mock<IClinicRepository>(
               MockBehavior.Loose);

            Mock<IDoctorRepository> mockedDRepo = new Mock<IDoctorRepository>(
               MockBehavior.Loose);

            mockedDRepo.Setup(repo => repo.Remove(It.IsAny<int>())).Returns(true);

            ClinicDoctorLogic cdlogic = new ClinicDoctorLogic(mockedCRepo.Object, mockedDRepo.Object);

            var result = cdlogic.RemoveOneDoctor(5);

            mockedDRepo.Verify(repo => repo.Remove(5), Times.Exactly(1));
        }
    }
}
