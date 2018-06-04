using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ISpecialityService
    {
        Speciality InsertSpeciality(Speciality speciality);
        Speciality UpdateSpeciality(Speciality speciality);
        void DeleteSpeciality(Speciality speciality);
        List<Speciality> GetSpecialities();
        List<Speciality> GetSpecialities(Faculty faculty);
        List<Speciality> GetSpecialities(FormOfStudy formOfStudy);
        List<Speciality> GetSpecialities(Faculty faculty, FormOfStudy formOfStudy);
        List<Speciality> GetSpecialities(Speciality groupSpeciality);
        Speciality GetSpeciality(int id);
        Speciality GetSpeciality(string cipher);
    }
}
