using System;
using AuthentificationApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AuthentificationApi.DAL.Context
{
    public partial class GuardianBagPackContext : DbContext, IGuardianBagPackContext
    {

        public GuardianBagPackContext(DbContextOptions<GuardianBagPackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.HasKey(e => e.IdAuth);

                entity.ToTable("Auth");

                entity.Property(e => e.AppId).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
