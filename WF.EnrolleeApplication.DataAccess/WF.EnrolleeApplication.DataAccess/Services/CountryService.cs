using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class CountryService : ICountryService
    {
        private EnrolleeContext context;
        public CountryService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteCountry(Country country)
        {
            Country countryToDelete = context.Country.FirstOrDefault(c => c.CountryId == country.CountryId);
            context.Country.Remove(countryToDelete);
            context.SaveChanges();
        }

        public List<Country> GetCountries()
        {
            List<Country> countries = context.Country.ToList();
            return countries;
        }

        public Country GetCountry(int id)
        {
            Country countryById = context.Country.FirstOrDefault(c => c.CountryId == id);
            return countryById;
        }

        public Country GetCountry(string name)
        {
            Country countryByName = context.Country.FirstOrDefault(c => c.Name == name);
            return countryByName;
        }

        public Country InsertCountry(Country country)
        {
            context.Country.Add(country);
            context.SaveChanges();
            return country;
        }

        public Country UpdateCountry(Country country)
        {
            Country countryToUpdate = context.Country.FirstOrDefault(c => c.CountryId == country.CountryId);
            countryToUpdate.Name = country.Name;
            context.SaveChanges();
            return countryToUpdate;
        }
    }
}
