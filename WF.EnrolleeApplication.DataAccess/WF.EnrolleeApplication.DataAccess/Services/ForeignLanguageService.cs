using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ForeignLanguageService : IForeignLanguageService
    {
        private EnrolleeContext context;
        public ForeignLanguageService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteForeignLanguage(ForeignLanguage foreignLanguage)
        {
            ForeignLanguage foreignLanguageToDelete = context.ForeignLanguage.AsNoTracking().FirstOrDefault(fl => fl.LanguageId == foreignLanguage.LanguageId);
            context.ForeignLanguage.Remove(foreignLanguageToDelete);
            context.SaveChanges();
        }

        public ForeignLanguage GetForeignLanguage(int id)
        {
            ForeignLanguage foreignLanguage = context.ForeignLanguage.AsNoTracking().FirstOrDefault(fl => fl.LanguageId == id);
            return foreignLanguage;
        }

        public ForeignLanguage GetForeignLanguage(string name)
        {
            ForeignLanguage foreignLanguage = context.ForeignLanguage.AsNoTracking().FirstOrDefault(fl => fl.Name == name);
            return foreignLanguage;
        }

        public List<ForeignLanguage> GetForeignLanguages()
        {
            List<ForeignLanguage> foreignLanguages = context.ForeignLanguage.AsNoTracking().ToList();
            return foreignLanguages;
        }

        public ForeignLanguage InsertForeignLanguage(ForeignLanguage foreignLanguage)
        {
            context.ForeignLanguage.Add(foreignLanguage);
            context.SaveChanges();
            return foreignLanguage;
        }

        public ForeignLanguage UpdateForeignLanguage(ForeignLanguage foreignLanguage)
        {
            ForeignLanguage foreignLanguageToUpdate = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == foreignLanguage.LanguageId);
            foreignLanguageToUpdate.Name = foreignLanguage.Name;
            context.SaveChanges();
            return foreignLanguageToUpdate;
        }
    }
}
