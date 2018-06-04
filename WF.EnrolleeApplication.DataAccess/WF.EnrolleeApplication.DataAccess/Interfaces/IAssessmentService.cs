using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IAssessmentService
    {
        Assessment InsertAssessment(Assessment assessment);
        Assessment UpdateAssessment(Assessment assessment);
        void DeleteAssessment(Assessment assessment);
        List<Assessment> GetAssessments(Discipline discipline);
        List<Assessment> GetAssessments(Discipline discipline, BasisForAssessing basisForAssessing);
        List<Assessment> GetAssessments(Enrollee enrollee);
        List<Assessment> GetAssessments(Enrollee enrollee, BasisForAssessing basisForAssessing);
        List<Assessment> GetAssessments(BasisForAssessing basisForAssessing);
        Assessment GetAssessment(int id);
        Assessment GetAssessment(string sertcode);
        Assessment GetAssessment(Discipline discipline, Enrollee enrollee);
        Assessment GetAssessment(Discipline discipline, Enrollee enrollee, BasisForAssessing basisForAssessing);
    }
}
