using System;
using System.Collections.Generic;

namespace DbFirst_Stationery_;

public partial class Sale
{
    public int Id { get; set; }

    public int? StationeryId { get; set; }

    public int? FirmId { get; set; }

    public int? ManagerId { get; set; }

    public int Quantity { get; set; }

    public decimal? PricePerUnitSold { get; set; }

    public DateOnly? DateOfSale { get; set; }

    public virtual Firm? Firm { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual Stationery? Stationery { get; set; }
}
