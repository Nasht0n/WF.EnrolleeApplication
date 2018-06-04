using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IFacultyService
    {
        Faculty InsertFaculty(Faculty faculty);
        Faculty UpdateFaculty(Faculty faculty);
        void DeleteFaculty(Faculty faculty);
        List<Faculty> GetFaculties();
        Faculty GetFaculty(int id);
        Faculty GetFaculty(string fullname);
    }
}
