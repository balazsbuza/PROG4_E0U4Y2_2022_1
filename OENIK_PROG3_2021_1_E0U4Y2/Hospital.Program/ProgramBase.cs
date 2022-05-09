// <copyright file="ProgramBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Sysinternals Process Explorer
namespace Hospital.Program
{
    using System;
    using System.Collections.Generic;
    using ConsoleTools;
    using Hospital.Data.Tables;
    using Hospital.Logic;

    /// <summary>
    /// Base program.
    /// </summary>
    public static class ProgramBase
    {
        /// <summary>
        /// Main.
        /// </summary>
        public static void Main()
        {
            var hlogic = new Factory().GetHospitalLogicBase();
            var cdlogic = new Factory().GetClinicDoctorLogic();
            var ptlogic = new Factory().GetPatientTreatmentLogic();
            var mainmenu = new ConsoleMenu();
            var listmenu = new ConsoleMenu().
                Add("List clinics", () => ListAll(cdlogic, ptlogic, 1)).
                Add("List doctors", () => ListAll(cdlogic, ptlogic, 2)).
                Add("List patients", () => ListAll(cdlogic, ptlogic, 3)).
                Add("List treatments", () => ListAll(cdlogic, ptlogic, 4)).
                Add("Back", ConsoleMenu.Close);
            var addmenu = new ConsoleMenu().
                Add("Add a new clinic", () => AddAll(cdlogic, ptlogic, 1)).
                Add("Add a new doctor", () => AddAll(cdlogic, ptlogic, 2)).
                Add("Add a new patient", () => AddAll(cdlogic, ptlogic, 3)).
                Add("Add a new treatment", () => AddAll(cdlogic, ptlogic, 4)).
                Add("Back", ConsoleMenu.Close);
            var changemenu = new ConsoleMenu().
                Add("Change a clinic's address", () => ChangeAll(cdlogic, ptlogic, 1)).
                Add("Change a doctor's degree", () => ChangeAll(cdlogic, ptlogic, 2)).
                Add("Change a patient's disease", () => ChangeAll(cdlogic, ptlogic, 3)).
                Add("Change a treatment's description", () => ChangeAll(cdlogic, ptlogic, 4)).
                Add("Back", ConsoleMenu.Close);
            var removemenu = new ConsoleMenu().
                Add("Remove a clinic by id", () => RemoveAll(cdlogic, ptlogic, 1)).
                Add("Remove a doctor by id", () => RemoveAll(cdlogic, ptlogic, 2)).
                Add("Remove a patient by id", () => RemoveAll(cdlogic, ptlogic, 3)).
                Add("Remove a treatment by id", () => RemoveAll(cdlogic, ptlogic, 4)).
                Add("Back", ConsoleMenu.Close);
            var noncrudmenu = new ConsoleMenu().
                Add("Hospital NON-CRUD", () => NonCrudH(hlogic, 1)).
                Add("Clinic Doctor NON-CRUD", () => NonCrudCD(cdlogic, 1)).
                Add("Patient Treatment NON-CRUD", () => NonCrudPT(ptlogic, 1)).
                Add("Task: Hospital NON-CRUD", () => NonCrudH(hlogic, 2)).
                Add("Task: Clinic Doctor NON-CRUD", () => NonCrudCD(cdlogic, 2)).
                Add("Task: Patient Treatment NON-CRUD", () => NonCrudPT(ptlogic, 2)).
                Add("Back", ConsoleMenu.Close);
            mainmenu.
                Add("Listing", () => listmenu.Show()).
                Add("Adding", () => addmenu.Show()).
                Add("Changing", () => changemenu.Show()).
                Add("Removing", () => removemenu.Show()).
                Add("Listing noncrud", () => noncrudmenu.Show()).
                Add("Close", ConsoleMenu.Close);
            mainmenu.Show();
        }

