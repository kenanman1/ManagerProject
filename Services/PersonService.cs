using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using Services.DTO;
using System.ComponentModel.DataAnnotations;

namespace Services;

public class PersonService : IPersonService
{
    private IPersonRepository _personRepository;
    private ILogger<PersonService> _logger;

    public PersonService(IPersonRepository dbContext, ILogger<PersonService> logger)
    {
        _logger = logger;
        _personRepository = dbContext;
    }

    public async Task<PersonResponce> AddPerson(PersonAddRequest request)
    {
        ValidationContext context = new ValidationContext(request);
        List<ValidationResult> results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, context, results, true))
        {
            foreach (var error in results)
            {
                throw new ValidationException(error.ErrorMessage);
            }
        }

        Person person = new Person() { Id = Guid.NewGuid(), Name = request.Name, Surname = request.Surname, Address = request.Address, BirthDate = request?.BirthDate, Email = request.Email, CountryId = request.CountryId };

        await _personRepository.AddPerson(person);

        return PersonResponce.ToPersonResponce(person);
    }

    public async Task<List<PersonResponce>> GetAllPersons()
    {
        _logger.LogInformation("GetAllPersons called");
        List<Person> people = await _personRepository.GetAllPersons();
        List<PersonResponce> responces = new();
        foreach (var item in people)
        {
            responces.Add(PersonResponce.ToPersonResponce(item));
        }
        return responces;
    }

    public async Task<List<PersonResponce>> GetFilteredByAny(string filter, string? obj)
    {
        List<PersonResponce> all = await GetAllPersons();
        if(obj == null)
            return all;

        switch (filter)
        {
            case nameof(PersonResponce.Name):
                    return all.Where(x => x.Name.Contains(obj, StringComparison.OrdinalIgnoreCase)).ToList();
            case nameof(PersonResponce.Surname):
                    return all.Where(x => x.Surname != null).Where(x => x.Surname.Contains(obj, StringComparison.OrdinalIgnoreCase)).ToList();
            case nameof(PersonResponce.Email):
                    return all.Where(x => x.Email != null).Where(x => x.Email.Contains(obj, StringComparison.OrdinalIgnoreCase)).ToList();
            case nameof(PersonResponce.Address):
                    return all.Where(x => x.Address != null).Where(x => x.Address.Contains(obj, StringComparison.OrdinalIgnoreCase)).ToList();
            default:
                return all;
        }
    }

    public async Task<PersonResponce?> GetPerson(Guid id)
    {
        Person? person = await _personRepository.GetByID(id);
        if (person != null)
            return PersonResponce.ToPersonResponce(person);
        else
            return null;
    }

    public List<PersonResponce> GetSorted(List<PersonResponce> people, string filter, SortOrder order = SortOrder.ASC)
    {
        switch (filter)
        {
            case nameof(PersonResponce.Name):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Name).ToList() : people.OrderBy(x => x.Name).ToList();
            case nameof(PersonResponce.Surname):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Surname).ToList() : people.OrderBy(x => x.Surname).ToList();
            case nameof(PersonResponce.Email):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Email).ToList() : people.OrderBy(x => x.Email).ToList();
            case nameof(PersonResponce.Address):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Address).ToList() : people.OrderBy(x => x.Address).ToList();
            case nameof(PersonResponce.Age):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Age).ToList() : people.OrderBy(x => x.Age).ToList();
            case nameof(PersonResponce.Country):
                return order == SortOrder.DESC ? people.OrderByDescending(x => x.Country).ToList() : people.OrderBy(x => x.Country).ToList();
            default:
                return people;
        }
    }

    public async Task<PersonResponce> UpdatePerson(PersonUpdateRequest request)
    {
        Person? person = await _personRepository.GetByID(request.Id);
        if (person != null)
        {
            person.Name = request.Name;
            person.Surname = request.Surname;
            person.Email = request.Email;
            person.BirthDate = request.BirthDate;
            person.Address = request.Address;
            person.CountryId = request.CountryId;
            await _personRepository.UpdatePerson(person);
            return PersonResponce.ToPersonResponce(person);
        }
        else
            return null;
    }

    public async Task<bool> DeletePerson(Guid id)
    {
        Person? person = await _personRepository.GetByID(id);
        if (person != null)
        {
            await _personRepository.DeletePerson(person);
            return true;
        }
        else
            return false;
    }

    public async Task<bool> CheckEmailExists(string email)
    {
        return await _personRepository.EmailExist(email);
    }
}
