﻿namespace VolleyManagement.Data.Queries.Tournaments
{
    using VolleyManagement.Data.Contracts;

    /// <summary>
    /// Search by name criterion
    /// </summary>
    public class FirstByNameCriterion : IQueryCriterion
    {
        /// <summary>
        /// Gets or sets Name to search
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Id of the entity for Update operation
        /// </summary>
        public int? EntityId { get; set; }
    }
}