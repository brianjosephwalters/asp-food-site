using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext odeToFoodDbContext;

        public SqlRestaurantData(OdeToFoodDbContext odeToFoodDbContext)
        {
            this.odeToFoodDbContext = odeToFoodDbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            odeToFoodDbContext.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return odeToFoodDbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            if (restaurant != null)
            {
                odeToFoodDbContext.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return odeToFoodDbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in odeToFoodDbContext.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = odeToFoodDbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
