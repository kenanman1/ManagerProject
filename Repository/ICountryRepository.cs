using Entities;

namespace Repository;

public interface ICountryRepository
{
    Task<Country> AddCountry(Country country);
    Task<List<Country>> GetAllCountries();
    Task<Country> GetByID(Guid guid);
    Task<bool> CountryExist(string name);
}
