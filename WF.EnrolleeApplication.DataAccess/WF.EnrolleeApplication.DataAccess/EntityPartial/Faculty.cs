using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Faculty
    {
        public override string ToString()
        {
            return $"{this.FacultyId}. {this.Fullname}({this.Shortname}).";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Faculty && obj != null)
            {
                Faculty temp = (Faculty)obj;
                if (temp.FacultyId == this.FacultyId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname) return true;
                else return false;
            }
            return false;
        }
    }
}
