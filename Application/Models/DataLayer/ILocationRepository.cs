using System.Collections.Generic;
using System.Linq;

namespace Application.Models.DataLayer
{
    public interface ILocationRepository
    {
        Location get(int id);
        Location get(string cityName);
        IEnumerable<Location> getAll(string sort, string search);
        Location add(Location location);
        Location update(Location location);
        Location delete(int id);
    }
}