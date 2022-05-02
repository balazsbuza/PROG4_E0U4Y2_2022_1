// <copyright file="IPatientRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hospital.Data.Tables;

    /// <summary>
    /// IPatientRepository interface for PatientRepository.
    /// </summary>
    public interface IPatientRepository : IRepository<Patient>
    {
        /// <summary>
        /// Change the disease of a patient.
        /// </summary>
        /// <param name="id">Id of the patient.</param>
        /// <param name="newdisease">The new disease.</param>
        void ChangeDisease(int id, string newdisease);
    }
}
