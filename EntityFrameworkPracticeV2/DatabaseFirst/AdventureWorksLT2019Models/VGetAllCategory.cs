using System;
using System.Collections.Generic;

namespace DatabaseFirst.AdventureWorksLT2019Models;

public partial class VGetAllCategory
{
    public string ParentProductCategoryName { get; set; }

    public string ProductCategoryName { get; set; }

    public int? ProductCategoryId { get; set; }
}
