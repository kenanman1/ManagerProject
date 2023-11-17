using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CountryRepository : ICountryRepository
{
    private PersonDbContext _dbContext;
    public CountryRepository(PersonDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Country> AddCountry(Country country)
    {
        await _dbContext.AddAsync(country);
        await _dbContext.SaveChangesAsync();
        return country;
    }

    public async Task<bool> CountryExist(string name)
    {
        return await _dbContext.Countries.AsNoTracking().AnyAsync(x => x.Name == name);
    }

    public async Task<List<Country>> GetAllCountries()
    {
        return await _dbContext.Countries.AsNoTracking().ToListAsync();
    }

    public async Task<Country> GetByID(Guid guid)
    {
        return await _dbContext.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == guid);
    }
}
