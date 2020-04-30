using System.Collections.Generic;
using Application.Models.DataAccess;
using System.Linq;
using System;

namespace Application.Models.DataLayer
{
    public class LocationRepository : ILocationRepository
    {
        private readonly LawsuitDataContext context;

        public LocationRepository(LawsuitDataContext context)
        {
            this.context = context;
        }

        public Location add(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
            return location;
        }

        public Location delete(int id)
        {
            Location location = context.Locations.Find(id);
            if (location != null)
            {
                context.Locations.Remove(location);
                context.SaveChanges();
            }
            return location;
        }

        public Location get(int id)
        {
            return context.Locations.Find(id);
        }

        public Location get(string cityName)
        {
            return (from a in context.Locations 
                    where a.cityName == cityName
                    select a).FirstOrDefault();
        }

        public IEnumerable<Location> getAll(string sortOrder, string searchString)
        {
            IEnumerable<Location> locations = context.Locations;

            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l => l.cityName.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    return locations.OrderByDescending(s => s.cityName).ToList();
                default:
                    return locations.OrderBy(s => s.cityName).ToList();
            }
        }

        public Location update(Location location)
        {
            var lok = context.Locations.Attach(location);
            lok.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return location;
        }
    }
}