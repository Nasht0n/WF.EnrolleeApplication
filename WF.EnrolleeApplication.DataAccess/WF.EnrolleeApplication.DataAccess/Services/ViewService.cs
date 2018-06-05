using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ViewService : IViewService
    {
        private EnrolleeContext context;
        public ViewService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public List<EmployeeView> GetEmployees()
        {
            List<EmployeeView> employees = context.EmployeeView.ToList();
            return employees;
        }

        public List<EnrolleeView> GetEnrollees()
        {
            List<EnrolleeView> enrollees = context.EnrolleeView.ToList();
            return enrollees;
        }

        public List<SpecialityView> GetSpecialities()
        {
            List<SpecialityView> specialities = context.SpecialityView.ToList();
            return specialities;
        }
    }
}
