using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Employee
    {
        public override bool Equals(object obj)
        {
            if(obj is Employee && obj!=null)
            {
                Employee temp = (Employee)obj;
                if (temp.GetHashCode() == this.GetHashCode()) return true;
                else return false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Код пользователя = {this.EmployeeId}" +
                   $"\nКод группы пользователя = {this.PostId}" +
                   $"\nФИО пользователя = {this.Fullname.Trim()}" +
                   $"\n";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
