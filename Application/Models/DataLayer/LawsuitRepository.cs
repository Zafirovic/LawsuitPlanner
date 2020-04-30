using System.Collections.Generic;
using Application.Models.DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

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

        public Lawsuit add(Lawsuit lawsuit)
        {
            context.Lawsuits.Add(lawsuit);
            context.SaveChanges();
            return lawsuit;
        }

        public Lawsuit delete(int id)
        {
            Lawsuit lawsuit = context.Lawsuits.Find(id);
            if (lawsuit != null)
            {
                context.Lawsuits.Remove(lawsuit);
                context.SaveChanges();
            }
            return lawsuit;
        }

        public Lawsuit get(int id)
        {
            return context.Lawsuits.Find(id);
        }

        //vraca parnice samo za trenutno ulogovanog korisnika(advokata)
        public IEnumerable<Lawsuit> getForLawyer(string id)
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

            return lawsuits;
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

        public Lawsuit update(Lawsuit lawsuit)
        {
            var par = context.Lawsuits.Attach(lawsuit);
            par.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return lawsuit;
        }
    }
}