using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ICitizenshipService
    {
        Citizenship InsertCitizenship(Citizenship citizenship);
        Citizenship UpdateCitizenship(Citizenship citizenship);
        void DeleteCitizenship(Citizenship citizenship);
        List<Citizenship> GetCitizenships();
        Citizenship GetCitizenship(int id);
        Citizenship GetCitizenship(string fullname);
    }
}
