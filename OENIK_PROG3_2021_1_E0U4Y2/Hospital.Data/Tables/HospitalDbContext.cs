// <copyright file="HospitalDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Data.Tables
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Hospital database.
    /// </summary>
    public class HospitalDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalDbContext"/> class.
        /// </summary>
        public HospitalDbContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets Doctors in db.
        /// </summary>
        public virtual DbSet<Doctor> Doctors { get; set; }

        /// <summary>
        /// Gets or sets Patients in db.
        /// </summary>
        public virtual DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets Clinics in db.
        /// </summary>
        public virtual DbSet<Clinic> Clinics { get; set; }

        /// <summary>
        /// Gets or sets Treatments in db.
        /// </summary>
        public virtual DbSet<Treatment> Treatments { get; set; }

        /// <summary>
        /// DB configuration.
        /// </summary>
        /// <param name="optionsBuilder">Building options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\HospitalDb.mdf;Integrated Security=True; MultipleActiveResultSets = true");
            }
        }

        /// <summary>
        /// Creates tables and inserts records into it.
        /// </summary>
        /// <param name="modelBuilder">Model building.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            Clinic clinic1 = new Clinic() { ClinicId = 1, Address = "First street", Company = "Wellnest comp", Name = "Wellnest", Officehours = "8:00-16:00", Phone = 1112223 };
            Clinic clinic2 = new Clinic() { ClinicId = 2, Address = "Second avenue", Company = "Neoclinic comp", Name = "Neoclinic", Officehours = "7:00-16:00", Phone = 2223334 };
            Clinic clinic3 = new Clinic() { ClinicId = 3, Address = "Third road", Company = "Clinicfit comp", Name = "Clinicfit", Officehours = "8:00-15:00", Phone = 3334445 };

            Doctor doctor1 = new Doctor() { DoctorId = 1, Name = "Gedeon Umar", ClinicId = clinic1.ClinicId, Dateofbirth = new DateTime(1980, 1, 1), Degree = "Master of Medicine", Sealnumber = 55321, Specialization = "Cardiology" };
            Doctor doctor2 = new Doctor() { DoctorId = 2, Name = "Eliza Remigio", ClinicId = clinic2.ClinicId, Dateofbirth = new DateTime(1981, 2, 2), Degree = "Master of Surgery", Sealnumber = 44321, Specialization = "Family Medicine" };
            Doctor doctor3 = new Doctor() { DoctorId = 3, Name = "Larisa Branislav", ClinicId = clinic3.ClinicId, Dateofbirth = new DateTime(1982, 3, 3), Degree = "Doctor of Medicine", Sealnumber = 33321, Specialization = "Hematology" };
            Doctor doctor4 = new Doctor() { DoctorId = 4, Name = "Vavrinec Daireann", ClinicId = clinic3.ClinicId, Dateofbirth = new DateTime(1983, 4, 4), Degree = "Doctor of Osteopathic Medicine", Sealnumber = 22321, Specialization = "Medical genetics" };
            Doctor doctor5 = new Doctor() { DoctorId = 5, Name = "Rhagouel Ríoghnach", ClinicId = clinic2.ClinicId, Dateofbirth = new DateTime(1984, 5, 5), Degree = "Bachelor of Medicine", Sealnumber = 11321, Specialization = "Neurology" };

            Patient patient1 = new Patient() { PatientId = 1, Name = "Brian Steele", Dateofbirth = new DateTime(1980, 1, 1), DoctorId = doctor1.DoctorId, Gender = "M", Nameofmother = "Stella Flowers", Disease = "Chikungunya" };
            Patient patient2 = new Patient() { PatientId = 2, Name = "Ricky Hunter", Dateofbirth = new DateTime(1981, 2, 2), DoctorId = doctor2.DoctorId, Gender = "F", Nameofmother = "Bonnie Doyle", Disease = "Keratitis" };
            Patient patient3 = new Patient() { PatientId = 3, Name = "Kathryn Walker", Dateofbirth = new DateTime(1982, 3, 3), DoctorId = doctor3.DoctorId, Gender = "O", Nameofmother = "Sharon Hall", Disease = "Campylobacteriosis" };
            Patient patient4 = new Patient() { PatientId = 4, Name = "Kurt Jones", Dateofbirth = new DateTime(1983, 4, 4), DoctorId = doctor4.DoctorId, Gender = "M", Nameofmother = "Beth Harper", Disease = "Alopecia areata" };
            Patient patient5 = new Patient() { PatientId = 5, Name = "Katherine Morgan", Dateofbirth = new DateTime(1984, 5, 5), DoctorId = doctor1.DoctorId, Gender = "F", Nameofmother = "Iris Powers", Disease = "Rheumatism" };
            Patient patient6 = new Patient() { PatientId = 6, Name = "Emanuel Jennings", Dateofbirth = new DateTime(1985, 6, 6), DoctorId = doctor2.DoctorId, Gender = "O", Nameofmother = "Louise Terry", Disease = "Lymphoma" };
            Patient patient7 = new Patient() { PatientId = 7, Name = "Leeroy Jenkins", Dateofbirth = new DateTime(1986, 7, 7), DoctorId = doctor3.DoctorId, Gender = "M", Nameofmother = "Tracy Dennis", Disease = "Schistosomiasis" };
            Patient patient8 = new Patient() { PatientId = 8, Name = "Clark Wise", Dateofbirth = new DateTime(1987, 8, 8), DoctorId = doctor4.DoctorId, Gender = "F", Nameofmother = "Casey Erickson", Disease = "Non-gonococcal urethritis" };

            Treatment treatment1 = new Treatment() { TreatmentId = 1, DoctorId = doctor1.DoctorId, PatientId = patient1.PatientId, Treatmenttime = new DateTime(1994, 1, 5), Description = "Checkup" };
            Treatment treatment2 = new Treatment() { TreatmentId = 2, DoctorId = doctor2.DoctorId, PatientId = patient2.PatientId, Treatmenttime = new DateTime(1995, 2, 6), Description = "Blood test" };
            Treatment treatment3 = new Treatment() { TreatmentId = 3, DoctorId = doctor3.DoctorId, PatientId = patient3.PatientId, Treatmenttime = new DateTime(1996, 3, 7), Description = "DNA test" };
            Treatment treatment4 = new Treatment() { TreatmentId = 4, DoctorId = doctor4.DoctorId, PatientId = patient4.PatientId, Treatmenttime = new DateTime(1997, 4, 8), Description = "Regular test" };
            Treatment treatment5 = new Treatment() { TreatmentId = 5, DoctorId = doctor1.DoctorId, PatientId = patient1.PatientId, Treatmenttime = new DateTime(1994, 2, 5), Description = "Biopsy" };
            Treatment treatment6 = new Treatment() { TreatmentId = 6, DoctorId = doctor2.DoctorId, PatientId = patient2.PatientId, Treatmenttime = new DateTime(1995, 3, 6), Description = "Autopsy" };
            Treatment treatment7 = new Treatment() { TreatmentId = 7, DoctorId = doctor3.DoctorId, PatientId = patient3.PatientId, Treatmenttime = new DateTime(1996, 4, 7), Description = "Endoscopy" };
            Treatment treatment8 = new Treatment() { TreatmentId = 8, DoctorId = doctor4.DoctorId, PatientId = patient4.PatientId, Treatmenttime = new DateTime(1997, 5, 8), Description = "Liver function test" };
            Treatment treatment9 = new Treatment() { TreatmentId = 9, DoctorId = doctor1.DoctorId, PatientId = patient1.PatientId, Treatmenttime = new DateTime(1994, 5, 5), Description = "Checkup" };
            Treatment treatment10 = new Treatment() { TreatmentId = 10, DoctorId = doctor2.DoctorId, PatientId = patient2.PatientId, Treatmenttime = new DateTime(1995, 6, 6), Description = "Blood test" };
            Treatment treatment11 = new Treatment() { TreatmentId = 11, DoctorId = doctor3.DoctorId, PatientId = patient3.PatientId, Treatmenttime = new DateTime(1996, 7, 7), Description = "DNA test" };
            Treatment treatment12 = new Treatment() { TreatmentId = 12, DoctorId = doctor4.DoctorId, PatientId = patient4.PatientId, Treatmenttime = new DateTime(1997, 8, 8), Description = "Kidney function test" };

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasOne(doctor => doctor.Clinic)
                    .WithMany(clinic => clinic.Doctors)
                    .HasForeignKey(doctor => doctor.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(patient => patient.Doctor)
                    .WithMany(doctor => doctor.Patients)
                    .HasForeignKey(patient => patient.DoctorId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasOne(treatment => treatment.Doctor)
                    .WithMany(doctor => doctor.Treatments)
                    .HasForeignKey(treatment => treatment.DoctorId)
                    .OnDelete(DeleteBehavior.ClientCascade);

                entity.HasOne(treatment => treatment.Patient)
                    .WithMany(patient => patient.Treatments)
                    .HasForeignKey(treatment => treatment.PatientId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Clinic>().HasData(clinic1, clinic2, clinic3);
            modelBuilder.Entity<Doctor>().HasData(doctor1, doctor2, doctor3, doctor4, doctor5);
            modelBuilder.Entity<Patient>().HasData(patient1, patient2, patient3, patient4, patient5, patient6, patient7, patient8);
            modelBuilder.Entity<Treatment>().HasData(treatment1, treatment2, treatment3, treatment4, treatment5, treatment6, treatment7, treatment8, treatment9, treatment10, treatment11, treatment12);
        }
    }
}
