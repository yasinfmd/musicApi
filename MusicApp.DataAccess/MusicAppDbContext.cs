using System;
using Microsoft.EntityFrameworkCore;
using MusicApp.Entity;
using MySQL.Data.EntityFrameworkCore;
namespace MusicApp.DataAccess
{
    public class MusicAppDbContext : DbContext
    {
        public DbSet<MusicTypes> MusicTypes { get; set; }
        public DbSet<Files> Files { get; set; }

        public DbSet<Music> Musics { get; set; }

        public DbSet<Albums> Albums { get; set; }
        public DbSet<Artist> Artist { get; set; }



        public DbSet<AlbumsFiles> AlbumsFiles { get; set; }
        public MusicAppDbContext(DbContextOptions<MusicAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=music_app;user=root;password=");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Music>()
          .HasOne(m => m.Album)
          .WithMany(a => a.Musics)
          .HasForeignKey(m => m.AlbumId)
          .IsRequired().OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Music>()
                .HasOne(m => m.MusicTypes)
                .WithMany(mt => mt.Music)
               .HasForeignKey(m => m.MusicTypesId)
                .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Albums>()
           .HasOne(a => a.Artist)
           .WithMany(a => a.Albums)
              .HasForeignKey(a => a.ArtistId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<AlbumsFiles>()
              .HasKey(af => new { af.AlbumId, af.FileId });

            modelBuilder.Entity<AlbumsFiles>()
                .HasOne(af => af.Album)
                .WithMany(a => a.AlbumsFiles)
                .IsRequired().OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.AlbumId);

            modelBuilder.Entity<AlbumsFiles>()
                .HasOne(af => af.File)
                .WithMany(f => f.AlbumsFiles)
                .IsRequired().OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(af => af.FileId);

        }
    }
}
