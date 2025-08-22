using Microsoft.AspNetCore.Mvc.Rendering;
using AOWebApp.Models;
using System.Collections.Generic;

namespace AOWebApp.ViewModels
{
    public class ItemRatingViewModel
    {
        // to hold the actual item being displayed
        public Item TheItem { get; set; }

        // to hold the number of reviews for TheItem
        public int ReviewCount { get; set; }

        // to hold the average rating for the above reviews of this item
        public double AverageRating { get; set; }

    }
}
