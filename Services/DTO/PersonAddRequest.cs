using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class PersonAddRequest
{
    [Required]
    [StringLength(40, ErrorMessage = "Please enter a valid name")]
    public string Name { get; set; }

    [MinLength(6, ErrorMessage = "Please enter a valid email address.")]
    [StringLength(40, ErrorMessage = "Please enter a valid email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [StringLength(200, ErrorMessage = "Please enter a valid address")]
    public string? Address { get; set; }

    [StringLength(40, ErrorMessage = "Please enter a valid surname")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "The Country field is required")]
    public Guid CountryId { get; set; }
}
