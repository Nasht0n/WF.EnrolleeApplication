using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class ExamSchema
    {
        public override string ToString()
        {
            return $"{this.Speciality.Fullname} — {this.DisciplineRank}.{this.Discipline.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is ExamSchema && obj != null)
            {
                ExamSchema temp = (ExamSchema)obj;
                if (temp.ExamSchemaId == this.ExamSchemaId && temp.SpecialityId == this.SpecialityId && temp.DisciplineId == this.DisciplineId && temp.DisciplineRank == this.DisciplineRank) return true;
                else return false;
            }
            return false;
        }
    }
}
