﻿namespace VolleyManagement.UnitTests.Services.TeamService
{
    using System.Diagnostics.CodeAnalysis;
    using Domain.TeamsAggregate;

    /// <summary>
    /// Team domain object builder
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class TeamBuilder
    {
        /// <summary>
        /// Holds test player instance
        /// </summary>
        private Team _team;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamBuilder"/> class
        /// </summary>
        public TeamBuilder()
        {
            _team = new Team
            {
                Id = 1,
                Name = "TeamNameA",
                CaptainId = 1,
                Coach = "TeamCoachA",
                Achievements = "TeamAchievementsA"
            };
        }

        /// <summary>
        /// Sets team test Id
        /// </summary>
        /// <param name="id">Test team Id</param>
        /// <returns>Team builder object</returns>
        public TeamBuilder WithId(int id)
        {
            _team.Id = id;
            return this;
        }

        /// <summary>
        /// Sets team test first name
        /// </summary>
        /// <param name="name">Test team name</param>
        /// <returns>Team builder object</returns>
        public TeamBuilder WithName(string name)
        {
            _team.Name = name;
            return this;
        }

        /// <summary>
        /// Sets team test coach
        /// </summary>
        /// <param name="coach">Test team coach</param>
        /// <returns>Team builder object</returns>
        public TeamBuilder WithCoach(string coach)
        {
            _team.Coach = coach;
            return this;
        }

        /// <summary>
        /// Sets team test achievements
        /// </summary>
        /// <param name="achievements">Test team achievements</param>
        /// <returns>Team builder object</returns>
        public TeamBuilder WithAchievements(string achievements)
        {
            _team.Achievements = achievements;
            return this;
        }

        /// <summary>
        /// Sets team test captain
        /// </summary>
        /// <param name="captainId">Test team captain</param>
        /// <returns>Team builder object</returns>
        public TeamBuilder WithCaptain(int captainId)
        {
            _team.CaptainId = captainId;
            return this;
        }

        /// <summary>
        /// Builds test team
        /// </summary>
        /// <returns>Test team</returns>
        public Team Build()
        {
            return _team;
        }
    }
}
