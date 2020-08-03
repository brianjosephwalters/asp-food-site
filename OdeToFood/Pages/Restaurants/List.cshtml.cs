using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public ListModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Message = "Hello, World!";
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
