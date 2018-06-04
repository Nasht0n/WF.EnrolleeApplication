using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITypeOfStateService
    {
        TypeOfState InsertTypeOfState(TypeOfState typeOfState);
        TypeOfState UpdateTypeOfState(TypeOfState typeOfState);
        void DeleteTypeOfState(TypeOfState typeOfState);
        List<TypeOfState> GetTypeOfStates();
        TypeOfState GetTypeOfState(int id);
        TypeOfState GetTypeOfState(string name);
    }
}
