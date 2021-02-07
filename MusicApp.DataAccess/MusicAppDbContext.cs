using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicApp.Entity;
using MySQL.Data.EntityFrameworkCore;
namespace MusicApp.DataAccess
{
    public class MusicAppDbContext : IdentityDbContext
    {
   



        public DbSet<AlbumsFiles> AlbumsFiles { get; set; }
         public MusicAppDbContext(DbContextOptions options)
       : base(options)
         {

       }
        //public MusicAppDbContext(DbContextOptions<MusicAppDbContext> options)
        //     : base(options)
        // {

        // }
        public DbSet<MusicTypes> MusicTypes { get; set; }
        public DbSet<Files> Files { get; set; }

        public DbSet<Music> Musics { get; set; }

        public DbSet<Albums> Albums { get; set; }
        public DbSet<Artist> Artist { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=music_app;user=root;password=");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
            .Property(u => u.Id)
            .HasMaxLength(36);


            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(200));
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(200));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(200));




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

       //     modelBuilder.Entity<BookAuthor>()
       //.HasOne(pt => pt.Book)
       //.WithMany(p => p.AuthorsLink)
       //.HasForeignKey(pt => pt.BookId);

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
