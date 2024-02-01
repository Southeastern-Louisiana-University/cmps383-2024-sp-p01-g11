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
        private readonly DataContext _context; 

        public HotelController(ILogger<HotelController> logger, DataContext context)
        {
            _logger = logger;
            _context = context; 
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


        [HttpGet("{id}")]
        public ActionResult<HotelDTO> GetHotelById(int id)
        {
            var hotelDto = _context.Hotel?.Where(h => h.Id == id).FirstOrDefault();
            if(hotelDto == null)
            {
                return NotFound();

            }
            return Ok(hotelDto);
        }


        [HttpPost]
        public ActionResult<HotelDTO> CreateHotel(CreateHotelDTO createRequest)
        {
            if (string.IsNullOrEmpty(createRequest.Name) || createRequest.Name.Length > 120)
            {
                return BadRequest("Name must be provided and exceed 120 characters.");
            }

            if (string.IsNullOrEmpty(createRequest.Address))
            {
                return BadRequest("Must provide an address.");
            }

            var newHotel = new Hotel
            {
                Name = createRequest.Name,
                Address = createRequest.Address
            };

            _context.Hotel.Add(newHotel);
            _context.SaveChanges();

            var createdDto = new HotelDTO
            {
                Id = newHotel.Id,
                Name = newHotel.Name,
                Address = newHotel.Address
            };


            return CreatedAtAction(nameof(GetHotelById), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotel([FromBody] HotelDTO updateDto, int id)
        {

            var hotelToUpdate = _context.Set<Hotel>()
                .FirstOrDefault(h => h.Id == id);

            if (hotelToUpdate == null)
            {
                return NotFound();
            }
            hotelToUpdate.Id = updateDto.Id;
            hotelToUpdate.Name = updateDto.Name;
            hotelToUpdate.Address = updateDto.Address;

            _context.SaveChanges();

            var hotelToReturn = new HotelDTO
            {
                Id = hotelToUpdate.Id,
                Name = hotelToUpdate.Name,
                Address = hotelToUpdate.Address,
            };

            return Ok(hotelToReturn);

        }

        [HttpDelete("{id}")]

        public ActionResult DeleteHotel(int id)
        {
            var hotelToDelete = _context.Hotel.Find(id);
            if (hotelToDelete == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotelToDelete);
            _context.SaveChanges();

            return Ok(new HotelDTO
            {
                Id = hotelToDelete.Id,
                Name = hotelToDelete.Name,
                Address = hotelToDelete.Address
            });
        }
    }

}