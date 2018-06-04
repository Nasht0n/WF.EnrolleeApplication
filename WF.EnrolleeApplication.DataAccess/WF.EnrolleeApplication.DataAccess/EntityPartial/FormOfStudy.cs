using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class FormOfStudy
    {
        public override string ToString()
        {
            return $"{this.FormOfStudyId}. {this.Fullname}({this.Shortname}).";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is FormOfStudy && obj != null)
            {
                FormOfStudy temp = (FormOfStudy)obj;
                if (temp.FormOfStudyId == this.FormOfStudyId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname) return true;
                else return false;
            }
            return false;
        }
    }
}
