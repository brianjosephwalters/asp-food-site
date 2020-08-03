using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        IEnumerable<Restaurant> GetRestaurantsByName(string name);

        Restaurant GetRestaurantById(int id);

        Restaurant Update(Restaurant updatedRestaurant);

        Restaurant Add(Restaurant newRestaurant);

        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Cinnamon Club", Location="Louisiana", Cuisine = CuisineType.Indian},
                new Restaurant {Id = 3, Name = "La Bonita", Location="Colorado", Cuisine = CuisineType.Mexican}

            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants orderby r.Name select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name 
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
    }
}
