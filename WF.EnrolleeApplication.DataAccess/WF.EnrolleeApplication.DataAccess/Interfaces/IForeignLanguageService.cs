using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IForeignLanguageService
    {
        ForeignLanguage InsertForeignLanguage(ForeignLanguage foreignLanguage);
        ForeignLanguage UpdateForeignLanguage(ForeignLanguage foreignLanguage);
        void DeleteForeignLanguage(ForeignLanguage foreignLanguage);
        List<ForeignLanguage> GetForeignLanguages();
        ForeignLanguage GetForeignLanguage(int id);
        ForeignLanguage GetForeignLanguage(string name);
    }
}
