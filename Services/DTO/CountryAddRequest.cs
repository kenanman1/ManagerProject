using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class CountryAddRequest
{
    [Required]
    public string Name { get; set; }
}
