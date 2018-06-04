namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamSchema")]
    public partial class ExamSchema
    {
        public int ExamSchemaId { get; set; }

        public int SpecialityId { get; set; }

        public int DisciplineId { get; set; }

        public int DisciplineRank { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
