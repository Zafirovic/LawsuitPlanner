using System.Collections.Generic;

namespace Application.Models.DataLayer
{
    public interface IContactRepository
    {
        Contact get(int id);
        IEnumerable<Contact> getAll();
        Contact add(Contact contact);
        Contact update(Contact contact);
        Contact delete(int id);
    }
}