// <copyright file="IClinicDoctorLogic.cs" company="PlaceholderCompany">
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
    /// Clinic and doctor logic interface creation.
    /// </summary>
    public interface IClinicDoctorLogic
    {
        /// <summary>
        /// Get one clinic.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>A clinic by Id.</returns>
        Clinic GetOneClinic(int id);

        /// <summary>
        /// Get one doctor.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>A doctor by Id.</returns>
        Doctor GetOneDoctor(int id);

        /// <summary>
        /// Get every Clinic.
        /// </summary>
        /// <returns>A list of Clinics.</returns>
        IList<Clinic> GetAllClinics();

        /// <summary>
        /// Get every Doctor.
        /// </summary>
        /// <returns>A list of Doctors.</returns>
        IList<Doctor> GetAllDoctors();

        /// <summary>
        /// Add a clinic.
        /// </summary>
        /// <param name="entity">Entity of the clinic.</param>
        /// <returns>The Id of the new entity.</returns>
        int AddOneClinic(Clinic entity);

        /// <summary>
        /// Add a doctor.
        /// </summary>
        /// <param name="entity">Entity of the doctor.</param>
        /// <returns>The Id of the new entity.</returns>
        int AddOneDoctor(Doctor entity);

        /// <summary>
        /// Change one clinic's address.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="address">The new address.</param>
        void ChangeOneClinicAddress(Clinic entity);

        /// <summary>
        /// Change one doctor's degree.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="degree">The new degree.</param>
        void ChangeOneDoctorDegree(Doctor entity);

        /// <summary>
        /// Remove a Clinic.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        bool RemoveOneClinic(int id);

        /// <summary>
        /// Remove a Doctor.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        bool RemoveOneDoctor(int id);

        /// <summary>
        /// NON-CRUD gets every doctor and their workaddress.
        /// </summary>
        /// <returns>Every doctor and their workaddress.</returns>
        IList<DoctorWorkaddress> GetDoctorWorkAddress();
    }

    /// <summary>
    /// Class for non crud response.
    /// </summary>
    public class DoctorWorkaddress
    {
        /// <summary>
        /// Gets or sets the number of doctor's.
        /// </summary>
        public int NumberOfDoctors { get; set; }

        /// <summary>
        /// Gets or sets the clinic's address.
        /// </summary>
        public string ClinicAddress { get; set; }

        /// <summary>
        /// Override ToString.
        /// </summary>
        /// <returns>The doctor's name and the number doctors working there.</returns>
        public override string ToString()
        {
            return $"DOCTOR = {this.NumberOfDoctors}, ADDRESS = {this.ClinicAddress}";
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

            if (obj is DoctorWorkaddress)
            {
                DoctorWorkaddress other = obj as DoctorWorkaddress;
                return this.ClinicAddress == other.ClinicAddress &&
                    this.NumberOfDoctors == other.NumberOfDoctors;
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
            return HashCode.Combine(this.NumberOfDoctors, this.ClinicAddress);
        }
    }
}
