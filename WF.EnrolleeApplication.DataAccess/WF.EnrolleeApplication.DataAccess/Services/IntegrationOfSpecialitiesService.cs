using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class IntegrationOfSpecialitiesService : IIntegrationOfSpecialitiesService
    {
        private EnrolleeContext context;
        public IntegrationOfSpecialitiesService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            IntegrationOfSpecialities integrationOfSpecialitiesToDelete = context.IntegrationOfSpecialities.AsNoTracking().FirstOrDefault(ios => ios.IntegrationId == integrationOfSpecialities.IntegrationId);
            context.IntegrationOfSpecialities.Remove(integrationOfSpecialitiesToDelete);
            context.SaveChanges();
        }

        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities()
        {
            List<IntegrationOfSpecialities> integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().ToList();
            return integrationOfSpecialities;
        }

        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(Speciality speciality)
        {
            List<IntegrationOfSpecialities> integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().Where(ios => ios.FirstSpecialityId == speciality.SpecialityId).ToList();
            return integrationOfSpecialities;
        }

        public List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(SecondarySpeciality secondarySpeciality)
        {
            List<IntegrationOfSpecialities> integrationOfSpecialities = context.IntegrationOfSpecialities.AsNoTracking().Where(ios => ios.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId).ToList();
            return integrationOfSpecialities;
        }

        public IntegrationOfSpecialities GetIntegrationOfSpecialities(int id)
        {
            IntegrationOfSpecialities integrationOfSpeciality = context.IntegrationOfSpecialities.AsNoTracking().FirstOrDefault(ios => ios.IntegrationId == id);
            return integrationOfSpeciality;
        }

        public IntegrationOfSpecialities GetIntegrationOfSpecialities(Speciality speciality, SecondarySpeciality secondarySpeciality)
        {
            IntegrationOfSpecialities integrationOfSpeciality = context.IntegrationOfSpecialities.AsNoTracking().FirstOrDefault(ios => ios.FirstSpecialityId == speciality.SpecialityId && ios.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
            return integrationOfSpeciality;
        }

        public IntegrationOfSpecialities InsertIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            context.IntegrationOfSpecialities.Add(integrationOfSpecialities);
            context.SaveChanges();
            return integrationOfSpecialities;
        }

        public IntegrationOfSpecialities UpdateIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities)
        {
            IntegrationOfSpecialities integrationOfSpecialitiesToUpdate = context.IntegrationOfSpecialities.FirstOrDefault(ios => ios.IntegrationId == integrationOfSpecialities.IntegrationId);
            integrationOfSpecialitiesToUpdate.FirstSpecialityId = integrationOfSpecialities.FirstSpecialityId;
            integrationOfSpecialitiesToUpdate.SecondarySpecialityId = integrationOfSpecialities.SecondarySpecialityId;
            context.SaveChanges();
            return integrationOfSpecialitiesToUpdate;
        }
    }
}
