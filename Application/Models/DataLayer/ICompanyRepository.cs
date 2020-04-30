using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface ICompanyRepository
    {
        Company get(int id);
        IEnumerable<Company> getAll(string sortOrder, string searchString);
        IEnumerable<Company> getAll();

        Company add(Company company);
        Company update(Company company);
        Company delete(int id);
    }
}