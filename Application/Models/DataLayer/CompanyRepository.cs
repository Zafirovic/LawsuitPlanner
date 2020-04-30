using System.Collections.Generic;
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

        public Company add(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();
            return company;
        }

        public Company delete(int id)
        {
            Company company = context.Companies.Find(id);
            if (company != null)
            {
                context.Companies.Remove(company);
                context.SaveChanges();
            }
            return company;
        }

        public Company get(int id)
        {
            return context.Companies.Find(id);
        }

        public IEnumerable<Company> getAll()
        {
            return context.Companies;
        }

        public Company update(Company company)
        {
            var kom = context.Companies.Attach(company);
            kom.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return company;
        }
    }
}