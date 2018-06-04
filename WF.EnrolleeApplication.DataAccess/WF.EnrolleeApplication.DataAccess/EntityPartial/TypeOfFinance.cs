using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TypeOfFinance
    {
        public override string ToString()
        {
            return $"{this.FinanceTypeId} {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is TypeOfFinance && obj != null)
            {
                TypeOfFinance temp = (TypeOfFinance)obj;
                if (temp.FinanceTypeId == this.FinanceTypeId && temp.Fullname == this.Fullname) return true;
                else return false;
            }
            return false;
        }
    }
}
