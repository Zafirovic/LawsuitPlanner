using System.Collections.Generic;
using Application.Models.DataAccess;
using System.Linq;
using System;

namespace Application.Models.DataLayer
{
    public class TypeOfProcessRepository : ITypeOfProcessRepository
    {
        private readonly LawsuitDataContext context;

        public TypeOfProcessRepository(LawsuitDataContext context)
        {
            this.context = context;
        }

        public TypeOfProcess get(int id)
        {
            return context.TypeOfProcesses.Find(id);
        }

        public IEnumerable<TypeOfProcess> getAll(string sortOrder, string searchString)
        {
            IEnumerable<TypeOfProcess> types = context.TypeOfProcesses;

            if (!String.IsNullOrEmpty(searchString))
            {
                types = types.Where(l => l.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    return types.OrderByDescending(s => s.name).ToList();
                default:
                    return types.OrderBy(s => s.name).ToList();
            }
        }

        public void add(TypeOfProcess typeOfProcess)
        {
            context.TypeOfProcesses.Add(typeOfProcess);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            TypeOfProcess typeP = context.TypeOfProcesses.Find(id);
            if (typeP != null)
            {
                context.TypeOfProcesses.Remove(typeP);
                context.SaveChanges();
            }
        }

        public void update(TypeOfProcess typeOfProcess)
        {
            var typeP = context.TypeOfProcesses.Attach(typeOfProcess);
            typeP.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}