using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.MIS;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiseaseMIS.BAL.Configurations
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Animals>()
            .HasIndex(u => u.Name)
            .IsUnique();

            builder.Entity<Districts>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<AnimalCategory>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Samples>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Disease>()
                .HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(builder);
        }

        public DbSet<Modules> Modules { get; set; }
        public DbSet<ModuleOperations> ModulesOperations { get; set; }
        public DbSet<RolesPermissionMapper> RolesPermissionMappers { get; set; }
        public DbSet<UserPermissionsMapper> UserPermissionMappers { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<RecentActions> RecentActions { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Institutes> Institutes { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptoms> Symptoms { get; set; }
        public DbSet<DiseaseType> DiseaseType { get; set; }
        public DbSet<Incharges> Incharges { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<ReportCases> ReportCases { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        public DbSet<Animals> Animals { get; set; }
        public DbSet<Samples> Samples { get; set; }
        public DbSet<TestTypes> TestTypes { get; set; }
        public DbSet<DiseaseForms> DiseaseForms { get; set; }
        public DbSet<FormDiseaseValues> FormDiseaseValues { get; set; }
        public DbSet<LaboratoryForms> LaboratoryForms { get; set; }
        public DbSet<LabFormValues> LabFormValues { get; set; }
    }
}
