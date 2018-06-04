using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class EstimationString
    {
        public override string ToString()
        {
            return $"{this.EstimationNumber} = {this.EstimationText}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is EstimationString && obj != null)
            {
                EstimationString temp = (EstimationString)obj;
                if (temp.EstimationStringId == this.EstimationStringId && temp.EstimationNumber == this.EstimationNumber && temp.EstimationText == this.EstimationText) return true;
                else return false;
            }
            return false;
        }
    }
}
