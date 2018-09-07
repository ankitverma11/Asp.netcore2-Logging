using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspnetCoreLogging.Models
{
    public partial class MenuModuleMappingContext : DbContext
    {
        public virtual DbSet<MenuModuleMapping> MenuModuleMapping { get; set; }



        public MenuModuleMappingContext(DbContextOptions<MenuModuleMappingContext> dbContextOptions) : base(dbContextOptions)
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
            modelBuilder.Entity<MenuModuleMapping>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.MenuName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuPage)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            });
        }
    }
}
