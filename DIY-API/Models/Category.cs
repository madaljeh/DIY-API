using System;
using System.Collections.Generic;

namespace DIY_API.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryNameEn { get; set; } = null!;

    public string CategoryNameAr { get; set; } = null!;

    public string? Image { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}
