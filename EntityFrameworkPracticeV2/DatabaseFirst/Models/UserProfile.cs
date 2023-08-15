using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public string ImageUrl { get; set; }

    public string About { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }
}
