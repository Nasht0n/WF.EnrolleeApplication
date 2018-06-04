using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class SystemConfiguration
    {
        public override string ToString()
        {
            return $"{this.Title} — {this.Value}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is SystemConfiguration && obj != null)
            {
                SystemConfiguration temp = (SystemConfiguration)obj;
                if (temp.Name == this.Name && temp.Title == this.Title && temp.Value == this.Value) return true;
                else return false;
            }
            return false;
        }
    }
}
