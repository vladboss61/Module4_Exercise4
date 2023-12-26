using System;

namespace ConsoleEFCore.DbModels
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? HiredDate { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; } // navigation property.

        public UserProfile Profile { get; set; } // navigation property.
    }
}
