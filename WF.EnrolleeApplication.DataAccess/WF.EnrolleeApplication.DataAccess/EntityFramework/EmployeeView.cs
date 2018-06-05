namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeView")]
    public partial class EmployeeView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool Enabled { get; set; }
    }
}
