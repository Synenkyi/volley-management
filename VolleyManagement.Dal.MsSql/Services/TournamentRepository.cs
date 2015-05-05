﻿namespace VolleyManagement.Dal.MsSql.Services
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Linq.Expressions;
    using VolleyManagement.Dal.Contracts;
    using VolleyManagement.Dal.MsSql.Mappers;
    using VolleyManagement.Domain;
    using Dal = VolleyManagement.Dal.MsSql;
    using Domain = VolleyManagement.Domain.Tournaments;

    /// <summary>
    /// Defines implementation of the ITournamentRepository contract.
    /// </summary>
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly ObjectSet<Dal.Tournament> _dalTournaments;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TournamentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dalTournaments = unitOfWork.Context.CreateObjectSet<Dal.Tournament>();
        }

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// Gets all tournaments.
        /// </summary>
        /// <returns>Collection of domain tournaments.</returns>
        public IQueryable<Domain.Tournament> Find()
        {
            return _dalTournaments.Select(t => new Domain.Tournament
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    RegulationsLink = t.RegulationsLink,
                    Scheme = (Domain.TournamentSchemeEnum)t.Scheme,
                    Season = (short)(VolleyManagement.Domain.Constants.Tournament.SCHEMA_VALUE_OFFSET_DOMAIN_TO_DB + t.Season),
                    GamesStart = t.GamesStart,
                    GamesEnd = t.GamesEnd,
                    ApplyingPeriodStart = t.ApplyingPeriodStart,
                    ApplyingPeriodEnd = t.ApplyingPeriodEnd,
                    TransferEnd = t.TransferEnd,
                    TransferStart = t.TransferStart
                });
        }

        /// <summary>
        /// Gets specified collection of tournaments.
        /// </summary>
        /// <param name="predicate">Condition to find tournaments.</param>
        /// <returns>Collection of domain tournaments.</returns>
        public IQueryable<Domain.Tournament> FindWhere(Expression<Func<Domain.Tournament, bool>> predicate)
        {
            return this.Find().Where(predicate);
        }

        /// <summary>
        /// Adds new tournament.
        /// </summary>
        /// <param name="newEntity">The tournament for adding.</param>
        public void Add(Domain.Tournament newEntity)
        {
            Dal.Tournament newTournament = DomainToDal.Map(newEntity);
            _dalTournaments.AddObject(newTournament);
            _unitOfWork.Commit();
            newEntity.Id = newTournament.Id;
        }

        /// <summary>
        /// Updates specified tournament.
        /// </summary>
        /// <param name="oldEntity">The tournament to update.</param>
        public void Update(Domain.Tournament oldEntity)
        {
            var tournamentToUpdate = _dalTournaments.Single(t => t.Id == oldEntity.Id);
            tournamentToUpdate.Name = oldEntity.Name;
            tournamentToUpdate.Description = oldEntity.Description;
            tournamentToUpdate.RegulationsLink = oldEntity.RegulationsLink;
            tournamentToUpdate.Scheme = (byte)oldEntity.Scheme;
            tournamentToUpdate.Season = (byte)(oldEntity.Season
                - VolleyManagement.Domain.Constants.Tournament.SCHEMA_VALUE_OFFSET_DOMAIN_TO_DB);
            tournamentToUpdate.GamesStart = oldEntity.GamesStart;
            tournamentToUpdate.GamesEnd = oldEntity.GamesEnd;
            tournamentToUpdate.ApplyingPeriodStart = oldEntity.ApplyingPeriodStart;
            tournamentToUpdate.ApplyingPeriodEnd = oldEntity.ApplyingPeriodEnd;
            tournamentToUpdate.TransferStart = oldEntity.TransferStart;
            tournamentToUpdate.TransferEnd = oldEntity.TransferEnd;
            _dalTournaments.Context.ObjectStateManager.ChangeObjectState(tournamentToUpdate, EntityState.Modified);
        }

        /// <summary>
        /// Removes tournament by id.
        /// </summary>
        /// <param name="id">The id of tournament to remove.</param>
        public void Remove(int id)
        {
            var dalToRemove = new Dal.Tournament { Id = id };
            _dalTournaments.Attach(dalToRemove);
            _dalTournaments.DeleteObject(dalToRemove);
        }
    }
}
