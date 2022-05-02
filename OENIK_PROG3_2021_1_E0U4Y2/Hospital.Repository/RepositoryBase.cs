// <copyright file="RepositoryBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Repository
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Abstract class for the specific repositories.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Creating DBContext.
        /// </summary>
        private DbContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="ctx">The DBContext.</param>
        protected RepositoryBase(DbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// Gets DBContext.
        /// </summary>
        /// <returns>DBContext.</returns>
        public DbContext GetCtx => this.ctx;

        /// <summary>
        /// Get all entities from a table.
        /// </summary>
        /// <returns>All entities from a table.</returns>
        public IQueryable<T> GetAll()
        {
            return this.ctx.Set<T>();
        }

        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>One entity.</returns>
        public abstract T GetOne(int id);

        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity">Entity of something.</param>
        /// <returns>The Id of the new entity.</returns>
        public abstract int Add(T entity);

        /// <summary>
        /// Remove an entity.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        public abstract bool Remove(int id);
    }
}
