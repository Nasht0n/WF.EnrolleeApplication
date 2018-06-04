namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Speciality")]
    public partial class Speciality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Speciality()
        {
            Enrollee = new HashSet<Enrollee>();
            ExamSchema = new HashSet<ExamSchema>();
            IntegrationOfSpecialities = new HashSet<IntegrationOfSpecialities>();
            PriorityOfSpeciality = new HashSet<PriorityOfSpeciality>();
        }

        public int SpecialityId { get; set; }

        public int FacultyId { get; set; }

        public int FormOfStudyId { get; set; }

        [Required]
        public string Fullname { get; set; }

        public string Specialization { get; set; }

        [StringLength(255)]
        public string Cipher { get; set; }

        [Required]
        [StringLength(25)]
        public string Shortname { get; set; }

        public int BudgetCountPlace { get; set; }

        public int FeeCountPlace { get; set; }

        public int TargetCountPlace { get; set; }

        public bool IsGroup { get; set; }

        public bool IsAlternative { get; set; }

        public int? SpecialityGroupId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollee> Enrollee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSchema> ExamSchema { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual FormOfStudy FormOfStudy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntegrationOfSpecialities> IntegrationOfSpecialities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriorityOfSpeciality> PriorityOfSpeciality { get; set; }
    }
}
