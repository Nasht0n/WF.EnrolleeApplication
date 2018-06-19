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

        public List<AssessmentView> GetAssessments()
        {
            List<AssessmentView> assessments = context.AssessmentView.AsNoTracking().ToList();
            return assessments;
        }

        public List<AssessmentView> GetAssessments(Discipline discipline)
        {
            List<AssessmentView> assessments = context.AssessmentView.AsNoTracking().Where(a=>a.DisciplineId == discipline.DisciplineId).ToList();
            return assessments;
        }

        public List<EmployeeView> GetEmployees()
        {
            List<EmployeeView> employees = context.EmployeeView.AsNoTracking().ToList();
            return employees;
        }

        public List<EnrolleeView> GetEnrollees(Employee employee)
        {
            List<EnrolleeView> enrollees = context.EnrolleeView.AsNoTracking().Where(e=>e.EmployeeId == employee.EmployeeId).OrderByDescending(e=>e.EnrolleeId).ToList();
            return enrollees;
        }

        public List<EnrolleeView> GetEnrollees(Speciality speciality)
        {
            List<EnrolleeView> enrollees = context.EnrolleeView.AsNoTracking().Where(e=>e.SpecialityId == speciality.SpecialityId).OrderBy(e=>e.NumberOfDeal).ToList();
            return enrollees;
        }

        public List<PriorityView> GetPriorities(Enrollee enrollee)
        {
            List<PriorityView> priorities = context.PriorityView.AsNoTracking().Where(p => p.EnrolleeId == enrollee.EnrolleeId).ToList();
            return priorities;
        }

        public List<SpecialityView> GetSpecialities()
        {
            List<SpecialityView> specialities = context.SpecialityView.AsNoTracking().ToList();
            return specialities;
        }
    }
}
