// <copyright file="ClinicRepository.cs" company="PlaceholderCompany">
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
    /// Clinic's Repository.
    /// </summary>
    public class ClinicRepository : RepositoryBase<Clinic>, IClinicRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicRepository"/> class.
        /// </summary>
        /// <param name="ctx">DBcontext.</param>
        public ClinicRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Change the adress of a clinic.
        /// </summary>
        /// <param name="id">Id of the clinic.</param>
        /// <param name="newadress">The new address.</param>
        public void ChangeAdress(int id, string newadress)
        {
            var clinic = this.GetOne(id);
            if (clinic == null)
            {
                throw new InvalidOperationException("Clinic not found!");
            }

            clinic.Address = newadress;
            this.GetCtx.SaveChanges();
        }

        /// <summary>
        /// Get one clinic by id.
        /// </summary>
        /// <param name="id">The id of the clinic.</param>
        /// <returns>A clinic by id.</returns>
        public override Clinic GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.ClinicId == id);
        }

        /// <summary>
        /// Add a clinic.
        /// </summary>
        /// <param name="entity">Entity of the clinic.</param>
        /// <returns>The Id of the new entity.</returns>
        public override int Add(Clinic entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Clinic initalization error!");
            }

            this.GetCtx.Set<Clinic>().Add(entity);
            this.GetCtx.SaveChanges();
            return entity.ClinicId;
        }

        /// <summary>
        /// Remove a clinic.
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

            bool contains = false;
            foreach (var item in this.GetCtx.Set<Doctor>())
            {
                if (id == item.ClinicId)
                {
                    contains = true;
                }
            }

            if (!contains)
            {
                this.GetCtx.Set<Clinic>().Remove(entity);
                this.GetCtx.SaveChanges();
            }

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
