﻿namespace VolleyManagement.Data.MsSql.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    using VolleyManagement.Crosscutting.Contracts.Specifications;
    using VolleyManagement.Crosscutting.Specifications;
    using VolleyManagement.Data.Contracts;
    using VolleyManagement.Data.Exceptions;
    using VolleyManagement.Data.MsSql.Entities;
    using VolleyManagement.Data.MsSql.Mappers;
    using VolleyManagement.Data.MsSql.Repositories.Specifications;
    using VolleyManagement.Domain.TournamentsAggregate;

    /// <summary>
    /// Defines implementation of the ITournamentRepository contract.
    /// </summary>
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly DbSet<TournamentEntity> _dalTournaments;
        private readonly VolleyUnitOfWork _unitOfWork;

        private readonly ISpecification<TournamentEntity> _dbStorageSpecification
            = new TournamentsStorageSpecification();

        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TournamentRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (VolleyUnitOfWork)unitOfWork;
            this._dalTournaments = _unitOfWork.Context.Tournaments;
        }

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        /// <summary>
        /// Gets all tournaments.
        /// </summary>
        /// <returns>Collection of domain tournaments.</returns>
        public IQueryable<Tournament> Find()
        {
            return this._dalTournaments.Select(t => new Tournament
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    RegulationsLink = t.RegulationsLink,
                    Scheme = (TournamentSchemeEnum)t.Scheme,
                    Season = (short)(ValidationConstants.Tournament.SCHEMA_STORAGE_OFFSET + t.Season),
                    GamesStart = t.GamesStart,
                    GamesEnd = t.GamesEnd,
                    ApplyingPeriodStart = t.ApplyingPeriodStart,
                    ApplyingPeriodEnd = t.ApplyingPeriodEnd,
                    TransferEnd = t.TransferEnd,
                    TransferStart = t.TransferStart
                }).ToArray().AsQueryable();
        }

        /// <summary>
        /// Gets specified collection of tournaments.
        /// </summary>
        /// <param name="predicate">Condition to find tournaments.</param>
        /// <returns>Collection of domain tournaments.</returns>
        public IQueryable<Tournament> FindWhere(
            Expression<Func<Tournament, bool>> predicate)
        {
            return this.Find().Where(predicate);
        }

        /// <summary>
        /// Adds new tournament.
        /// </summary>
        /// <param name="newModel">The tournament for adding.</param>
        public void Add(Tournament newModel)
        {
            var tournament = new TournamentEntity();
            DomainToDal.Map(tournament, newModel);
            if (!_dbStorageSpecification.IsSatisfiedBy(tournament))
            {
                throw new InvalidEntityException();
            }

            this._dalTournaments.Add(tournament);
            this._unitOfWork.Commit();
            newModel.Id = tournament.Id;
        }

        /// <summary>
        /// Updates specified tournament.
        /// </summary>
        /// <param name="updatedModel">Updated tournament.</param>
        public void Update(Tournament updatedModel)
        {
            var tournamentToUpdate = this._dalTournaments.Single(t => t.Id == updatedModel.Id);
            DomainToDal.Map(tournamentToUpdate, updatedModel);

            // ToDo: Check Do we really need this?
            //// this._dalTournaments.Context.ObjectStateManager.ChangeObjectState(tournamentToUpdate, EntityState.Modified);
        }

        /// <summary>
        /// Removes tournament by id.
        /// </summary>
        /// <param name="id">The id of tournament to remove.</param>
        public void Remove(int id)
        {
            var dalToRemove = new Entities.TournamentEntity { Id = id };
            this._dalTournaments.Attach(dalToRemove);
            this._dalTournaments.Remove(dalToRemove);
        }
    }
}
