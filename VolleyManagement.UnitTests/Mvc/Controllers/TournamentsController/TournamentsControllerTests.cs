﻿namespace VolleyManagement.UnitTests.Mvc.Controllers.TournamentsController
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Contracts;
    using Domain.Tournaments;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;

    using VolleyManagement.Mvc.Controllers;
    using VolleyManagement.Mvc.ViewModels.Tournaments;
    using VolleyManagement.UnitTests.Services.TournamentService;

    /// <summary>
    /// Tests for MVC TournamentController class.
    /// </summary>
    [TestClass]
    public class TournamentsControllerTests
    {
        /// <summary>
        /// Test Fixture
        /// </summary>
        private readonly TournamentServiceTestFixture _testFixture =
            new TournamentServiceTestFixture();

        /// <summary>
        /// Tournaments Service Mock
        /// </summary>
        private readonly Mock<ITournamentService> _tournamentServiceMock =
            new Mock<ITournamentService>();

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
            this._kernel.Bind<ITournamentService>()
                   .ToConstant(this._tournamentServiceMock.Object);
        }

        /// <summary>
        /// Test for Index action. The action should return not empty tournaments list
        /// </summary>
        [TestMethod]
        public void Index_TournamentsExist_TournamentsReturned()
        {
            // Arrange
            var testData = this._testFixture.TestTournaments()
                                       .Build();
            this.MockTournaments(testData);

            var sut = this._kernel.Get<TournamentsController>();

            var expected = new TournamentServiceTestFixture()
                                            .TestTournaments()
                                            .Build()
                                            .ToList();

            // Act
            var result = sut.Index() as ViewResult;

            var actual = (IEnumerable<Tournament>)result.ViewData.Model;

            // Assert
            CollectionAssert.AreEqual(expected, actual.ToList(), new TournamentComparer());
        }

        /// <summary>
        /// Test with negative scenario for Index action.
        /// The action should thrown Argument null exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Index_TournamentsDoNotExist_ExceptionThrown()
        {
            List<Tournament> testData = null;
            this.MockTournaments(testData);
        }

        /// <summary>
        /// Test for Details()
        /// </summary>
        [TestMethod]
        public void Details_TournamentExists_TournamentIsReturned()
        {
            int searchId = 11;
            _tournamentServiceMock.Setup(tr => tr.FindById(It.IsAny<int>()))
                          .Returns(new Tournament
                          {
                              Id = 11,
                              Name = "Tournament 11",
                              Description = "Tournament 11 description",
                              Season = "2014/2015",
                              Scheme = TournamentSchemeEnum.Two,
                              RegulationsLink = "www.Volleyball.dp.ua/Regulations/Tournaments('11')"
                          });
            var tournamentService = this._kernel.Get<TournamentsController>();
            var result = tournamentService.Details(searchId) as ViewResult;
            var actual = (Tournament)result.ViewData.Model;
            var expected = new TournamentBuilder().WithId(searchId).WithName("Tournament 11")
                .WithDescription("Tournament 11 description").WithSeason("2014/2015")
                .WithScheme(TournamentSchemeEnum.Two)
                .WithRegulationsLink("www.Volleyball.dp.ua/Regulations/Tournaments('11')").Build();
            var tournamentComparer = new TournamentComparer();
            var resultofComparation = tournamentComparer.Compare(expected, actual);
            Assert.IsTrue(resultofComparation == 0);
        }

        /// <summary>
        /// Test for Delete tournament action
        /// </summary>
        [TestMethod]
        public void Delete_TournamentExists_TournamentIsDeleted()
        {
            var testData = this._testFixture.TestTournaments()
                                      .Build();
            var tournamentToDelete = testData.Last().Id;
            var tournamentService = _tournamentServiceMock.Object;

            tournamentService.Delete(tournamentToDelete);

            _tournamentServiceMock.Verify(m => m.Delete(tournamentToDelete));
        }

        /// <summary>
        /// Test for Create tournament action (GET)
        /// </summary>
        [TestMethod]
        public void CreateGetAction_TournamentViewModel_ReturnsToTheView()
        {
            // Arrange
            var tournamentsController = this._kernel.Get<TournamentsController>();

            // Act
            var result = tournamentsController.Create() as ViewResult;
            var model = result.ViewData.Model as TournamentViewModel;

            // Assert
            Assert.IsNotNull(model, "Tournament view model should return to the view.");
        }

        /// <summary>
        /// Mocks test data
        /// </summary>
        /// <param name="testData">Data to mock</param>
        private void MockTournaments(IEnumerable<Tournament> testData)
        {
            this._tournamentServiceMock.Setup(tr => tr.GetAll())
                .Returns(testData.AsQueryable());
        }
    }
}