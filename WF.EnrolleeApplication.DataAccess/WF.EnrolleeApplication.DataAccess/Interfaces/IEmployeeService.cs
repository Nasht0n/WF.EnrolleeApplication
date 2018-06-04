using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IEmployeeService
    {
        Employee InsertEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        List<Employee> GetEmployees();
        List<Employee> GetEmployees(EmployeePost post);
        Employee GetEmployee(int id);
        Employee GetEmployee(string username, string password);
    }
}
