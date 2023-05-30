using DomainModel.Contracts;
using DomainModel.Models.Hospitals;

namespace WebAPI.Controllers.version1;
[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class HospitalController : ControllerBase
{
    private readonly IUnitOfWork data;
    private readonly IBaseRepository<HospitalDto> Repo;

    public HospitalController(IUnitOfWork data, IBaseRepository<HospitalDto> repo)
    {
        this.data = data;
        Repo = repo;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] HospitalDto model)
    {
        HospitalDto? response = new();

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Repo.CreateWithImage(model);
        }
        else
            response = await Repo.CreateWithImage(model, file.OpenReadStream());

        return Created("https//........", response);
    }

    [HttpPut("add-translations/{id}")]
    public async Task<IActionResult> AddTranslations([FromBody] List<HospitalTranslation> model, int id)
    {
        var response = await Repo.AddTranslations(model, id);

        return Created("https//........", response);
    }

    [HttpGet("{id}/{lang?}")]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Repo.ReadSingleById(id, lang);
        return Ok(result);
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] HospitalDto model, int id)
    {
        HospitalDto? response = new();

        if (file == null)
        {
            response = await Repo.Update(model, id, null);
        }
        else
            response = await Repo.Update(model, id, file.OpenReadStream());

        if(response == null)
        {
            return BadRequest(new Error("400", "some data in invalid"));
        }
        return Created("fssssssssssssssssssssssssssssss", response);
    }

    [HttpPut("edit-body/{id}")]
    public async Task<IActionResult> UpdateSingle([FromBody] HospitalDto model, int id)
    {
        HospitalDto? response = new();

        response = await Repo.Update(model, id, null);


        if (response == null)
        {
            return BadRequest(new Error("204", "some data in invalid"));
        }
        return Created("fssssssssssssssssssssssssssssss", response);
    }

    [HttpGet("all/{lang?}/{page?}/{pageSize?}")]
    public async Task<IActionResult> GetAll([FromQuery] string? lang = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var hospitals = await Repo.ReadAll(true, lang, page, pageSize);
        return Ok(hospitals);
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteSingle(int id)
    {
        
        return Ok("");
    }

    [HttpPut("edit-image/{hosId}")]
    public async Task<IActionResult> UpdateTrans([FromForm] IFormFile file, int hosId)
    {
        var response = await Repo.UpdateAnImage(file.OpenReadStream(), hosId);

        return Created("fssssssssssssssssssssssssssssss", response);
    }

    [HttpPut("editTranc/{id}")]
    public async Task<IActionResult> UpdateSingleWithTranc([FromBody] HospitalDto? model, int id)
    {
        return Ok();
    }

}
