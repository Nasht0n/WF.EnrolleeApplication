namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecondarySpeciality")]
    public partial class SecondarySpeciality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecondarySpeciality()
        {
            Enrollee = new HashSet<Enrollee>();
            IntegrationOfSpecialities = new HashSet<IntegrationOfSpecialities>();
        }

        public int SecondarySpecialityId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [StringLength(255)]
        public string Cipher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollee> Enrollee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntegrationOfSpecialities> IntegrationOfSpecialities { get; set; }
    }
}
