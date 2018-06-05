namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialityView")]
    public partial class SpecialityView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpecialityId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacultyId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string Faculty { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FormOfStudyId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string FormOfStudy { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string Cipher { get; set; }

        [Key]
        [Column(Order = 6)]
        public string Speciality { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsGroup { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BudgetCountPlace { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeCountPlace { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TargetCountPlace { get; set; }
    }
}
