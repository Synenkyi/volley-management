﻿namespace VolleyManagement.UnitTests.Services.GameService
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;
    using VolleyManagement.Contracts;
    using VolleyManagement.Contracts.Exceptions;
    using VolleyManagement.Data.Contracts;
    using VolleyManagement.Data.Exceptions;
    using VolleyManagement.Data.Queries.Common;
    using VolleyManagement.Data.Queries.GameResult;
    using VolleyManagement.Domain.GamesAggregate;
    using VolleyManagement.Services;

    /// <summary>
    /// Tests for <see cref="GameService"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GameServiceTests
    {
        private const int GAME_RESULT_ID = 1;

        private const int TOURNAMENT_ID = 1;

        private readonly Mock<IGameRepository> _gameRepositoryMock = new Mock<IGameRepository>();

        private readonly Mock<IGameService> _gameServiceMock = new Mock<IGameService>();

        private readonly Mock<IQuery<GameResultDto, FindByIdCriteria>> _getByIdQueryMock
            = new Mock<IQuery<GameResultDto, FindByIdCriteria>>();

        private readonly Mock<IQuery<List<GameResultDto>, TournamentGameResultsCriteria>> _tournamentGameResultsQueryMock
            = new Mock<IQuery<List<GameResultDto>, TournamentGameResultsCriteria>>();

        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        private IKernel _kernel;

        /// <summary>
        /// Initializes test data.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IGameRepository>().ToConstant(_gameRepositoryMock.Object);
            _kernel.Bind<IQuery<GameResultDto, FindByIdCriteria>>().ToConstant(_getByIdQueryMock.Object);
            _kernel.Bind<IQuery<List<GameResultDto>, TournamentGameResultsCriteria>>()
                .ToConstant(_tournamentGameResultsQueryMock.Object);
            _kernel.Bind<IGameService>().ToConstant(_gameServiceMock.Object);
            _gameRepositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        }

        /// <summary>
        /// Test for Create method. GameResult object contains valid data. Game result is created successfully.
        /// </summary>
        [TestMethod]
        public void Create_GameValid_GameCreated()
        {
            // Arrange
            var newGame = new GameBuilder().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Once());
        }

        /// <summary>
        /// Test for Create method. The game result instance is null. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_GameNull_ExceptionThrown()
        {
            // Arrange
            var newGame = null as Game;
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. The home team and the away team are the same. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSameTeams_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithTheSameTeams().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. The final score of the game (sets score) is invalid.
        /// Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameInvalidSetsScore_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithInvalidSetsScore().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. The final score of the game (sets score) does not match set scores.
        /// Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSetsScoreNoMatchSetScores_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithSetsScoreNoMatchSetScores().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. The required set scores are invalid. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameResultInvalidRequiredSetScores_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithInvalidRequiredSetScores().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. The optional set scores are invalid. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameInvalidOptionalSetScores_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithInvalidOptionalSetScores().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. Previous optional set is not played (set score is 0:0). Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GamePreviousOptionalSetUnplayed_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithPreviousOptionalSetUnplayed().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. Set scores are not listed in the correct order. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSetScoresUnordered_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithSetScoresUnorderedForHomeTeam().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Never());
        }

        /// <summary>
        /// Test for Create method. Set scores are not listed in the correct order. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSetScoresUnorderedForAwayTeam_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithSetScoresUnorderedForAwayTeam().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Create method. Game object contains valid data for technical win of away team. Game is created successfully.
        /// </summary>
        [TestMethod]
        public void Create_GameHomeTeamTechnicalWinValidData_GameCreated()
        {
            // Arrange
            var newGame = new GameBuilder().WithTechnicalDefeatValidSetScoresHomeTeamWin().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Once());
        }

        /// <summary>
        /// Test for Create method. Game object contains valid data for technical win of away team. Game is created successfully.
        /// </summary>
        [TestMethod]
        public void Create_GameAwayTeamTechnicalWinValidData_GameCreated()
        {
            // Arrange
            var newGame = new GameBuilder().WithTechnicalDefeatValidSetScoresAwayTeamWin().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);

            // Assert
            VerifyCreateGame(newGame, Times.Once());
        }

        /// <summary>
        /// Test for Create method. Sets score are invalid. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_TechnicalDefeatInvalidSetsScore_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithTechnicalDefeatInvalidSetsScore().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Create method. Set scores are invalid. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameTechnicalDefeatInvalidSetScores_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithTechnicalDefeatInvalidSetScores().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Create method. Set scores are set to optional. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameTechnicalDefeatOptionalSetScore_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithTechnicalDefeatValidOptional().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Create method. Set scores are null. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSetScoresNull_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithSetScoresNull().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Create method. Set scores are invalid. Exception is thrown during creation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_GameSetsScoreInvalid_ExceptionThrown()
        {
            // Arrange
            var newGame = new GameBuilder().WithOrdinarySetsScoreInvalid().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Create(newGame);
        }

        /// <summary>
        /// Test for Get method. Existing game is requested. Game is returned.
        /// </summary>
        [TestMethod]
        public void Get_ExistingGame_GameReturned()
        {
            // Arrange
            var expected = new GameResultDtoBuilder().WithId(GAME_RESULT_ID).Build();
            var sut = _kernel.Get<GameService>();

            SetupGet(expected);

            // Act
            var actual = sut.Get(GAME_RESULT_ID);

            // Assert
            TestHelper.AreEqual(expected, actual, new GameResultDtoComparer());
        }

        /// <summary>
        /// Test for GetTournamentResults method. Game results of specified tournament are requested. Game results are returned.
        /// </summary>
        [TestMethod]
        public void GetTournamentResults_GameResultsRequsted_GameResultsReturned()
        {
            // Arrange
            var expected = new GameServiceTestFixture().TestGameResults().Build();
            var sut = _kernel.Get<GameService>();

            SetupGetTournamentResults(TOURNAMENT_ID, expected);

            // Act
            var actual = sut.GetTournamentResults(TOURNAMENT_ID);

            // Assert
            CollectionAssert.AreEqual(expected, actual, new GameResultDtoComparer());
        }

        /// <summary>
        /// Test for Edit method. Game object contains valid data. Game is edited successfully.
        /// </summary>
        [TestMethod]
        public void Edit_GameValid_GameEdited()
        {
            // Arrange
            var game = new GameBuilder().Build();
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Edit(game);

            // Assert
            VerifyEditGame(game, Times.Once());
        }

        /// <summary>
        /// Test for Edit method. Game is missing and cannot be edited. Exception is thrown during editing.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MissingEntityException))]
        public void Edit_MissingGame_ExceptionThrown()
        {
            // Arrange
            var game = new GameBuilder().Build();
            var sut = _kernel.Get<GameService>();

            SetupEditMissingEntityException(game);

            // Act
            sut.Edit(game);

            // Assert
            VerifyEditGame(game, Times.Once(), Times.Never());
        }

        /// <summary>
        /// Test for Delete method. Existing game has to be deleted. Game is deleted.
        /// </summary>
        [TestMethod]
        public void Delete_ExistingGame_GameDeleted()
        {
            // Arrange
            var sut = _kernel.Get<GameService>();

            // Act
            sut.Delete(GAME_RESULT_ID);

            // Assert
            VerifyDeleteGame(GAME_RESULT_ID, Times.Once());
        }

        private bool AreGamesEqual(Game x, Game y)
        {
            return new GameComparer().Compare(x, y) == 0;
        }

        private void SetupGet(GameResultDto gameResult)
        {
            _getByIdQueryMock.Setup(m => m.Execute(It.Is<FindByIdCriteria>(c => c.Id == gameResult.Id))).Returns(gameResult);
        }

        private void SetupGetTournamentResults(int tournamentId, List<GameResultDto> gameResults)
        {
            _tournamentGameResultsQueryMock.Setup(m =>
                m.Execute(It.Is<TournamentGameResultsCriteria>(c => c.TournamentId == tournamentId)))
                .Returns(gameResults);
        }

        private void SetupEditMissingEntityException(Game game)
        {
            _gameRepositoryMock.Setup(m =>
                m.Update(It.Is<Game>(grs => AreGamesEqual(grs, game))))
                .Throws(new ConcurrencyException());
        }

        private void VerifyCreateGame(Game game, Times times)
        {
            _gameRepositoryMock.Verify(
                m => m.Add(It.Is<Game>(grs => AreGamesEqual(grs, game))), times);
            _unitOfWorkMock.Verify(m => m.Commit(), times);
        }

        private void VerifyEditGame(Game game, Times times)
        {
            _gameRepositoryMock.Verify(
                m => m.Update(It.Is<Game>(grs => AreGamesEqual(grs, game))), times);
            _unitOfWorkMock.Verify(m => m.Commit(), times);
        }

        private void VerifyEditGame(Game game, Times repositoryTimes, Times unitOfWorkTimes)
        {
            _gameRepositoryMock.Verify(
                m => m.Update(It.Is<Game>(grs => AreGamesEqual(grs, game))), repositoryTimes);
            _unitOfWorkMock.Verify(m => m.Commit(), unitOfWorkTimes);
        }

        private void VerifyDeleteGame(int gameResultId, Times times)
        {
            _gameRepositoryMock.Verify(m => m.Remove(It.Is<int>(id => id == gameResultId)), times);
            _unitOfWorkMock.Verify(m => m.Commit(), times);
        }
    }
}
