// <copyright file="PatientRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hospital.Data.Tables;
    using Hospital.Repository.Repositories;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Patient's Repository.
    /// </summary>
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="ctx">DBcontext.</param>
        public PatientRepository(HospitalDbContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Change the disease of a patient.
        /// </summary>
        /// <param name="id">Id of the patient.</param>
        /// <param name="newdisease">The new disease.</param>
        public void ChangeDisease(int id, string newdisease)
        {
            var patient = this.GetOne(id);
            if (patient == null)
            {
                throw new InvalidOperationException("Patient not found!");
            }

            patient.Disease = newdisease;
            this.GetCtx.SaveChanges();
        }

        /// <summary>
        /// Get one patient by id.
        /// </summary>
        /// <param name="id">The id of the patient.</param>
        /// <returns>A patient by id.</returns>
        public override Patient GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.PatientId == id);
        }

        /// <summary>
        /// Add a patient.
        /// </summary>
        /// <param name="entity">Entity of the patient.</param>
        /// <returns>The Id of the new entity.</returns>
        public override int Add(Patient entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Patient initalization error!");
            }

            this.GetCtx.Set<Patient>().Add(entity);
            this.GetCtx.SaveChanges();
            return entity.PatientId;
        }

        /// <summary>
        /// Remove a patient.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        public override bool Remove(int id)
        {
            var entity = this.GetOne(id);
            if (entity == null)
            {
                return true;
            }

            //bool contains = false;
            //foreach (var item in this.GetCtx.Set<Treatment>())
            //{
            //    if (id == item.PatientId)
            //    {
            //        contains = true;
            //    }
            //}
 
            foreach (var item in this.GetCtx.Set<Treatment>())
            {
                if (id == item.PatientId)
                {
                    this.GetCtx.Set<Treatment>().Remove(item);
                }
            }

            //if (!contains)
            //{
            //    this.GetCtx.Set<Patient>().Remove(entity);
            //    this.GetCtx.SaveChanges();
            //}

            this.GetCtx.Set<Patient>().Remove(entity);
            this.GetCtx.SaveChanges();

            var check = this.GetOne(id);
            if (check == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
