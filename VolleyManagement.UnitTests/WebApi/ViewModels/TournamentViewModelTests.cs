﻿namespace VolleyManagement.UnitTests.WebApi.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.TournamentService;
    using VolleyManagement.Domain.Tournaments;
    using VolleyManagement.UI.Areas.WebApi.ViewModels.Tournaments;
    using VolleyManagement.UnitTests.WebApi.ViewModels;

    /// <summary>
    ///  Is correctly TournamentViewModel mapped to domain and back tests
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TournamentViewModelTests
    {
        /// <summary>
        /// Test for Map() method.
        /// The method should map tournament domain model to view model.
        /// </summary>
        [TestMethod]
        public void Map_TournamentAsParam_MappedToViewModelWebApi()
        {
            // Arrange
            var tournament = new TournamentBuilder()
                                        .WithId(1)
                                        .WithName("test")
                                        .WithDescription("Volley")
                                        .WithScheme(TournamentSchemeEnum.Two)
                                        .WithSeason("2016/2017")
                                        .WithRegulationsLink("volley.dp.ua")
                                        .Build();
            var expected = new TournamentViewModelBuilder()
                                        .WithId(1)
                                        .WithName("test")
                                        .WithDescription("Volley")
                                        .WithScheme("2")
                                        .WithSeason("2016/2017")
                                        .WithRegulationsLink("volley.dp.ua")
                                        .Build();

            // Act
            var actual = TournamentViewModel.Map(tournament);

            // Assert
            AssertExtensions.AreEqual<TournamentViewModel>(expected, actual, new TournamentViewModelComparer());
        }

        /// <summary>
        /// Test for Map() method.
        /// The method should map tournament view model to domain model.
        /// </summary>
        [TestMethod]
        public void Map_TournamentViewModelWebApi_MappedToDomainModel()
        {
            // Arrange
            var testViewModel = new TournamentViewModelBuilder()
                                        .WithId(2)
                                        .WithDescription("Volley")
                                        .WithName("test tournament")
                                        .WithScheme("2.5")
                                        .WithSeason("2016/2017")
                                        .WithRegulationsLink("volley.dp.ua")
                                        .Build();
            var expected = new TournamentBuilder()
                                        .WithId(2)
                                        .WithDescription("Volley")
                                        .WithName("test tournament")
                                        .WithScheme(TournamentSchemeEnum.TwoAndHalf)
                                        .WithSeason("2016/2017")
                                        .WithRegulationsLink("volley.dp.ua")
                                        .Build();

            // Act
            var actual = testViewModel.ToDomain();

            // Assert
            AssertExtensions.AreEqual<Tournament>(expected, actual, new TournamentComparer());
        }
    }
}
