using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Bike.Models;

#nullable disable

namespace Bike.Data
{
    public partial class bikeContext : DbContext
    {

        public bikeContext(DbContextOptions<bikeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostCommentRating> PostCommentRatings { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteComment> RouteComments { get; set; }
        public virtual DbSet<RouteCommentRating> RouteCommentRatings { get; set; }
        public virtual DbSet<RouteRating> RouteRatings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;Database=bike;Port=5432;User Id=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Coordinat)
                    .IsRequired()
                    .HasColumnName("coordinat");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActived)
                    .IsRequired()
                    .HasColumnName("is_actived")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.UpInt)
                    .HasColumnName("up_int")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admins");

                entity.HasIndex(e => e.Username, "admins_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("firstname");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.PasswordHass)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password_hass");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password_salt");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("follows");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FollowerId).HasColumnName("follower_id");

                entity.Property(e => e.IsActived)
                    .IsRequired()
                    .HasColumnName("is_actived")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Follower)
                    .WithMany(p => p.FollowFollowers)
                    .HasForeignKey(d => d.FollowerId)
                    .HasConstraintName("fk_follow_follower");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_follow_user");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Images)
                    .HasMaxLength(500)
                    .HasColumnName("images");

                entity.Property(e => e.IsActived)
                    .IsRequired()
                    .HasColumnName("is_actived")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_post_user");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.ToTable("post_comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UpId)
                    .HasColumnName("up_id")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("fk_post_comment_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_post_comment_user");
            });

            modelBuilder.Entity<PostCommentRating>(entity =>
            {
                entity.ToTable("post_comment_ratings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PostCommentId).HasColumnName("post_comment_id");

                entity.Property(e => e.RatingLike).HasColumnName("rating_like");

                entity.Property(e => e.RatingUnlike).HasColumnName("rating_unlike");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.PostComment)
                    .WithMany(p => p.PostCommentRatings)
                    .HasForeignKey(d => d.PostCommentId)
                    .HasConstraintName("fk_post_comment_rating_comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostCommentRatings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_post_comment_rating_user");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.ToTable("post_likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.RatingLike).HasColumnName("rating_like");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("fk_post_likes_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_post_likes_user");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("routes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Desc).HasColumnName("desc");

                entity.Property(e => e.Images)
                    .HasColumnType("character varying(250)[]")
                    .HasColumnName("images");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.Route1)
                    .IsRequired()
                    .HasColumnName("route");

                entity.Property(e => e.Tags)
                    .HasColumnType("character varying(50)[]")
                    .HasColumnName("tags");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.Property(e => e.TypeOfRoute).HasColumnName("type_of_route");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("fk_route_address");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_route_user");
            });

            modelBuilder.Entity<RouteComment>(entity =>
            {
                entity.ToTable("route_comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.UpId)
                    .HasColumnName("up_id")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteComments)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("fk_route_comment_route");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RouteComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_route_comment_user");
            });

            modelBuilder.Entity<RouteCommentRating>(entity =>
            {
                entity.ToTable("route_comment_ratings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RatingLike).HasColumnName("rating_like");

                entity.Property(e => e.RatingUnlike).HasColumnName("rating_unlike");

                entity.Property(e => e.RouteCommentId).HasColumnName("route_comment_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.RouteComment)
                    .WithMany(p => p.RouteCommentRatings)
                    .HasForeignKey(d => d.RouteCommentId)
                    .HasConstraintName("fk_route_comment_rating_comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RouteCommentRatings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_route_comment_rating_user");
            });

            modelBuilder.Entity<RouteRating>(entity =>
            {
                entity.ToTable("route_ratings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteRatings)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("fk_route_rating_route");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RouteRatings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_route_rating_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "users_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(500)
                    .HasColumnName("avatar");

                entity.Property(e => e.Birthday)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("birthday");

                entity.Property(e => e.CommentCount)
                    .HasColumnName("comment_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("firstname");

                entity.Property(e => e.FollowerCount)
                    .HasColumnName("follower_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FollowingCount)
                    .HasColumnName("following_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsActived).HasColumnName("is_actived");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.PasswordHass)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password_hass");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password_salt");

                entity.Property(e => e.PostCount)
                    .HasColumnName("post_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
