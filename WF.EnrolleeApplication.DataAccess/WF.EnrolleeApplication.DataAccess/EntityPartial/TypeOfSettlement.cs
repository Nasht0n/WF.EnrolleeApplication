using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class TypeOfSettlement
    {
        public override string ToString()
        {
            return $"{this.SettlementTypeId} {this.Fullname}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj is TypeOfSettlement)
            {
                TypeOfSettlement temp = (TypeOfSettlement)obj;
                if (temp.SettlementTypeId == this.SettlementTypeId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname && temp.IsTown == this.IsTown) return true;
                else return false;
            }
            return false;
        }
    }
}
