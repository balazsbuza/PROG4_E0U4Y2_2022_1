// <copyright file="ITreatmentRepository.cs" company="PlaceholderCompany">
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
    /// ITreatmentRepository interface for TreatmentRepository.
    /// </summary>
    public interface ITreatmentRepository : IRepository<Treatment>
    {
        /// <summary>
        /// Change the description of a treatment.
        /// </summary>
        /// <param name="id">Id of the treatment.</param>
        /// <param name="newdesc">The new description.</param>
        void ChangeDescription(int id, string newdesc);
    }
}
