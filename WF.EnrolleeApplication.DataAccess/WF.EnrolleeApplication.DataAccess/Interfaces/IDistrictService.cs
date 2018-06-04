using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IDistrictService
    {
        District InsertDistrict(District district);
        District UpdateDistrict(District district);
        void DeleteDistrict(District district);
        List<District> GetDistricts();
        District GetDistrict(int id);
        District GetDistrict(string name);
    }
}
