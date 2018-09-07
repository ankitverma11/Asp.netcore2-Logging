using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class MenuModuleMapping_CoreContext : DbContext
    {
        public virtual DbSet<MenuModuleMapping_Core> MenuModuleMapping_Core { get; set; }

        public MenuModuleMapping_CoreContext(DbContextOptions<MenuModuleMapping_CoreContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=Powells-db\Genacis2016;database=Genacis;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuModuleMapping_Core>(entity =>
            {
                entity.HasKey(e => e.MenuID);

                entity.Property(e => e.MenuID).HasColumnName("MenuID");

                entity.Property(e => e.MenuName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuPage)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleID).HasColumnName("ModuleID");

                entity.Property(e => e.ParentMenuID).HasColumnName("ParentMenuID");
            });
        }
    }
}
    