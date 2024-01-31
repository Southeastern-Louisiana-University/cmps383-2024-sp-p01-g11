using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Features.Hotel;
using System.Linq.Expressions;
using System.Net;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly DataContext dataContext;

        public HotelController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        private static Expression<Func<Hotel, HotelDto>> MapDto()
        {
            return x => new HotelDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            };
        }
        private IQueryable<HotelDto> GetDtos() 
        {
            return dataContext.Set<Hotel>().Select(MapDto());
        }

        [HttpGet]
        [Route("ListAllHotels")]
        public ActionResult Get()
        {
            var hotelDtos = GetDtos().ToList();

            if (hotelDtos.Any())
            {
                return Ok(new { value = 200, data = hotelDtos });
            }
            else
            {
                return NoContent();
            }
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<HotelDto> GetById(int id)
        {
            var result = dataContext
                .Set<Hotel>()
                .Select(MapDto())
                .FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public ActionResult<HotelDto> CreateHotel(CreateHotelDto createRequest)
        {
            if (string.IsNullOrEmpty(createRequest.Name) || createRequest.Name.Length > 120)
            {
                return BadRequest("Name must be provided and cannot be longer than 120 characters.");
            }

            if (string.IsNullOrEmpty(createRequest.Address))
            {
                return BadRequest("Must have an address.");
            }

            var newHotel = new Hotel
            {
                Name = createRequest.Name,
                Address = createRequest.Address
            };

            dataContext.Hotel.Add(newHotel);
            dataContext.SaveChanges();

            var createdDto = new HotelDto
            {
                Id = newHotel.Id,
                Name = newHotel.Name,
                Address = newHotel.Address
            };


            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteHotel(int id)
        {
            var hotelToDelete = dataContext.Hotel.Find(id);
            if (hotelToDelete == null)
            {
                return NotFound();
            }

            dataContext.Hotel.Remove(hotelToDelete);
            dataContext.SaveChanges();

            return Ok(new HotelDto
            {
                Id = hotelToDelete.Id,
                Name = hotelToDelete.Name,
                Address = hotelToDelete.Address
            });
        }

        [HttpPut("{id}")]
        public ActionResult<HotelDto> Update(int id, UpdateHotelDto updateRequest)
        {
            var hotelToUpdate = dataContext.Hotel.Find(id);
            if (hotelToUpdate == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(updateRequest.Name))
            {
                if (updateRequest.Name.Length > 120)
                {
                    return BadRequest("Name cannot be longer than 120 characters.");
                }
                hotelToUpdate.Name = updateRequest.Name;
            }

            if (!string.IsNullOrEmpty(updateRequest.Address))
            {
                hotelToUpdate.Address = updateRequest.Address;
            }

            dataContext.SaveChanges();

            var updatedDto = new HotelDto
            {
                Id = hotelToUpdate.Id,
                Name = hotelToUpdate.Name,
                Address = hotelToUpdate.Address
            };

            return updatedDto;
        }

    }
}
