using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TypeOfSchool
    {
        public override string ToString()
        {
            return $"{this.SchoolTypeId} {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is TypeOfSchool && obj != null)
            {
                TypeOfSchool temp = (TypeOfSchool)obj;
                if (temp.SchoolTypeId == this.SchoolTypeId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
