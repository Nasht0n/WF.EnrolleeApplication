namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EnrolleeContext : DbContext
    {
        public EnrolleeContext(string EnrolleeContext)
            : base("name=EnrolleeContext")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Assessment> Assessment { get; set; }
        public virtual DbSet<Atribute> Atribute { get; set; }
        public virtual DbSet<AtributeForEnrollee> AtributeForEnrollee { get; set; }
        public virtual DbSet<BasisForAssessing> BasisForAssessing { get; set; }
        public virtual DbSet<Citizenship> Citizenship { get; set; }
        public virtual DbSet<Contest> Contest { get; set; }
        public virtual DbSet<ConversionSystem> ConversionSystem { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Decree> Decree { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeePost> EmployeePost { get; set; }
        public virtual DbSet<Enrollee> Enrollee { get; set; }
        public virtual DbSet<EstimationString> EstimationString { get; set; }
        public virtual DbSet<ExamSchema> ExamSchema { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<ForeignLanguage> ForeignLanguage { get; set; }
        public virtual DbSet<FormOfStudy> FormOfStudy { get; set; }
        public virtual DbSet<IntegrationOfSpecialities> IntegrationOfSpecialities { get; set; }
        public virtual DbSet<PriorityOfSpeciality> PriorityOfSpeciality { get; set; }
        public virtual DbSet<ReasonForAddmission> ReasonForAddmission { get; set; }
        public virtual DbSet<SecondarySpeciality> SecondarySpeciality { get; set; }
        public virtual DbSet<Speciality> Speciality { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public virtual DbSet<TargetWorkPlace> TargetWorkPlace { get; set; }
        public virtual DbSet<TypeOfFinance> TypeOfFinance { get; set; }
        public virtual DbSet<TypeOfSchool> TypeOfSchool { get; set; }
        public virtual DbSet<TypeOfSettlement> TypeOfSettlement { get; set; }
        public virtual DbSet<TypeOfState> TypeOfState { get; set; }
        public virtual DbSet<TypeOfStreet> TypeOfStreet { get; set; }
        public virtual DbSet<EmployeeView> EmployeeView { get; set; }
        public virtual DbSet<EnrolleeView> EnrolleeView { get; set; }
        public virtual DbSet<SpecialityView> SpecialityView { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atribute>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<Citizenship>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<Decree>()
                .Property(e => e.DecreeNumber)
                .IsFixedLength();

            modelBuilder.Entity<Decree>()
                .Property(e => e.ProtocolNumber)
                .IsFixedLength();

            modelBuilder.Entity<Decree>()
                .HasMany(e => e.Enrollee)
                .WithOptional(e => e.Decree)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Enrollee>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<Enrollee>()
                .Property(e => e.DocumentSeria)
                .IsFixedLength();

            modelBuilder.Entity<Enrollee>()
                .Property(e => e.SchoolYear)
                .IsFixedLength();

            modelBuilder.Entity<Enrollee>()
                .Property(e => e.Seniority)
                .IsFixedLength();

            modelBuilder.Entity<Enrollee>()
                .Property(e => e.CurrentNumberCurs)
                .IsFixedLength();

            modelBuilder.Entity<Faculty>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<ForeignLanguage>()
                .HasMany(e => e.Enrollee)
                .WithRequired(e => e.ForeignLanguage)
                .HasForeignKey(e => e.ForeignLanguageId);

            modelBuilder.Entity<FormOfStudy>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<ReasonForAddmission>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<SecondarySpeciality>()
                .HasMany(e => e.Enrollee)
                .WithOptional(e => e.SecondarySpeciality)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Speciality>()
                .HasMany(e => e.IntegrationOfSpecialities)
                .WithRequired(e => e.Speciality)
                .HasForeignKey(e => e.FirstSpecialityId);

            modelBuilder.Entity<Speciality>()
                .HasMany(e => e.PriorityOfSpeciality)
                .WithRequired(e => e.Speciality)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TargetWorkPlace>()
                .HasMany(e => e.Enrollee)
                .WithOptional(e => e.TargetWorkPlace)
                .HasForeignKey(e => e.TargetWorkPlaceId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TypeOfSettlement>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<TypeOfState>()
                .HasMany(e => e.Enrollee)
                .WithRequired(e => e.TypeOfState)
                .HasForeignKey(e => e.StateTypeId);

            modelBuilder.Entity<TypeOfStreet>()
                .Property(e => e.Shortname)
                .IsFixedLength();

            modelBuilder.Entity<EnrolleeView>()
                .Property(e => e.FormOfStudyShortname)
                .IsFixedLength();
        }
    }
}
