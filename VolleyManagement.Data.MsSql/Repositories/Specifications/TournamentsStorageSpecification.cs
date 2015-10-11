﻿namespace VolleyManagement.Data.MsSql.Repositories.Specifications
{
    using VolleyManagement.Crosscutting.Specifications;
    using VolleyManagement.Data.MsSql.Entities;

    /// <summary>
    /// Provides specification for storing Tournaments in the DB
    /// </summary>
    public class TournamentsStorageSpecification : ISpecification<TournamentEntity>
    {
        /// <summary>
        /// Verifies that entity matches specification
        /// </summary>
        /// <param name="entity"> The entity to test </param>
        /// <returns> Results of the match </returns>
        public bool IsSatisfiedBy(TournamentEntity entity)
        {
            var name = new ExpressionSpecification<TournamentEntity>(
                                t => !string.IsNullOrEmpty(t.Name)
                                  && t.Name.Length <= ValidationConstants.Tournament.MAX_NAME_LENGTH);
            var description = new ExpressionSpecification<TournamentEntity>(
                                t => t.Description == null
                                  || t.Description.Length <= ValidationConstants.Tournament.MAX_DESCRIPTION_LENGTH);
            var link = new ExpressionSpecification<TournamentEntity>(
                                t => t.RegulationsLink == null
                                  || t.RegulationsLink.Length <= ValidationConstants.Tournament.MAX_URL_LENGTH);


            return name.And(description)
                       .And(link)
                       .IsSatisfiedBy(entity);
        }
    }
}