using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface IContactRepository
    {
        Contact get(int id);
        IEnumerable<Contact> getAll(string sortOrder, string searchString);
        void add(Contact contact);
        void update(Contact contact);
        void delete(int id);
    }
}