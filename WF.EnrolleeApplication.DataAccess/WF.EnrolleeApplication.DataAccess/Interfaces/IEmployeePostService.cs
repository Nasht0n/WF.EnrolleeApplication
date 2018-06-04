using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IEmployeePostService
    {
        EmployeePost InsertEmployeePost(EmployeePost post);
        EmployeePost UpdateEmployeePost(EmployeePost post);
        void DeleteEmployeePost(EmployeePost post);
        List<EmployeePost> GetEmployeePosts();
        EmployeePost GetEmployeePost(int id);
        EmployeePost GetEmployeePost(string name);
    }
}
