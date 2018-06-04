using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ISecondarySpecialityService
    {
        SecondarySpeciality InsertSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        SecondarySpeciality UpdateSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        void DeleteSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        List<SecondarySpeciality> GetSecondarySpecialities();
        SecondarySpeciality GetSecondarySpeciality(int id);
        SecondarySpeciality GetSecondarySpeciality(string cipher);
    }
}
