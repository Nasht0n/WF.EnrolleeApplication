namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntegrationOfSpecialities
    {
        [Key]
        public int IntegrationId { get; set; }

        public int FirstSpecialityId { get; set; }

        public int SecondarySpecialityId { get; set; }

        public virtual SecondarySpeciality SecondarySpeciality { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
