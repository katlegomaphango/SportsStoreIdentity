﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportsStore.Models;

public class Order
{
    [BindNever]
    public int OrderID { get; set; }

    [BindNever]
    public ICollection<CartLine> Lines { get; set; }

    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the first address line")]
    [DisplayName("Line 1")]
    public string Line1 { get; set; }

    [DisplayName("Line 2")]
    public string Line2 { get; set; }

    [DisplayName("Line 3")]
    public string Line3 { get; set; }

    [Required(ErrorMessage = "Please enter a city name")]
    public string City { get; set; }

    [Required(ErrorMessage = "Please enter a state name")]
    public string State { get; set; }

    public string Zip { get; set; }

    [Required(ErrorMessage = "Please enter a country name")]
    public string Country { get; set; }

    public bool GiftWrap { get; set; }

    [BindNever]
    public bool Shipped { get; set; }
}
