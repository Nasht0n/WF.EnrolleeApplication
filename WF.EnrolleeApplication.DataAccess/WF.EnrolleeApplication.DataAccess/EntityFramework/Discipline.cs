namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discipline")]
    public partial class Discipline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discipline()
        {
            Assessment = new HashSet<Assessment>();
            ExamSchema = new HashSet<ExamSchema>();
        }

        public int DisciplineId { get; set; }

        public int? BasisForAssessingId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsGroup { get; set; }

        public bool IsAlternative { get; set; }

        public int? DisciplineGroupId { get; set; }

        public string ConsultDate { get; set; }

        public string EntryExamDate { get; set; }

        public int? StageCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assessment> Assessment { get; set; }

        public virtual BasisForAssessing BasisForAssessing { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSchema> ExamSchema { get; set; }
    }
}
