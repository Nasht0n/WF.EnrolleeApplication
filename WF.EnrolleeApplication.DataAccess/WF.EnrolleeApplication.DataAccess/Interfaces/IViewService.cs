using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IViewService
    {     
        List<EmployeeView> GetEmployees();
        List<EnrolleeView> GetEnrollees();
        List<SpecialityView> GetSpecialities();
    }
}
