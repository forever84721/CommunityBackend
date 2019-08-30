using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models.DbModels
{
    public partial class CommunityContext : DbContext
    {
        public CommunityContext()
        {
        }

        public CommunityContext(DbContextOptions<CommunityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.20.11;Database=Community;User Id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.InnerText).IsRequired();

                entity.Property(e => e.PostTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
