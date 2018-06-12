using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TypeOfStreetService : ITypeOfStreetService
    {
        private EnrolleeContext context;
        public TypeOfStreetService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            TypeOfStreet typeOfStreetToDelete = context.TypeOfStreet.AsNoTracking().FirstOrDefault(ts => ts.StreetTypeId == typeOfStreet.StreetTypeId);
            context.TypeOfStreet.Remove(typeOfStreet);
            context.SaveChanges();
        }

        public TypeOfStreet GetTypeOfStreet(int id)
        {
            TypeOfStreet typeOfStreet = context.TypeOfStreet.AsNoTracking().FirstOrDefault(ts => ts.StreetTypeId == id);
            return typeOfStreet;
        }

        public TypeOfStreet GetTypeOfStreet(string fullname)
        {
            TypeOfStreet typeOfStreet = context.TypeOfStreet.AsNoTracking().FirstOrDefault(ts => ts.Fullname == fullname);
            return typeOfStreet;
        }

        public List<TypeOfStreet> GetTypeOfStreets()
        {
            List<TypeOfStreet> typeOfStreets = context.TypeOfStreet.AsNoTracking().ToList();
            return typeOfStreets;
        }

        public TypeOfStreet InsertTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            context.TypeOfStreet.Add(typeOfStreet);
            context.SaveChanges();
            return typeOfStreet;
        }

        public TypeOfStreet UpdateTypeOfStreet(TypeOfStreet typeOfStreet)
        {
            TypeOfStreet typeOfStreetToUpdate = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == typeOfStreet.StreetTypeId);
            typeOfStreetToUpdate.Fullname = typeOfStreet.Fullname;
            typeOfStreetToUpdate.Shortname = typeOfStreet.Shortname;
            context.SaveChanges();
            return typeOfStreetToUpdate;
        }
    }
}
