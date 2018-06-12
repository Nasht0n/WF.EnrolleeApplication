using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ConversionSystemService : IConversionSystemService
    {
        private EnrolleeContext context;

        public ConversionSystemService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public double ConversionToFive(double ten)
        {
            ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Ten == ten);
            return conversionSystem.Five;
        }

        public double ConversionToTen(double five)
        {
            ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Five == five);
            return conversionSystem.Ten;
        }

        public void DeleteConversionSystem(ConversionSystem conversionSystem)
        {
            ConversionSystem conversionSystemToDelete = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Five == conversionSystem.Five && cs.Ten == conversionSystem.Ten);
            context.ConversionSystem.Remove(conversionSystemToDelete);
            context.SaveChanges();
        }

        public ConversionSystem GetConversion(double five, double ten)
        {
            ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.Five == five && cs.Ten == ten);
            return conversionSystem;
        }

        public ConversionSystem GetConversion(int id)
        {
            ConversionSystem conversionSystem = context.ConversionSystem.AsNoTracking().FirstOrDefault(cs => cs.ConversionSystemId == id);
            return conversionSystem;
        }

        public List<ConversionSystem> GetConversions()
        {
            List<ConversionSystem> conversions = context.ConversionSystem.AsNoTracking().ToList();
            return conversions;
        }

        public ConversionSystem InsertConversionSystem(ConversionSystem conversionSystem)
        {
            context.ConversionSystem.Add(conversionSystem);
            context.SaveChanges();
            return conversionSystem;
        }
    }
}
