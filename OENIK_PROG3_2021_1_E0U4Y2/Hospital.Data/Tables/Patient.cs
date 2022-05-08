// <copyright file="Patient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Data.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Patient table.
    /// </summary>
    [Table("patient")]
    public class Patient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
        {
            this.Treatments = new HashSet<Treatment>();
        }

        /// <summary>
        /// Gets or sets the Id of the patient.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the patient.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Gender of the patient.
        /// </summary>
        [MaxLength(1)]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the Dateofbirth of the patient.
        /// </summary>
        public DateTime Dateofbirth { get; set; }

        /// <summary>
        /// Gets or sets the Disease of the patient.
        /// </summary>
        public string Disease { get; set; }

        /// <summary>
        /// Gets or sets the Name of the patient's mother.
        /// </summary>
        public string Nameofmother { get; set; }

        /// <summary>
        /// Gets or sets the doctor of the patient.
        /// </summary>
        [NotMapped]
        public virtual Doctor Doctor { get; set; }

        /// <summary>
        /// Gets or sets the doctorid of the patient.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Gets the Treatments of the patient.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Treatment> Treatments { get; }

        /// <summary>
        /// Override ToString for testing.
        /// </summary>
        /// <returns>String output.</returns>
        public override string ToString()
        {
            return $"#{this.PatientId}: {this.Name}, dateofbirth: #{this.Dateofbirth}";
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

            if (obj is Patient)
            {
                Patient other = obj as Patient;
                return this.DoctorId == other.DoctorId &&
                    this.PatientId == other.PatientId &&
                    this.Dateofbirth == other.Dateofbirth &&
                    this.Disease == other.Disease &&
                    this.Name == other.Name &&
                    this.Nameofmother == other.Nameofmother &&
                    this.Gender == other.Gender;
            }

            return false;
        }

        /// <summary>
        /// Override GetHashCode for testing.
        /// </summary>
        /// <returns>The Id.</returns>
        public override int GetHashCode()
        {
            return this.PatientId;
        }
    }
}
