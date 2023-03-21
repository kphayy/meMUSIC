using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace meMUSIC
{
    public partial class meMUSIC_DBContext : DbContext
    {
        public meMUSIC_DBContext()
            : base("name=meMUSIC_DBContext")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<BaiHat> BaiHats { get; set; }
        public virtual DbSet<CT_Album> CT_Album { get; set; }
        public virtual DbSet<CT_Playlist> CT_Playlist { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<UserFrequentArtist> UserFrequentArtists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.MaAlbum)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .Property(e => e.MaArtist)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .HasMany(e => e.CT_Album)
                .WithRequired(e => e.Album)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artist>()
                .Property(e => e.MaArtist)
                .IsUnicode(false);

            modelBuilder.Entity<Artist>()
                .HasMany(e => e.Albums)
                .WithRequired(e => e.Artist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artist>()
                .HasMany(e => e.BaiHats)
                .WithRequired(e => e.Artist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artist>()
                .HasMany(e => e.UserFrequentArtists)
                .WithRequired(e => e.Artist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.MaBaiHat)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.MaArtist)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.TheLoai1)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.TheLoai2)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHat>()
                .HasMany(e => e.CT_Album)
                .WithRequired(e => e.BaiHat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaiHat>()
                .HasMany(e => e.CT_Playlist)
                .WithRequired(e => e.BaiHat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CT_Album>()
                .Property(e => e.MaAlbum)
                .IsUnicode(false);

            modelBuilder.Entity<CT_Album>()
                .Property(e => e.MaBaiHat)
                .IsUnicode(false);

            modelBuilder.Entity<CT_Playlist>()
                .Property(e => e.MaPlaylist)
                .IsUnicode(false);

            modelBuilder.Entity<CT_Playlist>()
                .Property(e => e.MaBaiHat)
                .IsUnicode(false);

            modelBuilder.Entity<Playlist>()
                .Property(e => e.MaPlaylist)
                .IsUnicode(false);

            modelBuilder.Entity<Playlist>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Playlist>()
                .HasMany(e => e.CT_Playlist)
                .WithRequired(e => e.Playlist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserFrequentArtist>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<UserFrequentArtist>()
                .Property(e => e.MaArtist)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Playlists)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserFrequentArtists)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
