using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class RouteRating
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RouteId { get; set; }
        public int? Rating { get; set; }
        public bool IsActived { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Route Route { get; set; }
        public virtual User User { get; set; }

        public static implicit operator RouteRating(List<RouteRating> v)
        {
            throw new NotImplementedException();
        }
    }
}
