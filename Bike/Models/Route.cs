using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable

namespace Bike.Models
{
    public partial class Route
    {
        public Route()
        {
            RouteComments = new HashSet<RouteComment>();
            RouteRatings = new HashSet<RouteRating>();
        }

        public int Id { get; set; }
        public short TypeOfRoute { get; set; }
        public string Route1 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? AddressId { get; set; }
        public bool IsActived { get; set; }
        public bool IsBlocked { get; set; }
        public string[] Tags { get; set; }
        public string Desc { get; set; }
        public string[] Images { get; set; }
        public string Title { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<RouteComment> RouteComments { get; set; }
        public virtual ICollection<RouteRating> RouteRatings { get; set; }
    }
}
