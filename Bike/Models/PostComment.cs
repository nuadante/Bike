using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class PostComment
    {
        public PostComment()
        {
            PostCommentRatings = new HashSet<PostCommentRating>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? UpId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public bool IsActived { get; set; }
        public bool IsBlocked { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostCommentRating> PostCommentRatings { get; set; }

        public static implicit operator PostComment(List<PostComment> v)
        {
            throw new NotImplementedException();
        }
    }
}
