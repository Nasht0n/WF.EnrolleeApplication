using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITypeOfSchoolService
    {
        TypeOfSchool InsertTypeOfSchool(TypeOfSchool typeOfSchool);
        TypeOfSchool UpdateTypeOfSchool(TypeOfSchool typeOfSchool);
        void DeleteTypeOfSchool(TypeOfSchool typeOfSchool);
        List<TypeOfSchool> GetTypeOfSchools();
        TypeOfSchool GetTypeOfSchool(int id);
        TypeOfSchool GetTypeOfSchool(string name);
    }
}
