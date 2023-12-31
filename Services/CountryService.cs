﻿using Entities;
using Repository;
using Services.DTO;

namespace Services;

public class CountryService : ICountriesService
{
    private ICountryRepository _countryRepository;
    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<CountryResponce> AddCountry(CountryAddRequest request)
    {
        if (request != null)
        {
            if (await _countryRepository.CountryExist(request.Name))
                throw new ArgumentException(nameof(request.Name));

            Country country = new Country { Id = Guid.NewGuid(), Name = request.Name };
            await _countryRepository.AddCountry(country);
            return new CountryResponce { Id = country.Id, Name = country.Name };
        }
        else
            throw new ArgumentNullException(nameof(request));
    }

    public async Task<List<CountryResponce>> GetAllCountries()
    {
        List<Country> countries = await _countryRepository.GetAllCountries();
        return countries.Select(x => new CountryResponce { Id = x.Id, Name = x.Name }).ToList();
    }

    public async Task<CountryResponce> GetByID(Guid guid)
    {
        Country? country = await _countryRepository.GetByID(guid);
        if (country != null)
            return new CountryResponce { Id = country.Id, Name = country.Name };
        else
            throw new ArgumentException(nameof(guid));
    }
}
