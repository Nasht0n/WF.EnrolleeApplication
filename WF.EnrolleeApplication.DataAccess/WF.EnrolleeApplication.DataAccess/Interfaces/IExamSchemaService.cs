using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IExamSchemaService
    {
        ExamSchema InsertExamSchema(ExamSchema examSchema);
        void DeleteExamSchema(ExamSchema examSchema);
        List<ExamSchema> GetExamSchemas();
        List<ExamSchema> GetExamSchemas(Speciality speciality);
        ExamSchema GetExamSchema(Speciality speciality, Discipline discipline);
    }
}
