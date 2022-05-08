// <copyright file="DoctorRepository.cs" company="PlaceholderCompany">
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
    /// Doctor's Repository.
    /// </summary>
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorRepository"/> class.
        /// </summary>
        /// <param name="ctx">DBcontext.</param>
        public DoctorRepository(HospitalDbContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Change the degree of a doctor.
        /// </summary>
        /// <param name="id">Id of the doctor.</param>
        /// <param name="newdegree">The new degree.</param>
        public void ChangeDegree(int id, string newdegree)
        {
            var doctor = this.GetOne(id);
            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor not found!");
            }

            doctor.Degree = newdegree;
            this.GetCtx.SaveChanges();
        }

        /// <summary>
        /// Get one doctor by id.
        /// </summary>
        /// <param name="id">The id of the doctor.</param>
        /// <returns>A doctor by id.</returns>
        public override Doctor GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.DoctorId == id);
        }

        /// <summary>
        /// Add a doctor.
        /// </summary>
        /// <param name="entity">Entity of the doctor.</param>
        /// <returns>The Id of the new entity.</returns>
        public override int Add(Doctor entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Doctor initalization error!");
            }

            this.GetCtx.Set<Doctor>().Add(entity);
            this.GetCtx.SaveChanges();
            return entity.DoctorId;
        }

        /// <summary>
        /// Remove a Doctor.
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
            foreach (var pitem in this.GetCtx.Set<Patient>())
            {
                if (id == pitem.DoctorId)
                {
                    contains = true;
                }
            }

            foreach (var titem in this.GetCtx.Set<Treatment>())
            {
                if (id == titem.DoctorId)
                {
                    contains = true;
                }
            }

            if (!contains)
            {
                this.GetCtx.Set<Doctor>().Remove(entity);
                try
                {
                    this.GetCtx.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    Console.WriteLine(new string("You are too fast"));
                }
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
