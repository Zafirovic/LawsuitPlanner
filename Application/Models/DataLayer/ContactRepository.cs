using System;
using System.Collections.Generic;
using System.Linq;
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

        public void add(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Contact contact = context.Contacts.Find(id);
            if (contact != null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
        }

        public Contact get(int id)
        {
            return context.Contacts.Find(id);
        }

        //returning sorted or/and searched contacts
        public IEnumerable<Contact> getAll(string sortOrder, string searchString)
        {
            IEnumerable<Contact> contacts = context.Contacts;

            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(l => l.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.phone1.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.phone2.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.address.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.email.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.job.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .ToList();
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    return contacts.OrderByDescending(s => s.name).ToList();
                default:
                    return contacts.OrderBy(s => s.name).ToList();
            }
        }

        public void update(Contact contact)
        {
            var kon = context.Contacts.Attach(contact);
            kon.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}