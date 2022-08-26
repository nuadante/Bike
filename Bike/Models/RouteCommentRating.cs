﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class RouteCommentRating
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? RouteCommentId { get; set; }
        public bool RatingLike { get; set; }
        public bool RatingUnlike { get; set; }

        public virtual RouteComment RouteComment { get; set; }
        public virtual User User { get; set; }

        public static implicit operator RouteCommentRating(List<RouteCommentRating> v)
        {
            throw new NotImplementedException();
        }
    }
}
