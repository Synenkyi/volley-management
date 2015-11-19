﻿namespace VolleyManagement.UnitTests.Services.TournamentService
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using VolleyManagement.Crosscutting.Contracts.Providers;
    using VolleyManagement.Domain.TournamentsAggregate;

    /// <summary>
    /// Builder for test tournaments
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class TournamentBuilder
    {
        public const int TRANSFER_PERIOD_DAYS = 10;

        public const int TRANSFER_PERIOD_MONTH = 6;

        public const int TOURNAMENT_DEFAULT_ID = 1;

        /// <summary>
        /// Holds test tournament instance
        /// </summary>
        private Tournament _tournament;

        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentBuilder"/> class
        /// </summary>
        public TournamentBuilder()
        {
            this._tournament = new Tournament
            {
                Id = TOURNAMENT_DEFAULT_ID,
                Name = "Name",
                Description = "Description 1",
                Season = 2014,
                Scheme = TournamentSchemeEnum.Two,
                RegulationsLink = "http://default.com",
                ApplyingPeriodStart = new DateTime(2015, 06, 02),
                ApplyingPeriodEnd = new DateTime(2015, 09, 02),
                GamesStart = new DateTime(2015, 09, 03),
                GamesEnd = new DateTime(2015, 12, 03),
                TransferStart = new DateTime(2015, 10, 01),
                TransferEnd = new DateTime(2015, 11, 01),
                Divisions = this.Divisions
            };
        }

        private List<Division> Divisions
        {
            get
            {
                return new List<Division>
                {
                    new Division() { Id = 1, TournamentId = TOURNAMENT_DEFAULT_ID, Name = "Division 1" },
                    new Division() { Id = 2, TournamentId = TOURNAMENT_DEFAULT_ID, Name = "Division 2" }
                };
            }
        }

        /// <summary>
        /// Sets id of test tournament
        /// </summary>
        /// <param name="id">Id for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithId(int id)
        {
            this._tournament.Id = id;
            return this;
        }

        /// <summary>
        /// Sets name of test tournament
        /// </summary>
        /// <param name="name">Name for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithName(string name)
        {
            this._tournament.Name = name;
            return this;
        }

        /// <summary>
        /// Sets description of test tournament
        /// </summary>
        /// <param name="description">Description for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithDescription(string description)
        {
            this._tournament.Description = description;
            return this;
        }

        /// <summary>
        /// Sets scheme of test tournament
        /// </summary>
        /// <param name="scheme">Scheme for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithScheme(TournamentSchemeEnum scheme)
        {
            this._tournament.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Sets season of test tournament
        /// </summary>
        /// <param name="season">Season for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithSeason(short season)
        {
            this._tournament.Season = season;
            return this;
        }

        /// <summary>
        /// Sets regulations link of test tournament
        /// </summary>
        /// <param name="regulationsLink">Regulations link for test tournament</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithRegulationsLink(string regulationsLink)
        {
            this._tournament.RegulationsLink = regulationsLink;
            return this;
        }

        /// <summary>
        /// Sets tournament start
        /// </summary>
        /// <param name="gamesStart">Games start</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithGamesStart(DateTime gamesStart)
        {
            this._tournament.GamesStart = gamesStart;
            return this;
        }

        /// <summary>
        /// Sets tournament end
        /// </summary>
        /// <param name="gamesEnd">Games end</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithGamesEnd(DateTime gamesEnd)
        {
            this._tournament.GamesEnd = gamesEnd;
            return this;
        }

        /// <summary>
        /// Sets applying start date of a tournament
        /// </summary>
        /// <param name="applyingPeriodStart">Applying period start</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithApplyingPeriodStart(DateTime applyingPeriodStart)
        {
            this._tournament.ApplyingPeriodStart = applyingPeriodStart;
            return this;
        }

        /// <summary>
        /// Sets applying end date of a tournament
        /// </summary>
        /// <param name="applyingPeriodEnd">Applying period end</param>
        /// <returns>Tournament builder object</returns>
        public TournamentBuilder WithApplyingPeriodEnd(DateTime applyingPeriodEnd)
        {
            this._tournament.ApplyingPeriodEnd = applyingPeriodEnd;
            return this;
        }

        /// <summary>
        /// Sets tournament transfer start date to a specified date.
        /// </summary>
        /// <param name="transferStart">Date of transfer start.</param>
        /// <returns>Instance of Tournament builder.</returns>
        public TournamentBuilder WithTransferStart(DateTime? transferStart)
        {
            this._tournament.TransferStart = transferStart;
            return this;
        }

        /// <summary>
        /// Sets tournament transfer end date to a specified date.
        /// </summary>
        /// <param name="transferEnd">Date of transfer end.</param>
        /// <returns>Instance of Tournament builder.</returns>
        public TournamentBuilder WithTransferEnd(DateTime? transferEnd)
        {
            this._tournament.TransferEnd = transferEnd;
            return this;
        }

        /// <summary>
        /// Sets tournament transfer start date and end date to null.
        /// </summary>
        /// <returns>Instance of Tournament builder.</returns>
        public TournamentBuilder WithNoTransferPeriod()
        {
            this._tournament.TransferStart = null;
            this._tournament.TransferEnd = null;
            return this;
        }

        /// <summary>
        /// Set divisions list
        /// </summary>
        /// <param name="divisions">Divisions list</param>
        /// <returns>Test tournament</returns>
        public TournamentBuilder WithDivisions(List<Division> divisions)
        {
            this._tournament.Divisions = divisions;
            return this;
        }

        /// <summary>
        /// Builds test tournament
        /// </summary>
        /// <returns>Test tournament</returns>
        public Tournament Build()
        {
            return this._tournament;
        }
    }
}
