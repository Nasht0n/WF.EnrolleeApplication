using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class ForeignLanguage
    {
        public override string ToString()
        {
            return $"{this.LanguageId}. {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is ForeignLanguage && obj != null)
            {
                ForeignLanguage temp = (ForeignLanguage)obj;
                if (temp.LanguageId == this.LanguageId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
