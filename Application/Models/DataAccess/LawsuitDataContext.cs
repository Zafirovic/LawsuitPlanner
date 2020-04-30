using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Models.DataAccess
{
    public class LawsuitDataContext : IdentityDbContext<User>
    {
        public LawsuitDataContext(DbContextOptions<LawsuitDataContext> options): base(options)
        {
            
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TypeOfProcess> TypeOfProcesses { get; set; }
        public DbSet<Lawsuit> Lawsuits { get; set; }
        public DbSet<LawsuitLawyer> LawsuitLawyers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }

            builder.Entity<LawsuitLawyer>()
                    .HasOne(x => x.lawsuit)
                    .WithMany(l => l.lawyers)
                    .HasForeignKey(x => x.lawsuitId);

            builder.Entity<LawsuitLawyer>()
                    .HasOne(x => x.user)
                    .WithMany(l => l.lawsuit)
                    .HasForeignKey(x => x.userId);

        }
    }
}