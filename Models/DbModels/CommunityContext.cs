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

        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=Community;User Id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FollowUserId });

                entity.Property(e => e.StartFollowTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PostId });

                entity.Property(e => e.LikeTime).HasColumnType("datetime");
            });

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
