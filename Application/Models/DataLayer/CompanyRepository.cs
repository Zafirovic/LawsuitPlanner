using System;
using System.Collections.Generic;
using System.Linq;
using Application.Models.DataAccess;

namespace Application.Models.DataLayer
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly LawsuitDataContext context;

        public CompanyRepository(LawsuitDataContext context)
        {
            this.context = context;
        }

        public void add(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Company company = context.Companies.Find(id);
            if (company != null)
            {
                context.Companies.Remove(company);
                context.SaveChanges();
            }
        }

        public Company get(int id)
        {
            return context.Companies.Find(id);
        }

        public IEnumerable<Company> getAll()
        {
            return context.Companies;
        }

        //getting all companies sorted and in particular order
        public IEnumerable<Company> getAll(string sortOrder, string searchString)
        {
            IEnumerable<Company> companies = context.Companies;

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(l => l.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    return companies.OrderByDescending(s => s.name).ToList();
                default:
                    return companies.OrderBy(s => s.name).ToList();
            }
        }

        public void update(Company company)
        {
            var kom = context.Companies.Attach(company);
            kom.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }
    }
}