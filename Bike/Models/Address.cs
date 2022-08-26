using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class Address
    {
        public Address()
        {
            Routes = new HashSet<Route>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public decimal[] Coordinat { get; set; }
        public int? UpInt { get; set; }
        public bool? IsActived { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
