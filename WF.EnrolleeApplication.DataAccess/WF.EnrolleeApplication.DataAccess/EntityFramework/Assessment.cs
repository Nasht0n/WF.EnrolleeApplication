namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment")]
    public partial class Assessment
    {
        public int AssessmentId { get; set; }

        public int DisciplineId { get; set; }

        public int EnrolleeId { get; set; }

        public int? Estimation { get; set; }

        [StringLength(255)]
        public string SertCode { get; set; }

        [StringLength(255)]
        public string SertDate { get; set; }

        [StringLength(255)]
        public string ChangeDiscipline { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual Enrollee Enrollee { get; set; }
    }
}
