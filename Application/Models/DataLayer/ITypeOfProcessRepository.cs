using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ITypeOfProcessRepository
    {
        TypeOfProcess get(int id);
        TypeOfProcess get(string name);
        IEnumerable<TypeOfProcess> getAll();
        TypeOfProcess add(TypeOfProcess typeOfProcess);
        TypeOfProcess update(TypeOfProcess typeOfProcess);
        TypeOfProcess delete(int id);

    }
}