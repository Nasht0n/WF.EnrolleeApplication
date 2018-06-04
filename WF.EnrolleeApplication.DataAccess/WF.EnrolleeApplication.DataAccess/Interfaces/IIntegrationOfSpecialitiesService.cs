using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IIntegrationOfSpecialitiesService
    {
        IntegrationOfSpecialities InsertIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        IntegrationOfSpecialities UpdateIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        void DeleteIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities();
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(Speciality speciality);
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(SecondarySpeciality secondarySpeciality);
        IntegrationOfSpecialities GetIntegrationOfSpecialities(int id);
        IntegrationOfSpecialities GetIntegrationOfSpecialities(Speciality speciality, SecondarySpeciality secondarySpeciality);
    }
}
