﻿namespace VolleyManagement.UnitTests.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Contracts;
    using Domain.Players;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;
    using VolleyManagement.UI.Areas.Mvc.Controllers;
    using VolleyManagement.UnitTests.Mvc.ViewModels;
    using VolleyManagement.UI.Areas.Mvc.ViewModels.Players;
    using VolleyManagement.UnitTests.Services.PlayerService;

    /// <summary>
    /// Tests for MVC PlayersController class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PlayersControllerTests
    {
        private const int NUMBER_OF_PLAYERS_FOR_MOCK = 12;
        private const int FIRST_ASCII_LETTER = 65;
        private const int LAST_ASCII_LETTER = 90;
        private const int MAX_PLAYERS_ON_PAGE = 10;
        private const int TESTING_PAGE = 1;

        private readonly Mock<IPlayerService> _playerServiceMock = new Mock<IPlayerService>();
        private IKernel _kernel;

        /// <summary>
        /// Initializes test data
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            this._kernel = new StandardKernel();
            this._kernel.Bind<IPlayerService>()
                   .ToConstant(this._playerServiceMock.Object);
        }

        /// <summary>
        /// Test for Index action. The action should return not empty ordering page with players
        /// </summary>
        [TestMethod]
        public void Index_PlayersExist_PlayersPageReturned()
        {
            // Arrange
            List<Player> currectList = new List<Player>();
            Random rand = new Random();

            for (int i = 0; i < NUMBER_OF_PLAYERS_FOR_MOCK; i++)
            {
                char lastName = (char)rand.Next(FIRST_ASCII_LETTER, LAST_ASCII_LETTER + 1);
                currectList.Add(new PlayerBuilder()
                    .WithId(i)
                    .WithLastName(lastName.ToString())
                    .Build());
            }

            _playerServiceMock.Setup(tr => tr.Get()).Returns(currectList.AsQueryable());

            var sut = this._kernel.Get<PlayersController>();
            var expected = currectList.OrderBy(p => p.LastName)
                .Skip((TESTING_PAGE - 1) * MAX_PLAYERS_ON_PAGE)
                .Take(MAX_PLAYERS_ON_PAGE)
                .Select(p => 
                    new PlayerViewModel
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        BirthYear = p.BirthYear,
                        Height = p.Height,
                        Weight = p.Weight
                    })
                .ToList();

            // Act
            var actual = TestExtensions.GetModel<PlayersListViewModel>(sut.Index(TESTING_PAGE)).List;

            // Assert
            CollectionAssert.AreEqual(expected, actual, new PlayerViewModelComparer());
        }

        /// <summary>
        /// Test with negative scenario for Index action.
        /// The action should thrown Argument null exception
        /// </summary>
        [TestMethod]
        public void Index_PlayersDoNotExist_ExceptionThrown()
        {
            // Arrange
            this._playerServiceMock.Setup(tr => tr.Get())
                .Throws(new ArgumentNullException());

            var sut = this._kernel.Get<PlayersController>();
            var expected = (int)HttpStatusCode.NotFound;

            // Act
            var actual = (sut.Index(It.IsAny<int>()) as HttpNotFoundResult).StatusCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
