using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Features.Hotel;
using System.Linq.Expressions;

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
        [Route("all")]
        public ActionResult<IEnumerable<HotelDto>> Get()
        {
            return GetDtos().ToList();
        }


        [HttpGet(Name = "GetById")]
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
    }
}
