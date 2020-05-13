using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ITypeOfProcessRepository
    {
        TypeOfProcess get(int id);
        IEnumerable<TypeOfProcess> getAll(string sortOrder, string searchString);
        void add(TypeOfProcess typeOfProcess);
        void update(TypeOfProcess typeOfProcess);
        void delete(int id);

    }
}