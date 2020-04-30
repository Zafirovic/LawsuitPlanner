using System.Collections.Generic;
using Application.Models.DataAccess;

namespace Application.Models.DataLayer
{
    public class ContactRepository : IContactRepository
    {
        private readonly LawsuitDataContext context;

        public ContactRepository(LawsuitDataContext context)
        {
            this.context = context;
        }

        public Contact add(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
            return contact;
        }

        public Contact delete(int id)
        {
            Contact contact = context.Contacts.Find(id);
            if (contact != null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
            return contact;
        }

        public Contact get(int id)
        {
            return context.Contacts.Find(id);
        }

        public IEnumerable<Contact> getAll()
        {
            return context.Contacts;
        }

        public Contact update(Contact contact)
        {
            var kon = context.Contacts.Attach(contact);
            kon.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return contact;
        }
    }
}