using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IntroConsoleEF.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string About { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
