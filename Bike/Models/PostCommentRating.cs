using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class PostCommentRating
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? PostCommentId { get; set; }
        public bool RatingLike { get; set; }
        public bool RatingUnlike { get; set; }

        public virtual PostComment PostComment { get; set; }
        public virtual User User { get; set; }

        public static implicit operator PostCommentRating(List<PostCommentRating> v)
        {
            throw new NotImplementedException();
        }
    }
}
