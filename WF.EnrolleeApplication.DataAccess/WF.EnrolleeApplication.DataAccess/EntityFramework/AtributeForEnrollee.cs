namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AtributeForEnrollee")]
    public partial class AtributeForEnrollee
    {
        public int Id { get; set; }

        public int AtributeId { get; set; }

        public int EnrolleeId { get; set; }

        public virtual Atribute Atribute { get; set; }

        public virtual Enrollee Enrollee { get; set; }
    }
}
