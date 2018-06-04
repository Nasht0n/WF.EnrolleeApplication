using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class PriorityOfSpeciality
    {
        public override string ToString()
        {
            return $"{this.Enrollee.RuSurname} {this.Enrollee.RuName} — {this.Speciality.Fullname} — {this.PriorityLevel}";
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is PriorityOfSpeciality && obj != null)
            {
                PriorityOfSpeciality temp = (PriorityOfSpeciality)obj;
                if (temp.PriorityId == this.PriorityId && temp.EnrolleeId == this.EnrolleeId && temp.SpecialityId == this.SpecialityId && temp.PriorityLevel == this.PriorityLevel) return true;
                else return false;
            }
            return false;
        }
    }
}
