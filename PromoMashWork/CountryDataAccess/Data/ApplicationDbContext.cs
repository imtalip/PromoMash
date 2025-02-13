namespace CountryDataAccess.Data
{
    using CountryDataAccess.Constants;
    using CountryDataAccess.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly string _schema;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="workflowSchema">Наименование схемы БД.</param>
        public ApplicationDbContext(DbContextOptions options)
        : base(options) =>
            _schema = TableConstants.Schema;

        /// <summary>
        /// Country.
        /// </summary>
        public DbSet<Country> Country => Set<Country>();

        /// <summary>
        /// Province.
        /// </summary>
        public DbSet<Province> Province => Set<Province>();

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            // Можно логику для переопределения сделать.

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
