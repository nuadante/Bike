using System;
using System.Collections.Generic;

#nullable disable

namespace Bike.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public bool IsActived { get; set; }
        public bool IsBlocked { get; set; }
        public string? PasswordSalt { get; set; }
        public string PasswordHass { get; set; }
    }
}
