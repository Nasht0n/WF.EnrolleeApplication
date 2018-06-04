using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class EmployeePost
    {
        public override string ToString()
        {
            return $"{this.PostId}. {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is EmployeePost && obj != null)
            {
                EmployeePost temp = (EmployeePost)obj;
                if (temp.PostId == this.PostId && temp.Name == this.Name && temp.Note == this.Note && temp.RegistrationAllow == this.RegistrationAllow && temp.EnrollAllow == this.EnrollAllow && temp.DictionaryAllow == this.DictionaryAllow) return true;
                else return false;
            }
            return false;
        }
    }
}
