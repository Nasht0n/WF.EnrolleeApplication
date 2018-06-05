namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnrolleeView")]
    public partial class EnrolleeView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnrolleeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string FormOfStudy { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Speciality { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string Surname { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string Contest { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(255)]
        public string Finance { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfDeal { get; set; }
    }
}
