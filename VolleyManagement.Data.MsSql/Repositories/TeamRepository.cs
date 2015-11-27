﻿namespace VolleyManagement.Data.MsSql.Repositories
{
    using System;
    using System.Data.Entity;

    using VolleyManagement.Data.Contracts;
    using VolleyManagement.Data.Exceptions;
    using VolleyManagement.Data.MsSql.Entities;
    using VolleyManagement.Data.MsSql.Mappers;
    using VolleyManagement.Data.MsSql.Repositories.Specifications;
    using VolleyManagement.Domain.TeamsAggregate;

    /// <summary>
    /// Defines implementation of the ITeamRepository contract.
    /// </summary>
    internal class TeamRepository : ITeamRepository
    {
        private static readonly TeamStorageSpecification _dbStorageSpecification
            = new TeamStorageSpecification();

        private readonly DbSet<TeamEntity> _dalTeams;

        private readonly VolleyUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TeamRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (VolleyUnitOfWork)unitOfWork;
            this._dalTeams = _unitOfWork.Context.Teams;
        }

        /// <summary>
        /// Gets unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        /// <summary>
        /// Adds new team.
        /// </summary>
        /// <param name="newModel">The team for adding.</param>
        public void Add(Team newModel)
        {
            var newTeam = new TeamEntity();
            DomainToDal.Map(newTeam, newModel);

            if (!_dbStorageSpecification.IsSatisfiedBy(newTeam))
            {
                throw new InvalidEntityException();
            }

            this._dalTeams.Add(newTeam);
            this._unitOfWork.Commit();

            newModel.Id = newTeam.Id;
        }

        /// <summary>
        /// Updates specified team.
        /// </summary>
        /// <param name="updatedModel">Updated team.</param>
        public void Update(Team updatedModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes team by id.
        /// </summary>
        /// <param name="id">The id of team to remove.</param>
        public void Remove(int id)
        {
            var dalToRemove = new TeamEntity { Id = id };
            this._dalTeams.Attach(dalToRemove);
            this._dalTeams.Remove(dalToRemove);
        }
    }
}
