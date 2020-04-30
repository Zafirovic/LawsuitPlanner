using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ILawsuitRepository
    {
        Lawsuit get(int id);
        IEnumerable<Lawsuit> getAll();
        Lawsuit add(Lawsuit lawsuit);
        Lawsuit update(Lawsuit lawsuit);
        Lawsuit delete(int id);
        IEnumerable<Lawsuit> getForLawyer(string id, string sortOrder, string searchString);
    }
}