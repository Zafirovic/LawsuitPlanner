using System.Collections.Generic;
using System.Linq;

namespace Application.Models.DataLayer
{
    public interface ILocationRepository
    {
        Location get(int id);
        IEnumerable<Location> getAll(string sort, string search);
        void add(Location location);
        void update(Location location);
        void delete(int id);
    }
}