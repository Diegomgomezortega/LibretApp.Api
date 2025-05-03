using System;
using System.Collections.Generic;

namespace libretapp.Data.Entities;

public partial class ProductPriceHistory
{
    public int PriceHistoryId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
