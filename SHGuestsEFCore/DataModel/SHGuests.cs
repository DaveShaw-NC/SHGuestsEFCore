using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SHGuestsEFCore.DataModel
{
    public partial class SHGuests : DbContext
    {
        public virtual DbSet<Guests> Guests { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string pr_connect = Properties.Settings.Default.Production_Connect;
                string connect = Properties.Settings.Default.ConnectionString;
# if DEBUG

                    optionsBuilder.UseSqlServer ( connect );
#else
                optionsBuilder.UseSqlServer ( pr_connect );
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("FK_dbo.Photos_dbo.Guests_Guest_ID");
            });

            modelBuilder.Entity<Visits>(entity =>
            {
                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.VisitsNavigation)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("FK_dbo.Visits_dbo.Guests_GuestID");
            });
        }
    }
}
