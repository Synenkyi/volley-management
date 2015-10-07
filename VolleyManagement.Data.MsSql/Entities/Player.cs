﻿namespace VolleyManagement.Data.MsSql.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// DAL player model
    /// </summary>
    [Table("Players")]
    public partial class Player
    {
        /// <summary>
        /// Gets or sets id of player
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets first name of player
        /// </summary>
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name of player
        /// </summary>
        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets birth year of player
        /// </summary>
        public int? BirthYear { get; set; }

        /// <summary>
        /// Gets or sets height of player
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets weight of player
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets teamId of player
        /// </summary>
        public int? TeamId { get; set; }
    }
}