using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PersonRepository : IPersonRepository
{
    private PersonDbContext _dbContext;
    public PersonRepository(PersonDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Person> AddPerson(Person person)
    {
        await _dbContext.AddAsync(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task DeletePerson(Person person)
    {
        _dbContext.People.Remove(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> EmailExist(string email)
    {
        return await _dbContext.People.AsNoTracking().AnyAsync(x => x.Email == email);
    }

    public async Task<List<Person>> GetAllPersons()
    {
        return await _dbContext.People.Include(p => p.Country).AsNoTracking().ToListAsync();
    }

    public async Task<Person> GetByID(Guid guid)
    {
        return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(x => x.Id == guid);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        _dbContext.People.Update(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }
}
