using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Speciality
    {
        public override string ToString()
        {
            return $"{this.SpecialityId}. {this.Cipher} {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Speciality && obj != null)
            {
                Speciality temp = (Speciality)obj;
                if (temp.SpecialityId == this.SpecialityId && temp.FacultyId == this.FacultyId && temp.FormOfStudyId == this.FormOfStudyId && temp.Fullname == this.Fullname && temp.Specialization == this.Specialization &&
                   temp.Cipher == this.Cipher && temp.Shortname == this.Shortname && temp.BudgetCountPlace == this.BudgetCountPlace && temp.FeeCountPlace == this.FeeCountPlace && temp.TargetCountPlace == this.TargetCountPlace &&
                   temp.IsGroup == this.IsGroup && temp.IsAlternative == this.IsAlternative && temp.SpecialityGroupId == this.SpecialityGroupId) return true;
                else return false;
            }
            return false;
        }
    }
}
