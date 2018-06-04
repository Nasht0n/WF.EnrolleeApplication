namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Atribute")]
    public partial class Atribute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Atribute()
        {
            AtributeForEnrollee = new HashSet<AtributeForEnrollee>();
        }

        public int AtributeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(10)]
        public string Shortname { get; set; }

        public bool IsDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AtributeForEnrollee> AtributeForEnrollee { get; set; }
    }
}
