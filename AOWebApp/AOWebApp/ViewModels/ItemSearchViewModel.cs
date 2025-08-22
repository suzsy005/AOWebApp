using Microsoft.AspNetCore.Mvc.Rendering;
using AOWebApp.Models;
using System.Collections.Generic;

namespace AOWebApp.ViewModels
{
    public class ItemSearchViewModel
    {
        public string SearchText { get; set; }

        public int? CategoryId { get; set; }

        public SelectList CategoryList { get; set; }

        public List<ItemRatingViewModel> ItemList { get; set; }
    }
}
