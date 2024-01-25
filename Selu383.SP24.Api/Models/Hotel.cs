using System.ComponentModel.DataAnnotations;

namespace Selu383.SP24.Api.Models;

public class Hotel
{
    public int Id { get; set; }
    [Required]
    [MaxLength(120)]
    public string? Name { get; set; } = string.Empty;
    [Required]
    public string? Address { get; set; } = string.Empty;
}


public class HotelDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(120)]
    public string? Name { get; set; } = string.Empty;
    [Required]
    public string? Address { get; set; } = string.Empty;
}

public class CreateHotelDTO
{
    [Required]
    [MaxLength(120)]
    public string? Name { get; set; } = string.Empty;
    [Required]
    public string? Address { get; set; } = string.Empty;

}