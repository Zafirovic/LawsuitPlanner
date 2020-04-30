using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface IContactRepository
    {
        Contact get(int id);
        IEnumerable<Contact> getAll(string sortOrder, string searchString);
        Contact add(Contact contact);
        Contact update(Contact contact);
        Contact delete(int id);
    }
}