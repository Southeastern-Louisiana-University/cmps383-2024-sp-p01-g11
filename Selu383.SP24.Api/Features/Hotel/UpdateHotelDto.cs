using System.ComponentModel.DataAnnotations;

namespace Selu383.SP24.Api.Features.Hotel
{
    public class UpdateHotelDto
    {
        [MaxLength(120)]
        public string Name { get; set; }
        [MaxLength(120)]
        public string Address { get; set; }
    }
}
