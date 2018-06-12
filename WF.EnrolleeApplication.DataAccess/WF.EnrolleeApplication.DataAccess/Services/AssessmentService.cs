using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class AssessmentService : IAssessmentService
    {
        private EnrolleeContext context;
        public AssessmentService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteAssessment(Assessment assessment)
        {
            Assessment assessmentToDelete = context.Assessment.AsNoTracking().FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
            context.Assessment.Remove(assessmentToDelete);
            context.SaveChanges();
        }

        public Assessment GetAssessment(int id)
        {
            Assessment assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.AssessmentId == id);
            return assessment;
        }

        public Assessment GetAssessment(string sertcode)
        {
            Assessment assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.SertCode == sertcode);
            return assessment;
        }

        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee)
        {
            Assessment assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId);
            return assessment;
        }

        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            Assessment assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
            return assessment;
        }

        public List<Assessment> GetAssessments(Discipline discipline)
        {
            List<Assessment> assessments = context.Assessment.AsNoTracking().Where(a => a.DisciplineId == discipline.DisciplineId).ToList();          
            return assessments;
        }

        public List<Assessment> GetAssessments(Discipline discipline, BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = context.Assessment.AsNoTracking().Where(a => a.DisciplineId == discipline.DisciplineId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            return assessments;
        }

        public List<Assessment> GetAssessments(Enrollee enrollee)
        {
            List<Assessment> assessments = context.Assessment.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
            return assessments;
        }

        public List<Assessment> GetAssessments(Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = context.Assessment.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            return assessments;
        }

        public List<Assessment> GetAssessments(BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = context.Assessment.AsNoTracking().Where(a => a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            return assessments;
        }

        public Assessment InsertAssessment(Assessment assessment)
        {
            context.Assessment.Add(assessment);
            context.SaveChanges();
            return assessment;
        }

        public Assessment UpdateAssessment(Assessment assessment)
        {
            Assessment assessmentToUpdate = context.Assessment.FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
            assessmentToUpdate.ChangeDiscipline = assessment.ChangeDiscipline;
            assessmentToUpdate.DisciplineId = assessment.DisciplineId;
            assessmentToUpdate.EnrolleeId = assessment.EnrolleeId;
            assessmentToUpdate.Estimation = assessment.Estimation;
            assessmentToUpdate.SertCode = assessment.SertCode;
            assessmentToUpdate.SertDate = assessment.SertDate;
            context.SaveChanges();
            return assessmentToUpdate;
        }
    }
}
