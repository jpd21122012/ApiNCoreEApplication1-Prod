using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNCoreEApplication1.Entity.Context
{
    public class ApiNCoreEApplication1Context : DbContext
    {

        public ApiNCoreEApplication1Context(DbContextOptions<ApiNCoreEApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EnigmaUser> EnigmaUsers { get; set; }
        public DbSet<EnigmaUsersType> EnigmaUsersType { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStat> PlayerStats { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<User> Users { get; set; }

        //lazy-loading
        //https://entityframeworkcore.com/querying-data-loading-eager-lazy
        //https://docs.microsoft.com/en-us/ef/core/querying/related-data
        //EF Core will enable lazy-loading for any navigation property that is virtual and in an entity class that can be inherited
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<User>()
           .HasOne(u => u.Account)
           .WithMany(e => e.Users);

            //concurrency
            modelBuilder.Entity<Account>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Award>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Category>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<EnigmaUser>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<EnigmaUsersType>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Match>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Player>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<PlayerStat>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Score>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Season>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Team>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Tournament>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<User>()
            .Property(a => a.RowVersion).IsRowVersion();

            //modelBuilder.Entity<User>()
            //.Property(p => p.DecryptedPassword)
            //.HasComputedColumnSql("Uncrypt(p.PasswordText)");
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }

    }
}
