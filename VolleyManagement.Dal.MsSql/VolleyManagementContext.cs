namespace VolleyManagement.Dal.MsSql
{
    using System.Data.Entity;

    /// <summary>
    /// volley management database context
    /// </summary>
    internal partial class VolleyManagementContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolleyManagementContext" /> class.
        /// </summary>
        public VolleyManagementContext()
            : base("VolleyManagementContext")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        /// <summary>
        /// Gets or sets the tournament table.
        /// </summary>
        public virtual DbSet<Tournament> Tournaments { get; set; }

        /// <summary>
        /// Gets or sets the user table.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the player table.
        /// </summary>
        public virtual DbSet<Player> Players { get; set; }

        /// <summary>
        /// configure models if needed
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<VolleyManagement.Domain.Players.Player> Players { get; set; }
    }
}
