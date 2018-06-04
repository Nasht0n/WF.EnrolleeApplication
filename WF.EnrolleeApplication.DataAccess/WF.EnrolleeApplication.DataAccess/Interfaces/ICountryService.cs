using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ICountryService
    {
        Country InsertCountry(Country country);
        Country UpdateCountry(Country country);
        void DeleteCountry(Country country);
        List<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountry(string name);
    }
}
