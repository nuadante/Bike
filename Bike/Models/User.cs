using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class User
    {
        public User()
        {
            FollowFollowers = new HashSet<Follow>();
            FollowUsers = new HashSet<Follow>();
            PostCommentRatings = new HashSet<PostCommentRating>();
            PostComments = new HashSet<PostComment>();
            PostLikes = new HashSet<PostLike>();
            Posts = new HashSet<Post>();
            RouteCommentRatings = new HashSet<RouteCommentRating>();
            RouteComments = new HashSet<RouteComment>();
            RouteRatings = new HashSet<RouteRating>();
            Routes = new HashSet<Route>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FollowerCount { get; set; }
        public int? FollowingCount { get; set; }
        public int? PostCount { get; set; }
        public int? CommentCount { get; set; }
        public bool IsActived { get; set; }
        public bool IsBlocked { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHass { get; set; }
        public string Avatar { get; set; }

        public virtual ICollection<Follow> FollowFollowers { get; set; }
        public virtual ICollection<Follow> FollowUsers { get; set; }
        public virtual ICollection<PostCommentRating> PostCommentRatings { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<RouteCommentRating> RouteCommentRatings { get; set; }
        public virtual ICollection<RouteComment> RouteComments { get; set; }
        public virtual ICollection<RouteRating> RouteRatings { get; set; }
        public virtual ICollection<Route> Routes { get; set; }

        public static implicit operator User(List<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
