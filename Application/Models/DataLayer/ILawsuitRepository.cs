using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ILawsuitRepository
    {
        Lawsuit get(int id);
        IEnumerable<Lawsuit> getForLawyer(string id, string sortOrder, string searchString);
        IEnumerable<Lawsuit> getAll();
        void add(Lawsuit lawsuit);
        void update(Lawsuit lawsuit);
        void delete(int id);
    }
}