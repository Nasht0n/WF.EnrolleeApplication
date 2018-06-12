using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class SecondarySpecialityService : ISecondarySpecialityService
    {
        private EnrolleeContext context;
        public SecondarySpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            SecondarySpeciality secondarySpecialityToDelete = context.SecondarySpeciality.AsNoTracking().FirstOrDefault(ss => ss.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
            context.SecondarySpeciality.Remove(secondarySpecialityToDelete);
            context.SaveChanges();
        }

        public List<SecondarySpeciality> GetSecondarySpecialities()
        {
            List<SecondarySpeciality> secondarySpecialities = context.SecondarySpeciality.AsNoTracking().ToList();
            return secondarySpecialities;
        }

        public SecondarySpeciality GetSecondarySpeciality(int id)
        {
            SecondarySpeciality secondarySpeciality = context.SecondarySpeciality.AsNoTracking().FirstOrDefault(ss => ss.SecondarySpecialityId == id);
            return secondarySpeciality;
        }

        public SecondarySpeciality GetSecondarySpeciality(string cipher)
        {
            SecondarySpeciality secondarySpeciality = context.SecondarySpeciality.AsNoTracking().FirstOrDefault(ss => ss.Cipher == cipher);
            return secondarySpeciality;
        }

        public SecondarySpeciality InsertSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            context.SecondarySpeciality.Add(secondarySpeciality);
            context.SaveChanges();
            return secondarySpeciality;
        }

        public SecondarySpeciality UpdateSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            SecondarySpeciality secondarySpecialityToUpdate = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
            secondarySpecialityToUpdate.Fullname = secondarySpeciality.Fullname;
            secondarySpecialityToUpdate.Cipher = secondarySpeciality.Cipher;
            context.SaveChanges();
            return secondarySpecialityToUpdate;
        }
    }
}
