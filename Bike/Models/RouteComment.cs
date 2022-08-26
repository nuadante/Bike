using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class RouteComment
    {
        public RouteComment()
        {
            RouteCommentRatings = new HashSet<RouteCommentRating>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? UpId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? RouteId { get; set; }
        public bool IsActived { get; set; }
        public bool IsBlocked { get; set; }

        public virtual Route Route { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<RouteCommentRating> RouteCommentRatings { get; set; }

        public static implicit operator RouteComment(List<RouteComment> v)
        {
            throw new NotImplementedException();
        }
    }
}
