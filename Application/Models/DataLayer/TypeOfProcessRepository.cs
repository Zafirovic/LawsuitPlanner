using System.Collections.Generic;
using Application.Models.DataAccess;
using System.Linq;

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

        public IEnumerable<TypeOfProcess> getAll()
        {
            return context.TypeOfProcesses;
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