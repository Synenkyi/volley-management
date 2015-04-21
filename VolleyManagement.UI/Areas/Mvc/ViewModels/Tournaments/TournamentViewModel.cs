﻿namespace VolleyManagement.UI.Areas.Mvc.ViewModels.Tournaments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VolleyManagement.Domain;
    using VolleyManagement.Domain.Tournaments;
    using VolleyManagement.UI.App_GlobalResources;

    /// <summary>
    /// TournamentViewModel for Create and Edit actions
    /// </summary>
    public class TournamentViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentViewModel"/> class.
        /// </summary>
        public TournamentViewModel()
        {
            this.Scheme = TournamentSchemeEnum.One;
            this.InitializeSeasonsList();
        }

        /// <summary>
        /// Gets or sets the list of seasons.
        /// </summary>
        /// <value>The list of seasons.</value>
        public Dictionary<short, string> SeasonsList { get; set; }

        /// <summary>
        /// Gets or sets the default season
        /// </summary>
        /// <value>Default season</value>
        public string SelectedSeason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where Id.
        /// </summary>
        /// <value>Id of tournament.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where Name.
        /// </summary>
        /// <value>Name of tournament.</value>
        [Display(Name = "TournamentName", ResourceType = typeof(ViewModelResources))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ViewModelResources))]
        [StringLength(Constants.Tournament.MAX_NAME_LENGTH, ErrorMessageResourceName = "MaxLengthErrorMessage"
            , ErrorMessageResourceType = typeof(ViewModelResources))]
        [RegularExpression(@"^[\S\x20]+$", ErrorMessageResourceName = "InvalidEntriesError", ErrorMessageResourceType = typeof(ViewModelResources))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where Description.
        /// </summary>
        /// <value>Description of tournament.</value>
        [Display(Name = "TournamentDescription", ResourceType = typeof(ViewModelResources))]
        [StringLength(Constants.Tournament.MAX_DESCRIPTION_LENGTH, ErrorMessageResourceName = "MaxLengthErrorMessage"
            , ErrorMessageResourceType = typeof(ViewModelResources))]
        [RegularExpression(@"^[\S\x20]+$", ErrorMessageResourceName = "InvalidEntriesError", ErrorMessageResourceType = typeof(ViewModelResources))]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where Season.
        /// </summary>
        /// <value>Season of tournament.</value>
        [Display(Name = "TournamentSeason", ResourceType = typeof(ViewModelResources))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ViewModelResources))]
        [Range(Constants.Tournament.MINIMAL_SEASON_YEAR, Constants.Tournament.MAXIMAL_SEASON_YEAR
            , ErrorMessageResourceName = "NotInRange", ErrorMessageResourceType = typeof(ViewModelResources))]
        public short Season { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where Scheme.
        /// </summary>
        /// <value>Scheme of tournament.</value>
        [Display(Name = "TournamentScheme", ResourceType = typeof(ViewModelResources))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ViewModelResources))]
        public TournamentSchemeEnum Scheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating regulations of tournament.
        /// </summary>
        /// <value>regulations of tournament.</value>
        [Display(Name = "TournamentRegulationsLink", ResourceType = typeof(ViewModelResources))]
        [StringLength(Constants.Tournament.MAX_REGULATION_LENGTH, ErrorMessageResourceName = "MaxLengthErrorMessage"
            , ErrorMessageResourceType = typeof(ViewModelResources))]
        public string RegulationsLink { get; set; }


        /// <summary>
        /// Start of a tournament
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End of a tournament
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Start of a transfer period
        /// </summary>
        public DateTime TransferStart { get; set; }

        /// <summary>
        /// End of a transfer period
        /// </summary>
        public DateTime TransferEnd { get; set; }

        /// <summary>
        /// Start of a tournament registration
        /// </summary>
        public DateTime RegistrationStart { get; set; }

        /// <summary>
        /// End of a tournament registration
        /// </summary>
        public DateTime RegistrationEnd { get; set; }






        /// <summary>
        /// Initializes list of seasons.
        /// </summary>
        private void InitializeSeasonsList()
        {
            this.SeasonsList = new Dictionary<short, string>();
            const short yearsRange = 16;
            const short yearsBeforeToday = 5;
            short year = (short)(DateTime.Now.Year - yearsBeforeToday);
            for (int i = 0; i < yearsRange; i++, year++)
            {
                var str = string.Format("{0}/{1}", year, year + 1);
                if (DateTime.Now.Year == year + 1)
                {
                    SelectedSeason = str;
                }
                this.SeasonsList.Add(year, str);
            }
        }
        #region Factory Methods

        /// <summary>
        /// Maps domain entity to presentation
        /// </summary>
        /// <param name="tournament"> Domain object </param>
        /// <returns> View model object </returns>
        public static TournamentViewModel Map(Tournament tournament)
        {
            var tournamentViewModel = new TournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Description = tournament.Description,
                Season = tournament.Season,
                RegulationsLink = tournament.RegulationsLink,
                Scheme = tournament.Scheme,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                RegistrationStart = tournament.RegistrationStart,
                RegistrationEnd = tournament.RegistrationEnd,
                TransferStart = tournament.TransferStart,
                TransferEnd = tournament.TransferEnd
            };

            return tournamentViewModel;
        }

        /// <summary>
        /// Maps presentation entity to domain
        /// </summary>
        /// <returns> Domain object </returns>
        public Tournament ToDomain()
        {
            return new Tournament
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Season = this.Season,
                Scheme = this.Scheme,
                RegulationsLink = this.RegulationsLink,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                RegistrationStart = this.RegistrationStart,
                RegistrationEnd  = this.RegistrationEnd,
                TransferStart = this.TransferStart,
                TransferEnd = this.TransferEnd
            };
        }
        #endregion
    }
}