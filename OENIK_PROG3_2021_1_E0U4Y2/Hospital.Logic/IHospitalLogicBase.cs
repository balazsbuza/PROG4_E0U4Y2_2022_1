// <copyright file="IHospitalLogicBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hospital.Data.Tables;

    /// <summary>
    /// Hospital base logic interface creation.
    /// </summary>
    public interface IHospitalLogicBase
    {
        /// <summary>
        /// NON-CRUD gets every clinic and their patients by gender.
        /// </summary>
        /// <returns>Every clinic and their patient count by gender.</returns>
        IList<ClinicGender> GetClinicGender();
    }

    /// <summary>
    /// Class for non crud response.
    /// </summary>
    public class ClinicGender
    {
        /// <summary>
        /// Gets or sets the clinic's name.
        /// </summary>
        public string ClinicName { get; set; }

        /// <summary>
        /// Gets or sets the number of Males.
        /// </summary>
        public int NumberofM { get; set; }

        /// <summary>
        /// Gets or sets the number of Females.
        /// </summary>
        public int NumberofF { get; set; }

        /// <summary>
        /// Gets or sets the number of Others.
        /// </summary>
        public int NumberofO { get; set; }

        /// <summary>
        /// Override ToString.
        /// </summary>
        /// <returns>The clinicname, gender, number of genders.</returns>
        public override string ToString()
        {
            return $"CLINIC = {this.ClinicName}, NUMOFMALE = {this.NumberofM}, NUMOFFEMALE = {this.NumberofF}, NUMOFOTHER = {this.NumberofO}";
        }

        /// <summary>
        /// Override Equals() for testing.
        /// </summary>
        /// <param name="obj">Id of the entity.</param>
        /// <returns>True if the objects are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is ClinicGender)
            {
                ClinicGender other = obj as ClinicGender;
                return this.ClinicName == other.ClinicName &&
                    this.NumberofM == other.NumberofM &&
                    this.NumberofF == other.NumberofF &&
                    this.NumberofO == other.NumberofO;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Override GetHasCode() for testing.
        /// </summary>
        /// <returns>The hash.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.ClinicName, this.NumberofM, this.NumberofF, this.NumberofO);
        }
    }
}
