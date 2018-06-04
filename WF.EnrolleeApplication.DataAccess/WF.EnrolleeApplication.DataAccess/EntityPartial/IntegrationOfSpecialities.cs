using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class IntegrationOfSpecialities
    {
        public override string ToString()
        {
            return $"{this.Speciality.Fullname} — {this.SecondarySpeciality.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is IntegrationOfSpecialities && obj != null)
            {
                IntegrationOfSpecialities temp = (IntegrationOfSpecialities)obj;
                if (temp.IntegrationId == this.IntegrationId && temp.FirstSpecialityId == this.FirstSpecialityId && temp.SecondarySpecialityId == this.SecondarySpecialityId) return true;
                else return false;
            }
            return base.Equals(obj);
        }
    }
}
