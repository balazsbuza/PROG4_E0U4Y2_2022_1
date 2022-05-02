// <copyright file="Doctor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Data.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Doctor table.
    /// </summary>
    [Table("doctor")]
    public class Doctor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Doctor"/> class.
        /// </summary>
        public Doctor()
        {
            this.Patients = new HashSet<Patient>();
            this.Treatments = new HashSet<Treatment>();
        }

        /// <summary>
        /// Gets or sets the Id of the doctor.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the doctor.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Degree of the doctor.
        /// </summary>
        [MaxLength(100)]
        public string Degree { get; set; }

        /// <summary>
        /// Gets or sets the Dateofbirth of the doctor.
        /// </summary>
        public DateTime? Dateofbirth { get; set; }

        /// <summary>
        /// Gets or sets the Sealnumber of the doctor.
        /// </summary>
        [MaxLength(10)]
        public int Sealnumber { get; set; }

        /// <summary>
        /// Gets or sets the Specialization of the doctor.
        /// </summary>
        [MaxLength(100)]
        public string Specialization { get; set; }

        /// <summary>
        /// Gets the Patients of the doctor.
        /// </summary>
        [NotMapped]
        public virtual ICollection<Patient> Patients { get; }

        /// <summary>
        /// Gets the Treatments of the doctor.
        /// </summary>
        [NotMapped]
        public virtual ICollection<Treatment> Treatments { get; }

        /// <summary>
        /// Gets or sets the Doctor's clinic.
        /// </summary>
        [NotMapped]
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Gets or sets the Clinicsid of the doctor.
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Override ToString for testing.
        /// </summary>
        /// <returns>String output.</returns>
        public override string ToString()
        {
            return $"#{this.DoctorId}: {this.Name}, degree: #{this.Degree}";
        }

        /// <summary>
        /// Override Equals for testing.
        /// </summary>
        /// <returns>True if the two objects are the same.</returns>
        /// <param name="obj">The object we are checking.</param>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is Doctor)
            {
                Doctor other = obj as Doctor;
                return this.DoctorId == other.DoctorId &&
                    this.ClinicId == other.ClinicId &&
                    this.Dateofbirth == other.Dateofbirth &&
                    this.Degree == other.Degree &&
                    this.Name == other.Name &&
                    this.Sealnumber == other.Sealnumber &&
                    this.Specialization == other.Specialization;
            }

            return false;
        }

        /// <summary>
        /// Override GetHashCode for testing.
        /// </summary>
        /// <returns>The Id.</returns>
        public override int GetHashCode()
        {
            return this.DoctorId;
        }
    }
}
