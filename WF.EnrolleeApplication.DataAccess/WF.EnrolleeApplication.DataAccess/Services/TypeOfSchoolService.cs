using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TypeOfSchoolService : ITypeOfSchoolService
    {
        private EnrolleeContext context;
        public TypeOfSchoolService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            TypeOfSchool typeOfSchoolToDelete = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
            context.TypeOfSchool.Remove(typeOfSchoolToDelete);
            context.SaveChanges();
        }

        public TypeOfSchool GetTypeOfSchool(int id)
        {
            TypeOfSchool typeOfSchool = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.SchoolTypeId == id);
            return typeOfSchool;
        }

        public TypeOfSchool GetTypeOfSchool(string name)
        {
            TypeOfSchool typeOfSchool = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.Name == name);
            return typeOfSchool;
        }

        public List<TypeOfSchool> GetTypeOfSchools()
        {
            List<TypeOfSchool> typeOfSchools = context.TypeOfSchool.AsNoTracking().ToList();
            return typeOfSchools;
        }

        public TypeOfSchool InsertTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            context.TypeOfSchool.Add(typeOfSchool);
            context.SaveChanges();
            return typeOfSchool;
        }

        public TypeOfSchool UpdateTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            TypeOfSchool typeOfSchoolToUpdate = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
            typeOfSchoolToUpdate.Name = typeOfSchool.Name;
            context.SaveChanges();
            return typeOfSchoolToUpdate;
        }
    }
}
