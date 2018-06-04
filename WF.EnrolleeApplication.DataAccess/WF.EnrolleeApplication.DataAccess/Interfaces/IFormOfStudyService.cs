using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IFormOfStudyService
    {
        FormOfStudy InsertFormOfStudy(FormOfStudy formOfStudy);
        FormOfStudy UpdateFormOfStudy(FormOfStudy formOfStudy);
        void DeleteFormOfStudy(FormOfStudy formOfStudy);
        List<FormOfStudy> GetFormOfStudies();
        FormOfStudy GetFormOfStudy(int id);
        FormOfStudy GetFormOfStudy(string fullname);
    }
}
