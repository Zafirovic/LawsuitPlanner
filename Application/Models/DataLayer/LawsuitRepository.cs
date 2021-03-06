using System.Collections.Generic;
using Application.Models.DataAccess;
using System.Linq;
using System;

namespace Application.Models.DataLayer
{
    public class LawsuitRepository : ILawsuitRepository
    {
        private readonly LawsuitDataContext context;
        private readonly ILocationRepository locationRepository;
        private readonly IContactRepository contactRepository;
        private readonly ITypeOfProcessRepository typeOfProcessRepository;

        public LawsuitRepository(LawsuitDataContext context, ILocationRepository locRepository,
                                 IContactRepository conRepository, ITypeOfProcessRepository tRepository)
        {
            this.context = context;
            this.locationRepository = locRepository;
            this.contactRepository = conRepository;
            this.typeOfProcessRepository = tRepository;
        }

        public Lawsuit get(int id)
        {
            return context.Lawsuits.Find(id);
        }

        //returning lawsuits only for currently logged in user/lawyer
        public IEnumerable<Lawsuit> getForLawyer(string id, string sortOrder, string searchString)
        {
            List<LawsuitLawyer> lawyerLawsuits = (from l in context.LawsuitLawyers
                                                  where l.userId == id select l).ToList();

            List<Lawsuit> lawsuits = new List<Lawsuit>();
            Lawsuit lawsuit;
            foreach(LawsuitLawyer ll in lawyerLawsuits)
            {
                lawsuit = context.Lawsuits.Find(ll.lawsuitId);
                context.Entry(lawsuit).Reference(r => r.judge).Load();
                context.Entry(lawsuit).Reference(r => r.location).Load();
                context.Entry(lawsuit).Reference(r => r.prosecutor).Load();
                context.Entry(lawsuit).Reference(r => r.defendant).Load();
                context.Entry(lawsuit).Reference(r => r.typeOfProcess).Load();
                lawsuit.lawyers = null;
               
                lawsuits.Add(lawsuit);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                lawsuits = lawsuits.Where(l => l.judge.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 || 
                                               l.location.cityName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               ((TipSuda)l.courtType).ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.processId.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.courtroomNumber.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.prosecutor.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.defendant.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.note.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                               l.typeOfProcess.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .ToList();
            }
            
            switch (sortOrder)
            {
                case "judgeName_desc":
                    return lawsuits.OrderByDescending(s => s.judge.name).ToList();
                case "courtTypeName_desc":
                    return lawsuits.OrderByDescending(s => s.typeOfProcess.name).ToList();
                case "prosecutorName_desc":
                    return lawsuits.OrderByDescending(s => s.prosecutor.name).ToList();
                case "defendantTypeName_desc":
                    return lawsuits.OrderByDescending(s => s.defendant.name).ToList();
                case "dateTime_desc":
                    return lawsuits.OrderByDescending(s => s.dateTimeOfEvent).ToList();
                default:
                    return lawsuits.OrderByDescending(s => s.courtType).ToList();
            }
        }

        public IEnumerable<Lawsuit> getAll()
        {
            IEnumerable<Lawsuit> lawsuits = context.Lawsuits;

            foreach(Lawsuit l in lawsuits)
            {
                context.Entry(l).Reference(r => r.judge).Load();
                context.Entry(l).Reference(r => r.location).Load();
                context.Entry(l).Reference(r => r.prosecutor).Load();
                context.Entry(l).Reference(r => r.defendant).Load();
                context.Entry(l).Reference(r => r.typeOfProcess).Load();
            }
            return lawsuits;
        }
        
        public void add(Lawsuit lawsuit)
        {
            context.Lawsuits.Add(lawsuit);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Lawsuit lawsuit = context.Lawsuits.Find(id);
            if (lawsuit != null)
            {
                context.Lawsuits.Remove(lawsuit);
                context.SaveChanges();
            }
        }

        public void update(Lawsuit lawsuit)
        {
            var par = context.Lawsuits.Attach(lawsuit);
            par.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}