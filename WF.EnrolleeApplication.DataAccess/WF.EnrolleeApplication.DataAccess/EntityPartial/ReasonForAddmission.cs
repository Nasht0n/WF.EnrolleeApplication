using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class ReasonForAddmission
    {
        public override string ToString()
        {
            return $"{this.ReasonForAddmissionId}.{this.Contest.Name} — {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is ReasonForAddmission && obj != null)
            {
                ReasonForAddmission temp = (ReasonForAddmission)obj;
                if (temp.ReasonForAddmissionId == this.ReasonForAddmissionId && temp.ContestId == this.ContestId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname) return true;
                else return false;
            }
            return false;
        }
    }
}
