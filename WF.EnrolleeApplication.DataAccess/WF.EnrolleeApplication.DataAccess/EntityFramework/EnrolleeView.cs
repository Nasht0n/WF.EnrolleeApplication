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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpecialityId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContestId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReasonForAddmissionId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinanceTypeId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(255)]
        public string FormOfStudy { get; set; }

        [Key]
        [Column(Order = 8)]
        public string Speciality { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(255)]
        public string Surname { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(255)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(255)]
        public string Contest { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(255)]
        public string ReasonForAddmission { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(255)]
        public string Finance { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfDeal { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(255)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(25)]
        public string SpecialityShortname { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(10)]
        public string FormOfStudyShortname { get; set; }
    }
}
