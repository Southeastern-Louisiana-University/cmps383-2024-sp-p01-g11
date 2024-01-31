using System.ComponentModel.DataAnnotations;

namespace Selu383.SP24.Api.Features.Hotel
{
    public class CreateHotelDto
    {
        [Required]
        [MaxLength(120)]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public string? Address { get; set; } = string.Empty;

    }
}
