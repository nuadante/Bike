using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class Follow
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FollowerId { get; set; }
        public bool? IsActived { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User Follower { get; set; }
        public virtual User User { get; set; }

        public static implicit operator Follow(List<Follow> v)
        {
            throw new NotImplementedException();
        }
    }
}
