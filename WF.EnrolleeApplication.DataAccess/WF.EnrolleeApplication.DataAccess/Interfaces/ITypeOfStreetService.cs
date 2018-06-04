using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITypeOfStreetService
    {
        TypeOfStreet InsertTypeOfStreet(TypeOfStreet typeOfStreet);
        TypeOfStreet UpdateTypeOfStreet(TypeOfStreet typeOfStreet);
        void DeleteTypeOfStreet(TypeOfStreet typeOfStreet);
        List<TypeOfStreet> GetTypeOfStreets();
        TypeOfStreet GetTypeOfStreet(int id);
        TypeOfStreet GetTypeOfStreet(string fullname);
    }
}
