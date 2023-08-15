using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class ProductVersion
{
    public int Id { get; set; }

    public int Name { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}
