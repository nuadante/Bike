using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class PostLike
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public bool RatingLike { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }

        public static implicit operator PostLike(List<PostLike> v)
        {
            throw new NotImplementedException();
        }
    }
}
