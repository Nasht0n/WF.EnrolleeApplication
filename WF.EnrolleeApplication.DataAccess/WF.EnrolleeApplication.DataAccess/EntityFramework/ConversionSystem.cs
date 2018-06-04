namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConversionSystem")]
    public partial class ConversionSystem
    {
        public int ConversionSystemId { get; set; }

        public double Five { get; set; }

        public double Ten { get; set; }
    }
}
