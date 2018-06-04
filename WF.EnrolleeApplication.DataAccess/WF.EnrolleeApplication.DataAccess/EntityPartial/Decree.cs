using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Decree
    {
        public override string ToString()
        {
            return $"Приказ №{this.DecreeNumber} от {this.DecreeDate.ToShortDateString()}. Протокол №{this.ProtocolNumber} от {this.ProtocolDate.ToShortDateString()} ";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Decree && obj != null)
            {
                Decree temp = (Decree)obj;
                if (temp.DecreeId == this.DecreeId && temp.DecreeNumber == this.DecreeNumber && temp.DecreeDate == this.DecreeDate && temp.ProtocolNumber == this.ProtocolNumber && temp.ProtocolDate == this.ProtocolDate && temp.Content == this.Content) return true;
                else return false;
            }
            return false;
        }
    }
}
