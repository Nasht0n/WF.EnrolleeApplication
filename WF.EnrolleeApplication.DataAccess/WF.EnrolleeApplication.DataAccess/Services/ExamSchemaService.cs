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
            ExamSchema examSchemaToDelete = context.ExamSchema.AsNoTracking().FirstOrDefault(es => es.DisciplineId == examSchema.DisciplineId && es.SpecialityId == examSchema.SpecialityId);
            context.ExamSchema.Remove(examSchemaToDelete);
            context.SaveChanges();
        }

        public ExamSchema GetExamSchema(Speciality speciality, Discipline discipline)
        {
            ExamSchema examSchema = context.ExamSchema.AsNoTracking().FirstOrDefault(es => es.DisciplineId == discipline.DisciplineId && es.SpecialityId == speciality.SpecialityId);
            return examSchema;
        }

        public List<ExamSchema> GetExamSchemas()
        {
            List<ExamSchema> examSchemas = context.ExamSchema.AsNoTracking().ToList();
            return examSchemas;
        }

        public List<ExamSchema> GetExamSchemas(Speciality speciality)
        {
            List<ExamSchema> examSchemas = context.ExamSchema.AsNoTracking().Where(es => es.SpecialityId == speciality.SpecialityId).ToList();
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
