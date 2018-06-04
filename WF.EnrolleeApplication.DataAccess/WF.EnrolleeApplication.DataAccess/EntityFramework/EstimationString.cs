namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstimationString")]
    public partial class EstimationString
    {
        public int EstimationStringId { get; set; }

        public int EstimationNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string EstimationText { get; set; }
    }
}
