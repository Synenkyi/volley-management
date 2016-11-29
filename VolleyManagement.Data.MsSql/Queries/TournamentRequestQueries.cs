﻿namespace VolleyManagement.Data.MsSql.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using VolleyManagement.Data.Contracts;
    using VolleyManagement.Data.MsSql.Entities;
    using VolleyManagement.Data.Queries.Common;
    using VolleyManagement.Data.Queries.TournamentRequest;
    using VolleyManagement.Domain.TournamentRequestAggregate;

    /// <summary>
    /// Provides Object Query implementation for Tournament Requests
    /// </summary>
    public class TournamentRequestQueries : IQuery<List<TournamentRequest>, GetAllCriteria>,
                                            IQuery<TournamentRequest, FindByIdCriteria>,
                                            IQuery<TournamentRequest, FindByTeamTournamentCriteria>
    {
        #region Fields

        private readonly VolleyUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentRequestQueries"/> class.
        /// </summary>
        /// <param name="unitOfWork"> The unit of work. </param>
        public TournamentRequestQueries(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (VolleyUnitOfWork)unitOfWork;
        }

        #endregion

        #region Implemenations

        /// <summary>
        /// Finds Requests by given criteria
        /// </summary>
        /// <param name="criteria"> The criteria. </param>
        /// <returns> The list <see cref="TournamentRequest"/>. </returns>
        public List<TournamentRequest> Execute(GetAllCriteria criteria)
        {
            return _unitOfWork.Context.TournamentRequests.Select(GetRequestMapping()).ToList();
        }

        /// <summary>
        /// Finds Requests by given criteria
        /// </summary>
        /// <param name="criteria"> The criteria. </param>
        /// <returns> The <see cref="TournamentRequest"/>. </returns>
        public TournamentRequest Execute(FindByIdCriteria criteria)
        {
            return _unitOfWork.Context.TournamentRequests
                                      .Where(r => r.Id == criteria.Id)
                                      .Select(GetRequestMapping())
                                      .SingleOrDefault();
        }

        /// <summary>
        /// Finds Requests by given criteria
        /// </summary>
        /// <param name="criteria"> The criteria. </param>
        /// <returns> The <see cref="TournamentRequest"/>. </returns>
        public TournamentRequest Execute(FindByTeamTournamentCriteria criteria)
        {
            return _unitOfWork.Context.TournamentRequests
                                      .Where(r => r.TeamId == criteria.TeamId)
                                      .Where(r => r.TournamentId == criteria.TournamentId)
                                      .Select(GetRequestMapping())
                                      .SingleOrDefault();
        }

        #endregion

        #region Mapping

        private static Expression<Func<TournamentRequestEntity, TournamentRequest>> GetRequestMapping()
        {
            return
                t =>
                new TournamentRequest()
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    TeamId = t.TeamId,
                    TournamentId = t.TournamentId
                };
        }

        #endregion
    }
}
