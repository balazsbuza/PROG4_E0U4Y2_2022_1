// <copyright file="IClinicRepository.cs" company="PlaceholderCompany">
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
    /// IClinicRepository interface for ClinicRepository.
    /// </summary>
    public interface IClinicRepository : IRepository<Clinic>
    {
        /// <summary>
        /// Change the adress of a clinic.
        /// </summary>
        /// <param name="id">Id of the clinic.</param>
        /// <param name="newadress">The new address.</param>
        void ChangeAdress(int id, string newadress);
    }
}
