using System;
using System.Collections.Generic;
using System.Text;

namespace IntroConsoleEF
{
    public class UserProfile
    {
        public int Id { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string Abount { get; set; }
        
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
