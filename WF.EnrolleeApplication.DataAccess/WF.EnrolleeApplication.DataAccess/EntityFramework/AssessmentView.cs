namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AssessmentView")]
    public partial class AssessmentView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DisciplineId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnrolleeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string RuSurname { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string RuName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(25)]
        public string Speciality { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string FormOfStudy { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfDeal { get; set; }

        public int? Estimation { get; set; }
    }
}
