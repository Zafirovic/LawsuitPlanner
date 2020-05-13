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

        public void add(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Location location = context.Locations.Find(id);
            if (location != null)
            {
                context.Locations.Remove(location);
                context.SaveChanges();
            }
        }

        public Location get(int id)
        {
            return context.Locations.Find(id);
        }

        public IEnumerable<Location> getAll(string sortOrder, string searchString)
        {
            IEnumerable<Location> locations = context.Locations;

            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l => l.cityName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return locations.OrderByDescending(s => s.cityName).ToList();
                default:
                    return locations.OrderBy(s => s.cityName).ToList();
            }
        }

        public void update(Location location)
        {
            var lok = context.Locations.Attach(location);
            lok.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}