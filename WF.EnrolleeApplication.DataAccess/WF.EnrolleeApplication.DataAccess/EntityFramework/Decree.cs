namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Decree")]
    public partial class Decree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Decree()
        {
            Enrollee = new HashSet<Enrollee>();
        }

        public int DecreeId { get; set; }

        [Required]
        [StringLength(10)]
        public string DecreeNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DecreeDate { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(10)]
        public string ProtocolNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime ProtocolDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollee> Enrollee { get; set; }
    }
}
