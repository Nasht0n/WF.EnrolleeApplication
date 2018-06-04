using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Document
    {
        public override string ToString()
        {
            return $"{this.DocumentId}. {this.Name}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Document && obj != null)
            {
                Document temp = (Document)obj;
                if (temp.DocumentId == this.DocumentId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
