using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Models;
using System.Net;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly DataContext _context; // Inject DataContext

        public HotelController(ILogger<HotelController> logger, DataContext context)
        {
            _logger = logger;
            _context = context; // Initialize DataContext
        }

        [HttpGet]
        public async Task<ActionResult<List<HotelDTO>>> ListAllHotels()
        {
            var resultDto = await _context.Hotel
                .Select(h => new HotelDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address
                })
                .ToListAsync();

            return resultDto;
        }
    }
}