﻿namespace VolleyManagement.UnitTests.Services.GameService
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using VolleyManagement.Domain.GamesAggregate;

    /// <summary>
    /// Represents a builder of <see cref="Game"/> objects for unit tests for <see cref="GameService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GameBuilder
    {
        #region Fields

        private Game _game;

        private const string DATE = "2016-04-03 10:00";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBuilder"/> class.
        /// </summary>
        public GameBuilder()
        {
            _game = new Game
            {
                Id = 1,
                TournamentId = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                Result = new Result
                {
                    SetsScore = new Score(3, 0),
                    IsTechnicalDefeat = false,
                    SetScores = new List<Score>
                    {
                        new Score(25, 20),
                        new Score(26, 24),
                        new Score(30, 28),
                        new Score(0, 0),
                        new Score(0, 0)
                    }
                },
                GameDate = DateTime.Parse(DATE),
                Round = 1
            };
        }

        #endregion

        #region Main setter methods

        /// <summary>
        /// Sets the identifier of the game.
        /// </summary>
        /// <param name="id">Identifier of the game.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithId(int id)
        {
            _game.Id = id;
            return this;
        }

        /// <summary>
        /// Sets the identifier of the tournament where game belongs.
        /// </summary>
        /// <param name="id">Identifier of the tournament.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithTournamentId(int id)
        {
            _game.TournamentId = id;
            return this;
        }

        /// <summary>
        /// Sets the identifier of the home team which played the game.
        /// </summary>
        /// <param name="id">Identifier of the home team.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithHomeTeamId(int id)
        {
            _game.HomeTeamId = id;
            return this;
        }

        /// <summary>
        /// Sets the identifier of the away team which played the game.
        /// </summary>
        /// <param name="id">Identifier of the away team.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithAwayTeamId(int id)
        {
            _game.AwayTeamId = id;
            return this;
        }

        /// <summary>
        /// Sets the final score of the game.
        /// </summary>
        /// <param name="score">Final score of the game.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithSetsScore(Score score)
        {
            _game.Result.SetsScore = score;
            return this;
        }

        /// <summary>
        /// Sets a value of technical defeat to true.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithTechnicalDefeat()
        {
            _game.Result.IsTechnicalDefeat = true;
            return this;
        }

        /// <summary>
        /// Sets a value of technical defeat to false.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithNoTechnicalDefeat()
        {
            _game.Result.IsTechnicalDefeat = false;
            return this;
        }

        /// <summary>
        /// Sets set score by the specified set number.
        /// </summary>
        /// <param name="setNumber">Set number.</param>
        /// <param name="score">Set score.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithSetScore(byte setNumber, Score score)
        {
            _game.Result.SetScores[setNumber - 1] = score;
            return this;
        }

        /// <summary>
        /// Sets the set scores of the game.
        /// </summary>
        /// <param name="scores">Set scores.</param>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithSetScores(IEnumerable<Score> scores)
        {
            _game.Result.SetScores.Clear();
            _game.Result.SetScores.AddRange(scores);
            return this;
        }

        /// <summary>
        /// Builds instance of <see cref="GameBuilder"/>.
        /// </summary>
        /// <returns>Instance of <see cref="Game"/>.</returns>
        public Game Build()
        {
            return _game;
        }

        #endregion

        #region Helper setter methods

        /// <summary>
        /// Sets the same home and away teams.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithTheSameTeams()
        {
            _game.HomeTeamId = 1;
            _game.AwayTeamId = 1;
            return this;
        }

        /// <summary>
        /// Sets the final score of the game in a way that it is invalid.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithInvalidSetsScore()
        {
            _game.Result.SetsScore = new Score(1, 0);
            return this;
        }

        /// <summary>
        /// Sets the final score of the game in a way that it does not match set scores.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithSetsScoreNoMatchSetScores()
        {
            _game.Result.SetsScore = new Score(3, 1);
            _game.Result.SetScores = new List<Score>
            {
                new Score(25, 20),
                new Score(24, 26),
                new Score(28, 30),
                new Score(25, 22),
                new Score(27, 25)
            };

            return this;
        }

        /// <summary>
        /// Sets the required set scores in a way that they are invalid.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithInvalidRequiredSetScores()
        {
            _game.Result.SetScores = new List<Score>
            {
                new Score(10, 0),
                new Score(10, 0),
                new Score(10, 0),
                new Score(0, 0),
                new Score(0, 0)
            };

            return this;
        }

        /// <summary>
        /// Sets the optional set scores in a way that they are invalid.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithInvalidOptionalSetScores()
        {
            _game.Result.SetsScore = new Score(3, 2);
            _game.Result.SetScores = new List<Score>
            {
                new Score(25, 20),
                new Score(24, 26),
                new Score(28, 30),
                new Score(10, 0),
                new Score(10, 0)
            };

            return this;
        }

        /// <summary>
        /// Sets the previous optional set score to 0:0.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithPreviousOptionalSetUnplayed()
        {
            _game.Result.SetsScore = new Score(3, 1);
            _game.Result.SetScores = new List<Score>
            {
                new Score(25, 23),
                new Score(20, 25),
                new Score(25, 21),
                new Score(0, 0),
                new Score(25, 17)
            };

            return this;
        }

        /// <summary>
        /// Sets the set scores in a way that they are not listed in the correct order.
        /// </summary>
        /// <returns>Instance of <see cref="GameBuilder"/>.</returns>
        public GameBuilder WithSetScoresUnordered()
        {
            _game.Result.SetsScore = new Score(3, 1);
            _game.Result.SetScores = new List<Score>
            {
                new Score(25, 20),
                new Score(25, 21),
                new Score(25, 18),
                new Score(23, 25),
                new Score(0, 0)
            };

            return this;
        }

        #endregion
    }
}
