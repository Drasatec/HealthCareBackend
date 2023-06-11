using DomainModel.Contracts;
using DomainModel.Models;
using DomainModel.Models.Floors;

namespace WebAPI.Controllers.version1
{
    [Route("api/[controller]", Order = 04)]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork Data;

        public RoomController(IUnitOfWork data)
        {
            Data = data;
        }


        // ============================= post ============================= 1

        [HttpPost("add", Order = 0401)]
        public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] FloorDto model)
        {

            Response response;
            var entity = (HosFloor)model;

            if (model == null)
            {
                return BadRequest(new Error("400", "Hospital is requerd"));
            }
            if (file == null)
            {
                response = await Data.Floors.GenericCreateWithImage(entity);
            }
            else
                response = await Data.Buildings.GenericCreateWithImage(entity, file.OpenReadStream());

            return Created("fawzy", response);
        }

    }
}
