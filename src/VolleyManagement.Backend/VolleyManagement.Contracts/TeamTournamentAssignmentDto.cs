﻿namespace VolleyManagement.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains information about assignment team to tournament
    /// </summary>
    public class TeamTournamentAssignmentDto
    {
        /// <summary>
        /// Gets or sets a value of TeamId.
        /// </summary>
        /// <value>TeamId of team.</value>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets a value of GroupId.
        /// </summary>
        /// <value>GroupId of group.</value>
        public int GroupId { get; set; }
    }
}
