﻿namespace VolleyManagement.UnitTests.Services.TournamentsService
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Ninject;

    using VolleyManagement.Dal.Contracts;
    using VolleyManagement.Domain.Tournaments;
    using VolleyManagement.Services;

    /// <summary>
    /// Tests for TournamentService class.
    /// </summary>
    [TestClass]
    public class TournamentServiceTests
    {
        /// <summary>
        /// Test Fixture
        /// </summary>
        private readonly TournamentServiceTestFixture _testFixture = new TournamentServiceTestFixture();

        /// <summary>
        /// Tournaments Repository Mock
        /// </summary>
        private readonly Mock<ITournamentRepository> _tournamentRepositoryMock = new Mock<ITournamentRepository>();

        /// <summary>
        /// IoC for tests
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// Initializes test data
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this._kernel = new StandardKernel();
            this._kernel.Bind<ITournamentRepository>()
                   .ToConstant(this._tournamentRepositoryMock.Object);
        }

        /// <summary>
        /// Test for GetAll() method. The method should return existing tournaments
        /// (order is important).
        /// </summary>
        [TestMethod]
        public void GetAll_TournamentsExist_TournamentsReturned()
        {
            // Arrange
            var testData = this._testFixture.TestTournaments()
                                       .Build();
            this.MockTournaments(testData);

            // sut - stands for System Under Test
            var sut = this._kernel.Get<TournamentService>();

            // Expected result
            var expected = new TournamentServiceTestFixture()
                                            .TestTournaments()
                                            .Build()
                                            .ToList();

            // Actual result
            var actual = sut.GetAll().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual, new TournamentComparer());
        }

        /// <summary>
        /// Mocks test data
        /// </summary>
        /// <param name="testData">Data to mock</param>
        private void MockTournaments(IEnumerable<Tournament> testData)
        {
            this._tournamentRepositoryMock.Setup(tr => tr.FindAll()).Returns(testData.AsQueryable());
        }
    }
}