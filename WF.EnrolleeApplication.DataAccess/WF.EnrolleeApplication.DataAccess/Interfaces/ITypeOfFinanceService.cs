using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITypeOfFinanceService
    {
        TypeOfFinance InsertTypeOfFinance(TypeOfFinance typeOfFinance);
        TypeOfFinance UpdateTypeOfFinance(TypeOfFinance typeOfFinance);
        void DeleteTypeOfFinance(TypeOfFinance typeOfFinance);
        List<TypeOfFinance> GetTypeOfFinances();
        TypeOfFinance GetTypeOfFinance(int id);
        TypeOfFinance GetTypeOfFinance(string fullname);
    }
}
