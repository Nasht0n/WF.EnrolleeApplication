using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class District
    {
        public override string ToString()
        {
            return $"{this.DistrictId}. {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is District && obj !=null)
            {
                District temp = (District)obj;
                if (temp.DistrictId == this.DistrictId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
