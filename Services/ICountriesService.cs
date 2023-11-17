using Services.DTO;

namespace Services;

public interface ICountriesService
{
    Task<CountryResponce> AddCountry(CountryAddRequest request);
    Task<List<CountryResponce>> GetAllCountries();
    Task<CountryResponce> GetByID(Guid guid);
}
