using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            EnrolleeContext enrolleeContext = new EnrolleeContext(connectionString);
            IntegrationOfSpecialitiesService integrationOfSpecialitiesService = new IntegrationOfSpecialitiesService(connectionString);
            List<IntegrationOfSpecialities> list = integrationOfSpecialitiesService.GetIntegrationOfSpecialities();

            foreach(var item in list)
            {
                Console.WriteLine(item.Speciality.Fullname+" "+item.SecondarySpeciality.Fullname);
            }
            Console.ReadKey();
        }
    }
}
