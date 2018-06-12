using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class SpecialityService : ISpecialityService
    {
        private EnrolleeContext context;
        public SpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteSpeciality(Speciality speciality)
        {
            Speciality specialityToDelete = context.Speciality.AsNoTracking().FirstOrDefault(s => s.SpecialityId == speciality.SpecialityId);
            context.Speciality.Remove(specialityToDelete);
            context.SaveChanges();
        }

        public List<Speciality> GetSpecialities()
        {
            List<Speciality> specialities = context.Speciality.AsNoTracking().ToList();
            return specialities;
        }

        public List<Speciality> GetSpecialities(Faculty faculty)
        {
            List<Speciality> specialities = context.Speciality.AsNoTracking().Where(s => s.FacultyId == faculty.FacultyId).ToList();
            return specialities;
        }

        public List<Speciality> GetSpecialities(FormOfStudy formOfStudy)
        {
            List<Speciality> specialities = context.Speciality.AsNoTracking().Where(s => s.FormOfStudyId == formOfStudy.FormOfStudyId).ToList();
            return specialities;
        }

        public List<Speciality> GetSpecialities(Faculty faculty, FormOfStudy formOfStudy)
        {
            List<Speciality> specialities = context.Speciality.AsNoTracking().Where(s => s.FormOfStudyId == formOfStudy.FormOfStudyId && s.FacultyId == faculty.FacultyId).ToList();
            return specialities;
        }

        public List<Speciality> GetSpecialities(Speciality groupSpeciality)
        {
            List<Speciality> specialities = context.Speciality.AsNoTracking().Where(s => s.SpecialityGroupId == groupSpeciality.SpecialityId).ToList();
            return specialities;
        }

        public Speciality GetSpeciality(int id)
        {
            Speciality speciality = context.Speciality.AsNoTracking().FirstOrDefault(s => s.SpecialityId == id);
            return speciality;
        }

        public Speciality GetSpeciality(string cipher)
        {
            Speciality speciality = context.Speciality.AsNoTracking().FirstOrDefault(s => s.Cipher == cipher);
            return speciality;
        }

        public Speciality InsertSpeciality(Speciality speciality)
        {
            context.Speciality.Add(speciality);
            context.SaveChanges();
            return speciality;
        }

        public Speciality UpdateSpeciality(Speciality speciality)
        {
            Speciality specialityToUpdate = context.Speciality.FirstOrDefault(s => s.SpecialityId == speciality.SpecialityId);
            specialityToUpdate.FacultyId = speciality.FacultyId;
            specialityToUpdate.FormOfStudyId = speciality.FormOfStudyId;
            specialityToUpdate.Fullname = speciality.Fullname;
            specialityToUpdate.Specialization = speciality.Specialization;
            specialityToUpdate.Cipher = speciality.Cipher;
            specialityToUpdate.BudgetCountPlace = speciality.BudgetCountPlace;
            specialityToUpdate.FeeCountPlace = speciality.FeeCountPlace;
            specialityToUpdate.TargetCountPlace = speciality.TargetCountPlace;
            specialityToUpdate.IsGroup = speciality.IsGroup;
            specialityToUpdate.IsAlternative = speciality.IsAlternative;
            specialityToUpdate.SpecialityGroupId = speciality.SpecialityGroupId;
            context.SaveChanges();
            return specialityToUpdate;
        }
    }
}
