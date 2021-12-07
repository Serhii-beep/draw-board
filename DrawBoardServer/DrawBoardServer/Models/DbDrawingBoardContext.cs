using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DrawBoardServer.Models
{
    public class DbDrawingBoardContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public DbDrawingBoardContext(DbContextOptions<DbDrawingBoardContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Room>().ToTable("Rooms");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasColumnType("nvarchar(50)");
                entity.Property(e => e.Login).IsRequired().HasColumnType("nvarchar(30)");
                entity.Property(e => e.Password).IsRequired().HasColumnType("nvarchar(30)");

                entity.HasOne(r => r.Room)
                    .WithMany(u => u.Users)
                    .HasForeignKey(r => r.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Users_Rooms");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Name).HasColumnType("nvarchar(30)");
                entity.Property(e => e.Password).HasColumnType("nvarchar(30)");
            });
        }
    }
}
