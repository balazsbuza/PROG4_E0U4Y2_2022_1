// <copyright file="IDoctorRepository.cs" company="PlaceholderCompany">
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
    /// IDoctorRepository interface for DoctorRepository.
    /// </summary>
    public interface IDoctorRepository : IRepository<Doctor>
    {
        /// <summary>
        /// Change the degree of a doctor.
        /// </summary>
        /// <param name="id">Id of the doctor.</param>
        /// <param name="newdegree">The new degree.</param>
        void ChangeDegree(int id, string newdegree);
    }
}
