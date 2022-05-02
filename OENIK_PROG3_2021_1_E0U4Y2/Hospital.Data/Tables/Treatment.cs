// <copyright file="Treatment.cs" company="PlaceholderCompany">
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
    /// Treatment table.
    /// </summary>
    [Table("treatment")]
    public class Treatment
    {
        /// <summary>
        /// Gets or sets the Id of the TreatmentId.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentId { get; set; }

        /// <summary>
        /// Gets or sets the Doctor of the treatment.
        /// </summary>
        [NotMapped]
        public virtual Doctor Doctor { get; set; }

        /// <summary>
        /// Gets or sets the Doctorid of the treatment.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the Patient of the treatment.
        /// </summary>
        [NotMapped]
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the Patientid of the treatment.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Description of the treatment.
        /// </summary>
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Treatmenttime of the treatment.
        /// </summary>
        [Required]
        public DateTime Treatmenttime { get; set; }

        /// <summary>
        /// Override ToString for testing.
        /// </summary>
        /// <returns>String output.</returns>
        public override string ToString()
        {
            return $"#{this.TreatmentId}: {this.Description}, treatmenttime: #{this.Treatmenttime}";
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

            if (obj is Treatment)
            {
                Treatment other = obj as Treatment;
                return this.DoctorId == other.DoctorId &&
                    this.PatientId == other.PatientId &&
                    this.TreatmentId == other.TreatmentId &&
                    this.Treatmenttime == other.Treatmenttime &&
                    this.Description == other.Description;
            }

            return false;
        }

        /// <summary>
        /// Override GetHashCode for testing.
        /// </summary>
        /// <returns>The Id.</returns>
        public override int GetHashCode()
        {
            return this.TreatmentId;
        }
    }
}
