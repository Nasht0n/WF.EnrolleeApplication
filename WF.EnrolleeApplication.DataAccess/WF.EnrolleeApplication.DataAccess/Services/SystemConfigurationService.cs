using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class SystemConfigurationService : ISystemConfigurationService
    {
        private EnrolleeContext context;
        public SystemConfigurationService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            SystemConfiguration systemConfigurationToDelete = context.SystemConfiguration.FirstOrDefault(sc => sc.Name == systemConfiguration.Name);
            context.SystemConfiguration.Remove(systemConfigurationToDelete);
            context.SaveChanges();
        }

        public SystemConfiguration GetSystemConfiguration(string name)
        {
            SystemConfiguration systemConfiguration = context.SystemConfiguration.FirstOrDefault(sc => sc.Name == name);
            return systemConfiguration;
        }

        public List<SystemConfiguration> GetSystemConfigurations()
        {
            List<SystemConfiguration> systemConfigurations = context.SystemConfiguration.ToList();
            return systemConfigurations;
        }

        public SystemConfiguration InsertSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            context.SystemConfiguration.Add(systemConfiguration);
            context.SaveChanges();
            return systemConfiguration;
        }

        public SystemConfiguration UpdateSystemConfiguration(SystemConfiguration systemConfiguration)
        {
            SystemConfiguration systemConfigurationToUpdate = context.SystemConfiguration.FirstOrDefault(sc => sc.Name == systemConfiguration.Name);
            systemConfigurationToUpdate.Title = systemConfiguration.Title;
            systemConfigurationToUpdate.Value = systemConfiguration.Value;
            context.SaveChanges();
            return systemConfigurationToUpdate;
        }
    }
}
