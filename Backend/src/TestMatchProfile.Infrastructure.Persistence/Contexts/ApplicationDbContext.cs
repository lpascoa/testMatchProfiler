using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Domain.Common;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Infrastructure.Persistence.Contexts
{

    public partial class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<LegalContract> LegalContracts { get; set; }

        public virtual DbSet<LegalEntity> LegalEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalContract>(entity =>
            {
                entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.LegalContracts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalContract_Author");

                entity.HasOne(d => d.LegalEntityNavigation).WithMany(p => p.LegalContracts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalContract_LegalEntity");
            });

            modelBuilder.Entity<LegalEntity>(entity =>
            {
                entity.Property(e => e.IdLegalEntity).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}