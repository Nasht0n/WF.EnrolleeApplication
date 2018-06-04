using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class SecondarySpeciality
    {
        public override string ToString()
        {
            return $"{this.SecondarySpecialityId}. {this.Cipher} {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is SecondarySpeciality && obj != null)
            {
                SecondarySpeciality temp = (SecondarySpeciality)obj;
                if (temp.SecondarySpecialityId == this.SecondarySpecialityId && temp.Fullname == this.Fullname && temp.Cipher == this.Cipher) return true;
                else return false;
            }
            return false;
        }
    }
}
