using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITypeOfSettlementService
    {
        TypeOfSettlement InsertTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        TypeOfSettlement UpdateTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        void DeleteTypeOfSettlement(TypeOfSettlement typeOfSettlement);
        List<TypeOfSettlement> GetTypeOfSettlements();
        TypeOfSettlement GetTypeOfSettlement(int id);
        TypeOfSettlement GetTypeOfSettlement(string fullname);
    }
}
