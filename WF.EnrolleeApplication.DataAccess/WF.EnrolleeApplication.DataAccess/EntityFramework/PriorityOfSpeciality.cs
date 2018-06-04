namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriorityOfSpeciality")]
    public partial class PriorityOfSpeciality
    {
        [Key]
        public int PriorityId { get; set; }

        public int EnrolleeId { get; set; }

        public int SpecialityId { get; set; }

        public int PriorityLevel { get; set; }

        public virtual Enrollee Enrollee { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
