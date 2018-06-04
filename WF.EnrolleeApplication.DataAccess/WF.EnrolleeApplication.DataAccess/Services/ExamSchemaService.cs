using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ExamSchemaService : IExamSchemaService
    {
        private EnrolleeContext context;
        public ExamSchemaService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteExamSchema(ExamSchema examSchema)
        {
            ExamSchema examSchemaToDelete = context.ExamSchema.FirstOrDefault(es => es.DisciplineId == examSchema.DisciplineId && es.SpecialityId == examSchema.SpecialityId);
            context.ExamSchema.Remove(examSchemaToDelete);
            context.SaveChanges();
        }

        public ExamSchema GetExamSchema(Speciality speciality, Discipline discipline)
        {
            ExamSchema examSchema = context.ExamSchema.FirstOrDefault(es => es.DisciplineId == discipline.DisciplineId && es.SpecialityId == speciality.SpecialityId);
            examSchema.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == examSchema.DisciplineId);
            examSchema.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa=>bfa.BasisForAssessingId == examSchema.Discipline.BasisForAssessingId);
            examSchema.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == examSchema.SpecialityId);
            examSchema.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == examSchema.Speciality.FacultyId);
            examSchema.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f=>f.FormOfStudyId == examSchema.Speciality.FormOfStudyId);
            return examSchema;
        }

        public List<ExamSchema> GetExamSchemas()
        {
            List<ExamSchema> examSchemas = context.ExamSchema.ToList();
            foreach (var examSchema in examSchemas)
            {
                examSchema.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == examSchema.DisciplineId);
                examSchema.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == examSchema.Discipline.BasisForAssessingId);
                examSchema.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == examSchema.SpecialityId);
                examSchema.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == examSchema.Speciality.FacultyId);
                examSchema.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == examSchema.Speciality.FormOfStudyId);
            }
            return examSchemas;
        }

        public List<ExamSchema> GetExamSchemas(Speciality speciality)
        {
            List<ExamSchema> examSchemas = context.ExamSchema.Where(es => es.SpecialityId == speciality.SpecialityId).ToList();
            foreach (var examSchema in examSchemas)
            {
                examSchema.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == examSchema.DisciplineId);
                examSchema.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == examSchema.Discipline.BasisForAssessingId);
                examSchema.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == examSchema.SpecialityId);
                examSchema.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == examSchema.Speciality.FacultyId);
                examSchema.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == examSchema.Speciality.FormOfStudyId);
            }
            return examSchemas;
        }

        public ExamSchema InsertExamSchema(ExamSchema examSchema)
        {
            context.ExamSchema.Add(examSchema);
            context.SaveChanges();
            return examSchema;
        }
    }
}
