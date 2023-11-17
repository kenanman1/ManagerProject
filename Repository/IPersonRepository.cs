using Entities;

namespace Repository;

public interface IPersonRepository
{
    Task<Person> AddPerson(Person person);
    Task<List<Person>> GetAllPersons();
    Task<Person> GetByID(Guid guid);
    Task<bool> EmailExist(string email);
    Task DeletePerson(Person person);
    Task<Person> UpdatePerson(Person person);
}
