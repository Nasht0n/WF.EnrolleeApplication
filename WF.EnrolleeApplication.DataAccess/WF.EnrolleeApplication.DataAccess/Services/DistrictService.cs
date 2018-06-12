using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class DistrictService : IDistrictService
    {
        private EnrolleeContext context;
        public DistrictService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteDistrict(District district)
        {
            District districtToDelete = context.District.AsNoTracking().FirstOrDefault(d => d.DistrictId == district.DistrictId);
            context.District.Remove(districtToDelete);
            context.SaveChanges();
        }

        public District GetDistrict(int id)
        {
            District districtById = context.District.AsNoTracking().FirstOrDefault(d => d.DistrictId == id);
            return districtById;
        }

        public District GetDistrict(string name)
        {
            District districtByName = context.District.AsNoTracking().FirstOrDefault(d => d.Name == name);
            return districtByName;
        }

        public List<District> GetDistricts()
        {
            List<District> districts = context.District.AsNoTracking().ToList();
            return districts;
        }

        public District InsertDistrict(District district)
        {
            context.District.Add(district);
            context.SaveChanges();
            return district;
        }

        public District UpdateDistrict(District district)
        {
            District districtToUpdate = context.District.FirstOrDefault(d => d.DistrictId == district.DistrictId);
            districtToUpdate.Name = district.Name;
            context.SaveChanges();
            return districtToUpdate;
        }
    }
}
