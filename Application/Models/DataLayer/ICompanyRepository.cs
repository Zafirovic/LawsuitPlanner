using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ICompanyRepository
    {
        Company get(int id);
        IEnumerable<Company> getAll(string sortOrder, string searchString);
        IEnumerable<Company> getAll();

        void add(Company company);
        void update(Company company);
        void delete(int id);
    }
}