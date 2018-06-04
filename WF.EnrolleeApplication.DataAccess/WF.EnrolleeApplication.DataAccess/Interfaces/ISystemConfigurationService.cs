using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ISystemConfigurationService
    {
        SystemConfiguration InsertSystemConfiguration(SystemConfiguration systemConfiguration);
        SystemConfiguration UpdateSystemConfiguration(SystemConfiguration systemConfiguration);
        void DeleteSystemConfiguration(SystemConfiguration systemConfiguration);
        List<SystemConfiguration> GetSystemConfigurations();
        SystemConfiguration GetSystemConfiguration(string name);
    }
}
