// <copyright file="TreatmentRepository.cs" company="PlaceholderCompany">
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
    /// Treatment's Repository.
    /// </summary>
    public class TreatmentRepository : RepositoryBase<Treatment>, ITreatmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentRepository"/> class.
        /// </summary>
        /// <param name="ctx">DBcontext.</param>
        public TreatmentRepository(HospitalDbContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Change the description of a treatment.
        /// </summary>
        /// <param name="id">Id of the treatment.</param>
        /// <param name="newdesc">The new description.</param>
        public void ChangeDescription(int id, string newdesc)
        {
            var treatment = this.GetOne(id);
            //if (treatment == null)
            //{
            //    throw new InvalidOperationException("Treatment not found!");
            //}

            if (treatment != null)
            {
                treatment.Description = newdesc;
                this.GetCtx.SaveChanges();
            }
        }

        /// <summary>
        /// Get one treatment by id.
        /// </summary>
        /// <param name="id">The id of the treatment.</param>
        /// <returns>A treatment by id.</returns>
        public override Treatment GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.TreatmentId == id);
        }

        /// <summary>
        /// Add a treatment.
        /// </summary>
        /// <param name="entity">Entity of the treatment.</param>
        /// <returns>The Id of the new entity.</returns>
        public override int Add(Treatment entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Treatment initalization error!");
            }

            this.GetCtx.Set<Treatment>().Add(entity);
            this.GetCtx.SaveChanges();
            return entity.TreatmentId;
        }

        /// <summary>
        /// Remove a treatment.
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

            this.GetCtx.Set<Treatment>().Remove(entity);
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
