using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public Country Country { get; set; }
    public Guid CountryId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }
}