        private static void ListAll(ClinicDoctorLogic cdlogic, PatientTreatmentLogic ptlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    foreach (var item in cdlogic.GetAllClinics())
                    {
                        Console.WriteLine("ClinicId: " + item.ClinicId + ", Company: " + item.Company + ", Address: " + item.Address + ", Name: " + item.Name + ", Officehours: " + item.Officehours + ", Phone: " + item.Phone);
                    }

                    break;
                case 2:
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        Console.WriteLine("DoctorId: " + item.DoctorId + ", Name: " + item.Name + ", ClinicId: " + item.ClinicId + ", Dateofbirth: " + item.Dateofbirth + ", Degree: " + item.Degree + ", Sealnumber: " + item.Sealnumber + ", Specialization: " + item.Specialization);
                    }

                    break;
                case 3:
                    foreach (var item in ptlogic.GetAllPatients())
                    {
                        Console.WriteLine("PateintId: " + item.PatientId + ", Name: " + item.Name + ", DoctorId: " + item.DoctorId + ", Dateofbirth: " + item.Dateofbirth + ", Gender: " + item.Gender + ", Nameofmother " + item.Nameofmother + ", Disease: " + item.Disease);
                    }

                    break;
                case 4:
                    foreach (var item in ptlogic.GetAllTreatments())
                    {
                        Console.WriteLine("TreatmentId: " + item.TreatmentId + ", DoctorId: " + item.DoctorId + ", PateintId: " + item.PatientId + ", Treatmenttime: " + item.Treatmenttime + ", Description: " + item.Description);
                    }

                    break;
            }

