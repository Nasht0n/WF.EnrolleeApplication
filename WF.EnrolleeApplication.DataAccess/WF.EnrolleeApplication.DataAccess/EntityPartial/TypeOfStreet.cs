using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TypeOfStreet
    {
        public override string ToString()
        {
            return $"{this.SteetTypeId} {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is TypeOfStreet && obj != null)
            {
                TypeOfStreet temp = (TypeOfStreet)obj;
                if (temp.SteetTypeId == this.SteetTypeId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname) return true;
                else return false;
            }
            return false;
        }
    }
}
