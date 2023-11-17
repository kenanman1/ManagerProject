using Entities;
using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class PersonResponce
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }
    public double? Age { get; set; }
    public Guid CountryId { get; set; }
    public string? Country { get; set; }

    public static PersonResponce ToPersonResponce(Person person)
    {
        return new PersonResponce() { ID = person.Id, Name = person.Name, Email = person.Email, Surname = person.Surname, BirthDate = person.BirthDate, Address = person.Address, Age = (person.BirthDate != null) ? Math.Round((DateTime.Now - person.BirthDate.Value).TotalDays / 365) : null, CountryId = person.CountryId, Country = person.Country?.Name };
    }
}
