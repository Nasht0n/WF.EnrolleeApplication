using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class FormOfStudyService : IFormOfStudyService
    {
        private EnrolleeContext context;
        public FormOfStudyService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteFormOfStudy(FormOfStudy formOfStudy)
        {
            FormOfStudy formOfStudyToDelete = context.FormOfStudy.AsNoTracking().FirstOrDefault(f => f.FormOfStudyId == formOfStudy.FormOfStudyId);
            context.FormOfStudy.Remove(formOfStudyToDelete);
            context.SaveChanges();
        }

        public List<FormOfStudy> GetFormOfStudies()
        {
            List<FormOfStudy> formOfStudies = context.FormOfStudy.AsNoTracking().ToList();
            return formOfStudies;
        }

        public FormOfStudy GetFormOfStudy(int id)
        {
            FormOfStudy formOfStudy = context.FormOfStudy.AsNoTracking().FirstOrDefault(f => f.FormOfStudyId == id);
            return formOfStudy;
        }

        public FormOfStudy GetFormOfStudy(string fullname)
        {
            FormOfStudy formOfStudy = context.FormOfStudy.AsNoTracking().FirstOrDefault(f => f.Fullname == fullname);
            return formOfStudy;
        }

        public FormOfStudy InsertFormOfStudy(FormOfStudy formOfStudy)
        {
            context.FormOfStudy.Add(formOfStudy);
            context.SaveChanges();
            return formOfStudy;
        }

        public FormOfStudy UpdateFormOfStudy(FormOfStudy formOfStudy)
        {
            FormOfStudy formOfStudyToUpdate = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == formOfStudy.FormOfStudyId);
            formOfStudyToUpdate.Fullname = formOfStudy.Fullname;
            formOfStudyToUpdate.Shortname = formOfStudy.Shortname;
            context.SaveChanges();
            return formOfStudyToUpdate;
        }
    }
}
