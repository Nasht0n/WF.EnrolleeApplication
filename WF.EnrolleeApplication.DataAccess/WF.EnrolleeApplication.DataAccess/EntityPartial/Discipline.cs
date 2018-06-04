using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Discipline
    {
        public override string ToString()
        {
            return $"{this.DisciplineId}. {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Discipline && obj!=null)
            {
                Discipline temp = (Discipline)obj;
                if (temp.DisciplineId == this.DisciplineId && temp.BasisForAssessingId == this.BasisForAssessingId && temp.Name == this.Name && this.IsGroup == temp.IsGroup && temp.IsAlternative == this.IsAlternative
                    && temp.DisciplineGroupId == this.DisciplineGroupId && temp.ConsultDate == this.ConsultDate && temp.EntryExamDate == this.EntryExamDate && temp.StageCount == this.StageCount) return true;
                else return false;
            }
            return false;
        }
    }
}
