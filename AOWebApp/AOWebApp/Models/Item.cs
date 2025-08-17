using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.Models;

public partial class Item
{
    [Required]
    public int ItemId { get; set; }

    [Required]
    [StringLength(150)]
    public string ItemName { get; set; } = null!;

    [Required]
    public string ItemDescription { get; set; } = null!;

    [Required]
    [Range(0, 99999999.99)]
    public decimal ItemCost { get; set; }

    [Required]
    public string ItemImage { get; set; } = null!;

    [Required]
    public int CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
