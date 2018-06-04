using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TypeOfState
    {
        public override string ToString()
        {
            return $"{this.StateId} {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is TypeOfState && obj != null)
            {
                TypeOfState temp = (TypeOfState)obj;
                if (temp.StateId == this.StateId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
