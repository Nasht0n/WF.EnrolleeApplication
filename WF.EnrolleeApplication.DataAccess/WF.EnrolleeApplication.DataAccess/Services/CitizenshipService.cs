using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class CitizenshipService: ICitizenshipService
    {
        private EnrolleeContext context;

        public CitizenshipService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteCitizenship(Citizenship citizenship)
        {
            Citizenship citizenshipToDelete = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == citizenship.CitizenshipId);
            context.Citizenship.Remove(citizenship);
            context.SaveChanges();
        }

        public Citizenship GetCitizenship(int id)
        {
            Citizenship citizenshipById = context.Citizenship.AsNoTracking().FirstOrDefault(c => c.CitizenshipId == id);
            return citizenshipById;
        }

        public Citizenship GetCitizenship(string fullname)
        {
            Citizenship citizenshipByFullname = context.Citizenship.AsNoTracking().FirstOrDefault(c => c.Fullname == fullname);
            return citizenshipByFullname;
        }

        public List<Citizenship> GetCitizenships()
        {
            List<Citizenship> citizenships = context.Citizenship.AsNoTracking().ToList();
            return citizenships;
        }

        public Citizenship InsertCitizenship(Citizenship citizenship)
        {
            context.Citizenship.Add(citizenship);
            context.SaveChanges();
            return citizenship;
        }

        public Citizenship UpdateCitizenship(Citizenship citizenship)
        {
            Citizenship citizenshipToUpdate = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == citizenship.CitizenshipId);
            citizenshipToUpdate.Fullname = citizenship.Fullname;
            citizenshipToUpdate.Shortname = citizenship.Shortname;
            context.SaveChanges();
            return citizenshipToUpdate;
        }
    }
}
