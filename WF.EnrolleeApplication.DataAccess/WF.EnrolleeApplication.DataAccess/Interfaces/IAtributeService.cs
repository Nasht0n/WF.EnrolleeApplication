using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IAtributeService
    {
        Atribute InsertAtribute(Atribute atribute);
        Atribute UpdateAtribute(Atribute atribute);
        void DeleteAtribute(Atribute atribute);
        List<Atribute> GetAtributes();
        List<Atribute> GetAtributes(bool IsDiscount);
        Atribute GetAtribute(int id);
        Atribute GetAtribute(string fullname);
    }
}