            Console.ReadLine();
        }

        private static void AddAll(ClinicDoctorLogic cdlogic, PatientTreatmentLogic ptlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    Clinic clinic = new Clinic();
                    Console.WriteLine(new string("Name of the clinic"));
                    clinic.Name = Console.ReadLine();
                    Console.WriteLine(new string("Address of the clinic"));
                    clinic.Address = Console.ReadLine();
                    Console.WriteLine(new string("Company name of the clinic"));
                    clinic.Company = Console.ReadLine();
                    Console.WriteLine(new string("Office hours of the clinic"));
                    clinic.Officehours = Console.ReadLine();
                    Console.WriteLine("The new clinic's id:" + cdlogic.AddOneClinic(clinic));

                    break;
                case 2:
                    Doctor doctor = new Doctor();
                    Console.WriteLine(new string("Name of the doctor"));
                    doctor.Name = Console.ReadLine();
                    Console.WriteLine(new string("Id of the clinic where the doctor works"));
                    foreach (var item in cdlogic.GetAllClinics())
                    {
                        Console.Write(item.ClinicId + ", ");
                    }

                    Console.WriteLine();
                    doctor.ClinicId = IdChecker(GetIds(1, cdlogic, ptlogic));
                    Console.WriteLine(new string("Degree of the doctor"));
                    doctor.Degree = Console.ReadLine();
                    Console.WriteLine(new string("Specilaization of the doctor"));
                    doctor.Specialization = Console.ReadLine();
                    Console.WriteLine("The new doctor's id:" + cdlogic.AddOneDoctor(doctor));

                    break;
                case 3:
                    Patient patient = new Patient();
                    Console.WriteLine(new string("Name of the Patient"));
                    patient.Name = Console.ReadLine();
                    Console.WriteLine(new string("Id of the patient's doctor"));
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        Console.Write(item.DoctorId + ", ");
                    }

                    Console.WriteLine();
                    patient.DoctorId = IdChecker(GetIds(2, cdlogic, ptlogic));
                    Console.WriteLine(new string("Gender of the patient: M, F, O"));

                    string gender = string.Empty;
                    while (gender != "M" && gender != "F" && gender != "O")
                    {
                        Console.WriteLine(new string("Enter the right character"));
                        gender = Console.ReadLine();
                    }

                    patient.Gender = gender;
                    Console.WriteLine(new string("Name of the patient's mother"));
                    patient.Nameofmother = Console.ReadLine();
                    Console.WriteLine("The new patient's id:" + ptlogic.AddOnePatient(patient));

                    break;
                case 4:
                    Treatment treatment = new Treatment();
                    Console.WriteLine(new string("Id of the doctor"));
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        Console.Write(item.DoctorId + ", ");
                    }

                    Console.WriteLine();
                    treatment.DoctorId = IdChecker(GetIds(2, cdlogic, ptlogic));
                    Console.WriteLine(new string("Id of the patient"));
                    foreach (var item in ptlogic.GetAllPatients())
                    {
                        Console.Write(item.PatientId + ", ");
                    }

                    Console.WriteLine();
                    treatment.PatientId = IdChecker(GetIds(3, cdlogic, ptlogic));
                    Console.WriteLine(new string("Description of the treatment"));
                    treatment.Description = Console.ReadLine();
                    Console.WriteLine(new string("Treatment time (YYYY MM DD)"));
                    DateTime treatmentDateTime;
                    if (DateTime.TryParse(Console.ReadLine(), out treatmentDateTime))
                    {
                        Console.WriteLine("Added value:" + treatmentDateTime.Date);
                    }
                    else
                    {
                        Console.WriteLine(new string("Incorrect value, added 1970.01.01. to treatmenttime."));
                        treatmentDateTime = new DateTime(1970, 01, 01);
                    }

                    treatment.Treatmenttime = treatmentDateTime;
                    Console.WriteLine("The new treatment's id:" + ptlogic.AddOneTreatment(treatment));

                    break;
            }

            Console.ReadLine();
        }

        private static void ChangeAll(ClinicDoctorLogic cdlogic, PatientTreatmentLogic ptlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(new string("Id of the clinic"));
                    foreach (var item in cdlogic.GetAllClinics())
                    {
                        Console.Write(item.ClinicId + ", ");
                    }

                    Console.WriteLine();
                    int cid = IdChecker(GetIds(1, cdlogic, ptlogic));
                    Console.WriteLine(new string("Address of the clinic"));
                    string address = Console.ReadLine();
                    //cdlogic.ChangeOneClinicAddress(cid, address);
                    Console.WriteLine(new string("Done"));

                    break;
                case 2:
                    Console.WriteLine(new string("Id of the doctor"));
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        Console.Write(item.DoctorId + ", ");
                    }

                    Console.WriteLine();
                    int did = IdChecker(GetIds(2, cdlogic, ptlogic));
                    Console.WriteLine(new string("The new degree"));
                    string degree = Console.ReadLine();
                    //cdlogic.ChangeOneDoctorDegree(did, degree);
                    Console.WriteLine(new string("Done"));

                    break;
                case 3:
                    Console.WriteLine(new string("Id of the patient"));
                    foreach (var item in ptlogic.GetAllPatients())
                    {
                        Console.Write(item.PatientId + ", ");
                    }

                    Console.WriteLine();
                    int pid = IdChecker(GetIds(3, cdlogic, ptlogic));
                    Console.WriteLine(new string("The new disease"));
                    string disease = Console.ReadLine();
                    //ptlogic.ChangeOnePatientDisease(pid, disease);
                    Console.WriteLine(new string("Done"));

                    break;
                case 4:
                    Console.WriteLine(new string("Id of the treatment"));
                    foreach (var item in ptlogic.GetAllTreatments())
                    {
                        Console.Write(item.TreatmentId + ", ");
                    }

                    Console.WriteLine();
                    int tid = IdChecker(GetIds(4, cdlogic, ptlogic));
                    Console.WriteLine(new string("The new description"));
                    string desc = Console.ReadLine();
                    //ptlogic.ChangeOneTreatmentDescription(tid, desc);
                    Console.WriteLine(new string("Done"));

                    break;
            }

            Console.ReadLine();
        }

        private static void RemoveAll(ClinicDoctorLogic cdlogic, PatientTreatmentLogic ptlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    int cid;
                    Console.WriteLine(new string("Id of a clinic"));
                    foreach (var item in cdlogic.GetAllClinics())
                    {
                        Console.Write(item.ClinicId + ", ");
                    }

                    Console.WriteLine();
                    cid = IdChecker(GetIds(1, cdlogic, ptlogic));
                    Console.WriteLine("Deletion success: " + cdlogic.RemoveOneClinic(cid));

                    break;
                case 2:
                    cdlogic.GetAllDoctors();
                    int did;
                    Console.WriteLine(new string("Id of a doctor"));
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        Console.Write(item.DoctorId + ", ");
                    }

                    Console.WriteLine();
                    did = IdChecker(GetIds(2, cdlogic, ptlogic));
                    Console.WriteLine("Deletion success: " + cdlogic.RemoveOneDoctor(did));

                    break;
                case 3:
                    int pid;
                    Console.WriteLine(new string("Id of a patient"));
                    foreach (var item in ptlogic.GetAllPatients())
                    {
                        Console.Write(item.PatientId + ", ");
                    }

                    Console.WriteLine();
                    pid = IdChecker(GetIds(3, cdlogic, ptlogic));
                    Console.WriteLine("Deletion success: " + ptlogic.RemoveOnePatient(pid));

                    break;
                case 4:
                    int tid;
                    Console.WriteLine(new string("Id of a treatment"));
                    foreach (var item in ptlogic.GetAllTreatments())
                    {
                        Console.Write(item.TreatmentId + ", ");
                    }

                    Console.WriteLine();
                    tid = IdChecker(GetIds(4, cdlogic, ptlogic));
                    Console.WriteLine("Deletion success: " + ptlogic.RemoveOneTreatment(tid));

                    break;
            }

            Console.ReadLine();
        }

        private static void NonCrudH(HospitalLogicBase hlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(new string("Name of the clinics and their number of patients by gender"));
                    foreach (var item in hlogic.GetClinicGender())
                    {
                        Console.WriteLine(item);
                    }

                    break;

                case 2:
                    Console.WriteLine(new string("Task:Name of the clinics and their number of patients by gender"));
                    foreach (var item in hlogic.GetClinicGenderAsync().Result)
                    {
                        Console.WriteLine(item);
                    }

                    break;
            }

            Console.ReadLine();
        }

        private static void NonCrudCD(ClinicDoctorLogic cdlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(new string("Number of doctors and their workaddress"));
                    foreach (var item in cdlogic.GetDoctorWorkAddress())
                    {
                        Console.WriteLine(item);
                    }

                    break;

                case 2:
                    Console.WriteLine(new string("Task:Number of doctors and their workaddress"));
                    foreach (var item in cdlogic.GetDoctorWorkAddressAsync().Result)
                    {
                        Console.WriteLine(item);
                    }

                    break;
            }

            Console.ReadLine();
        }

        private static void NonCrudPT(PatientTreatmentLogic ptlogic, int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(new string("The patient and their last treatmenttime, if they had any"));
                    foreach (var item in ptlogic.GetPatientLastTreatment())
                    {
                        Console.WriteLine(item);
                    }

                    break;

                case 2:
                    Console.WriteLine(new string("Task:The patient and their last treatmenttime, if they had any"));
                    foreach (var item in ptlogic.GetPatientLastTreatmentAsync().Result)
                    {
                        Console.WriteLine(item);
                    }

                    break;
            }

            Console.ReadLine();
        }

        private static int IdChecker(List<int> ids)
        {
            int id;

            while (!int.TryParse(Console.ReadLine(), out id) || !ids.Contains(id))
            {
                Console.WriteLine(new string("You entered an invalid number or character"));
            }

            return id;
        }

        private static List<int> GetIds(int caseSwitch, ClinicDoctorLogic cdlogic, PatientTreatmentLogic ptlogic)
        {
            List<int> ids = new List<int>();
            switch (caseSwitch)
            {
                case 1:
                    foreach (var item in cdlogic.GetAllClinics())
                    {
                        ids.Add(item.ClinicId);
                    }

                    break;
                case 2:
                    foreach (var item in cdlogic.GetAllDoctors())
                    {
                        ids.Add(item.DoctorId);
                    }

                    break;
                case 3:
                    foreach (var item in ptlogic.GetAllPatients())
                    {
                        ids.Add(item.PatientId);
                    }

                    break;
                case 4:
                    foreach (var item in ptlogic.GetAllTreatments())
                    {
                        ids.Add(item.TreatmentId);
                    }

                    break;
            }

            return ids;
        }
    }
}
