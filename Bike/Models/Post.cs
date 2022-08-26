using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class Post
    {
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostLikes = new HashSet<PostLike>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public bool? IsActived { get; set; }
        public bool IsBlocked { get; set; }
        public string Images { get; set; }
        public string Title { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
