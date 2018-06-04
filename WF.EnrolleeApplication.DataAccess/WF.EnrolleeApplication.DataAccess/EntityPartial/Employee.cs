using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Employee
    {
        public override string ToString()
        {
            return $"{this.Fullname} — {this.Username}.";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if(obj is Employee && obj != null)
            {
                Employee temp = (Employee)obj;
                if (temp.EmployeeId == this.EmployeeId && temp.PostId == this.PostId && temp.Fullname == this.Fullname && temp.Username == this.Username && temp.Password == this.Password && temp.CreateDate == this.CreateDate && temp.Enabled == this.Enabled) return true;
                else return false;
            }
            return false;
        }
    }
}
