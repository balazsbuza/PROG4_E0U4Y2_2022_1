// <copyright file="IPatientTreatmentLogic.cs" company="PlaceholderCompany">
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
    /// Patient and treatment interface creation.
    /// </summary>
    public interface IPatientTreatmentLogic
    {
        /// <summary>
        /// Get one Patient.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>A patient by Id.</returns>
        Patient GetOnePatient(int id);

        /// <summary>
        /// Get one treatment.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>A clinic by Id.</returns>
        Treatment GetOneTreatment(int id);

        /// <summary>
        /// Get every Patient.
        /// </summary>
        /// <returns>A list of Patients.</returns>
        IList<Patient> GetAllPatients();

        /// <summary>
        /// Get every Treatment.
        /// </summary>
        /// <returns>A list of Treatments.</returns>
        IList<Treatment> GetAllTreatments();

        /// <summary>
        /// Add a patient.
        /// </summary>
        /// <param name="entity">Entity of the patient.</param>
        /// <returns>The Id of the new entity.</returns>
        int AddOnePatient(Patient entity);

        /// <summary>
        /// Add a treatment.
        /// </summary>
        /// <param name="entity">Entity of the treatment.</param>
        /// <returns>The Id of the new entity.</returns>
        int AddOneTreatment(Treatment entity);

        /// <summary>
        /// Change one patient's disease.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="disease">The new disease.</param>
        void ChangeOnePatientDisease(Patient entity);

        /// <summary>
        /// Change one treatment's description.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="desc">The new description.</param>
        void ChangeOneTreatmentDescription(Treatment entity);

        /// <summary>
        /// Remove a Patient.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        bool RemoveOnePatient(int id);

        /// <summary>
        /// Remove a Treatment.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        bool RemoveOneTreatment(int id);

        /// <summary>
        /// NON-CRUD gets every patient and their last treatmenttime.
        /// </summary>
        /// <returns>Every patient and their last treatmenttime.</returns>
        IList<PatientLastTreatment> GetPatientLastTreatment();

        IList<PatientTreatmentLastYear> GetPatientTreatmentLastYear();
    }

    /// <summary>
    /// Class for non crud response.
    /// </summary>
    public class PatientLastTreatment
    {
        /// <summary>
        /// Gets or sets the patient's name.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the last treatmenttime.
        /// </summary>
        public DateTime TreatmentTime { get; set; }

        /// <summary>
        /// Override ToString.
        /// </summary>
        /// <returns>The patient's name and the last treatmenttime.</returns>
        public override string ToString()
        {
            return $"PATIENT = {this.PatientName}, TIME = {this.TreatmentTime}";
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

            if (obj is PatientLastTreatment)
            {
                PatientLastTreatment other = obj as PatientLastTreatment;
                return this.PatientName == other.PatientName &&
                    this.TreatmentTime == other.TreatmentTime;
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
            return HashCode.Combine(this.PatientName, this.TreatmentTime);
        }
    }

    public class PatientTreatmentLastYear
    {
        /// <summary>
        /// Gets or sets the patient's name.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the last treatmenttime.
        /// </summary>
        public bool LastYear { get; set; }

        /// <summary>
        /// Override ToString.
        /// </summary>
        /// <returns>The patient's name and the last treatmenttime.</returns>
        public override string ToString()
        {
            return $"PATIENT = {this.PatientName}, COUNT = {this.LastYear}";
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

            if (obj is PatientTreatmentLastYear)
            {
                PatientTreatmentLastYear other = obj as PatientTreatmentLastYear;
                return this.PatientName == other.PatientName &&
                    this.LastYear == other.LastYear;
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
            return HashCode.Combine(this.PatientName, this.LastYear);
        }
    }
}
