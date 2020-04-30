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

        public TypeOfProcess add(TypeOfProcess typeOfProcess)
        {
            context.TypeOfProcesses.Add(typeOfProcess);
            context.SaveChanges();
            return typeOfProcess;
        }

        public TypeOfProcess delete(int id)
        {
            TypeOfProcess typeP = context.TypeOfProcesses.Find(id);
            if (typeP != null)
            {
                context.TypeOfProcesses.Remove(typeP);
                context.SaveChanges();
            }
            return typeP;
        }

        public TypeOfProcess get(int id)
        {
            return context.TypeOfProcesses.Find(id);
        }

        public TypeOfProcess get(string name)
        {
            return (from a in context.TypeOfProcesses
                    where a.name == name 
                    select a).FirstOrDefault();
        }

        public IEnumerable<TypeOfProcess> getAll(string sortOrder, string searchString)
        {
            IEnumerable<TypeOfProcess> types = context.TypeOfProcesses;

            if (!String.IsNullOrEmpty(searchString))
            {
                types = types.Where(l => l.name.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    return types.OrderByDescending(s => s.name).ToList();
                default:
                    return types.OrderBy(s => s.name).ToList();
            }
        }

        public TypeOfProcess update(TypeOfProcess typeOfProcess)
        {
            var typeP = context.TypeOfProcesses.Attach(typeOfProcess);
            typeP.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return typeOfProcess;
        }
    }
}