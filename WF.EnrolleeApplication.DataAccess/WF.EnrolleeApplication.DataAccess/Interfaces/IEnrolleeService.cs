using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IEnrolleeService
    {
        Enrollee InsertEnrollee(Enrollee enrollee);
        Enrollee UpdateEnrollee(Enrollee enrollee);
        void DeleteEnrollee(Enrollee enrollee);
        List<Enrollee> GetEnrollees();
        List<Enrollee> GetEnrollees(Speciality speciality);
        List<Enrollee> GetEnrollees(Citizenship citizenship);
        List<Enrollee> GetEnrollees(Country country);
        List<Enrollee> GetEnrollees(Area area);
        List<Enrollee> GetEnrollees(District district);
        List<Enrollee> GetEnrollees(TypeOfSchool typeOfSchool);
        List<Enrollee> GetEnrollees(ReasonForAddmission reasonForAddmission);
        List<Enrollee> GetEnrollees(TypeOfState typeOfState);
        List<Enrollee> GetEnrollees(Employee employee);
        List<Enrollee> GetEnrollees(Decree decree);
        List<Enrollee> GetEnrollees(TypeOfFinance typeOfFinance);
        Enrollee GetEnrollee(int id);
        Enrollee GetEnrollee(string documentPersonalNumber);
    }
}
