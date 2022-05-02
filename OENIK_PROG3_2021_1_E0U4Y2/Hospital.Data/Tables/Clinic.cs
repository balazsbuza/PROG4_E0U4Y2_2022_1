// <copyright file="Clinic.cs" company="PlaceholderCompany">
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
    /// Clinic table.
    /// </summary>
    [Table("clinic")]
    public class Clinic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clinic"/> class.
        /// </summary>
        public Clinic()
        {
            this.Doctors = new HashSet<Doctor>();
        }

        /// <summary>
        /// Gets or sets the Id of the Clinic.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClinicId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Clinic.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Company of the clinic.
        /// </summary>
        [MaxLength(100)]
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the Officehours of the clinic.
        /// </summary>
        [MaxLength(100)]
        public string Officehours { get; set; }

        /// <summary>
        /// Gets or sets the Phone of the clinic.
        /// </summary>
        [MaxLength(10)]
        public int? Phone { get; set; }

        /// <summary>
        /// Gets or sets the Address of the clinic.
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// Gets the Doctors of the clinic.
        /// </summary>
        [NotMapped]
        public virtual ICollection<Doctor> Doctors { get; }

        /// <summary>
        /// Override ToString for testing.
        /// </summary>
        /// <returns>String output.</returns>
        public override string ToString()
        {
            return $"#{this.ClinicId}: {this.Name}, address: #{this.Address} clinic";
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

            if (obj is Clinic)
            {
                Clinic other = obj as Clinic;
                return this.ClinicId == other.ClinicId &&
                    this.Name == other.Name &&
                    this.Address == other.Address &&
                    this.Company == other.Company &&
                    this.Phone == other.Phone &&
                    this.Officehours == other.Officehours;
            }

            return false;
        }

        /// <summary>
        /// Override GetHashCode for testing.
        /// </summary>
        /// <returns>The Id.</returns>
        public override int GetHashCode()
        {
            return this.ClinicId;
        }
    }
}
