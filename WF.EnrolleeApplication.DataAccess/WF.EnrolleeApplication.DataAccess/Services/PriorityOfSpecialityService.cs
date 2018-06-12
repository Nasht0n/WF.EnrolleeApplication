using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class PriorityOfSpecialityService : IPriorityOfSpecialityService
    {
        private EnrolleeContext context;
        public PriorityOfSpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeletePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            PriorityOfSpeciality priorityOfSpecialityToDelete = context.PriorityOfSpeciality.AsNoTracking().FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
            context.PriorityOfSpeciality.Remove(priorityOfSpecialityToDelete);
            context.SaveChanges();
        }

        public List<PriorityOfSpeciality> GetPriorityOfSpecialities()
        {
            List<PriorityOfSpeciality> priorityOfSpecialities = context.PriorityOfSpeciality.AsNoTracking().ToList();
            return priorityOfSpecialities;
        }

        public List<PriorityOfSpeciality> GetPriorityOfSpecialities(Enrollee enrollee)
        {
            List<PriorityOfSpeciality> priorityOfSpecialities = context.PriorityOfSpeciality.AsNoTracking().Where(ps => ps.EnrolleeId == enrollee.EnrolleeId).ToList();
            return priorityOfSpecialities;
        }

        public PriorityOfSpeciality GetPriorityOfSpeciality(int id)
        {
            PriorityOfSpeciality priorityOfSpeciality = context.PriorityOfSpeciality.AsNoTracking().FirstOrDefault(ps => ps.EnrolleeId == id);
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality GetPriorityOfSpeciality(Enrollee enrollee, Speciality speciality)
        {
            PriorityOfSpeciality priorityOfSpeciality = context.PriorityOfSpeciality.AsNoTracking().FirstOrDefault(ps => ps.EnrolleeId == enrollee.EnrolleeId && ps.SpecialityId == speciality.SpecialityId);
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality InsertPriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            context.PriorityOfSpeciality.Add(priorityOfSpeciality);
            context.SaveChanges();
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality UpdatePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            PriorityOfSpeciality priorityOfSpecialityToUpdate = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
            priorityOfSpecialityToUpdate.EnrolleeId = priorityOfSpeciality.EnrolleeId;
            priorityOfSpecialityToUpdate.SpecialityId = priorityOfSpeciality.SpecialityId;
            priorityOfSpecialityToUpdate.PriorityLevel = priorityOfSpeciality.PriorityLevel;
            context.SaveChanges();
            return priorityOfSpeciality;
        }
    }
}
