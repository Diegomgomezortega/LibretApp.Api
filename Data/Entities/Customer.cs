using System;
using System.Collections.Generic;

namespace libretapp.Data.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();
}
