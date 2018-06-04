using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TargetWorkPlace
    {
        public override string ToString()
        {
            return $"{this.TargetId} {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is TargetWorkPlace && obj != null)
            {
                TargetWorkPlace temp = (TargetWorkPlace)obj;
                if (temp.TargetId == this.TargetId && temp.Name == this.Name) return true;
                return false;
            }
            return false;
        }
    }
}
