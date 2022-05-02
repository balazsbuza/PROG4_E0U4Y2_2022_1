// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hospital.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Hospital.Data.Tables;

    /// <summary>
    /// IRepository interface creation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>One entity.</returns>
        T GetOne(int id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Returns all entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity">Id of the entity.</param>
        /// <returns>The Id of the new entity.</returns>
        int Add(T entity);

        /// <summary>
        /// Remove an entity.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>True if the entity got removed.</returns>
        bool Remove(int id);
    }
}
