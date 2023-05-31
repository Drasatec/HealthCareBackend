using DomainModel.Contracts;
using DomainModel.Models;
using DomainModel.Models.Hospitals;

namespace WebAPI.Controllers.version1;
[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class HospitalController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public HospitalController(IUnitOfWork data)
    {
        this.Data = data;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] HospitalDto model)
    {
        HospitalDto? response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Data.Hospitals.CreateWithImage(model);
        }
        else
            response = await Data.Hospitals.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    [HttpPut("add-translations/{id}")]
    public async Task<IActionResult> AddTranslations([FromBody] List<HospitalTranslation> model, int id)
    {
        var response = await Data.Hospitals.AddTranslations(model, id);

        return Created("fawzy", response);
    }

    [HttpPut("add-phons/{id}")]
    public async Task<IActionResult> AddPhons([FromBody] List<HospitalPhoneNumber> model, int id)
    {
        var response = await Data.Hospitals.AddPhoneNumbers(model, id);

        return Created("https", response);
    }

    [HttpGet()]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Hospitals.ReadHospitalById(id, lang);
        return Ok(result);
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] HospitalDto model, int id)
    {
        Response<HospitalDto?> response;

        if (file == null)
        {
            response = await Data.Hospitals.UpdateHospital(model, id, null);
        }
        else
            response = await Data.Hospitals.UpdateHospital(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpPut("edit-body/{id}")]
    public async Task<IActionResult> UpdateSingle([FromBody] HospitalDto model, int id)
    {
        Response<HospitalDto?> response;
        response = await Data.Hospitals.UpdateHospital(model, id, null);
        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] string status = "active", [FromQuery] string? lang = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var hospitals = await Data.Hospitals.ReadAllHospitals(status, lang, page, pageSize);
        return Ok(hospitals);
    }


    [HttpPut("delete")]
    public async Task<IActionResult> DeleteSingle([FromQuery] int id, [FromQuery] string status)
    {
        bool isDeleted;
        if (status == "active")
            isDeleted = false;
        else if (status == "inactive")
            isDeleted = true;
        else
        {
            return BadRequest(new Response(false, "The status field in this context allows the values 'active' or 'inactive' "));
        }

        return Ok(await Data.Hospitals.DeleteHospitalById(id, isDeleted));
    }

    [HttpPut("edit-image/{hosId}")]
    public async Task<IActionResult> UpdateTrans([FromForm] IFormFile file, int hosId)
    {

        var response = await Data.Hospitals.UpdateAnImage(file.OpenReadStream(), hosId);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? codeNumber, [FromQuery] string? lang)
    {
        //Response<List<HospitalTranslation>> response;

        if (!string.IsNullOrEmpty(name))
            return Ok(await Data.Hospitals.SearchByName(name));

        if (!string.IsNullOrEmpty(codeNumber) && !string.IsNullOrEmpty(lang))
            return Ok(await Data.Hospitals.SearchByCodeNumber(codeNumber, lang));

        return BadRequest(new Error("400", "name or code number and lang is required"));
        //return Ok(response);
    }



    // delete ====================
    [HttpDelete("delete-translat")]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        var res = await Data.Hospitals.DeleteTranslat(translteId);
            if(res.Success)
        return Ok(res);
            return BadRequest(res);
    }

    [HttpDelete("delete-phone")]
    public async Task<IActionResult> DeletePhones([FromQuery] params int[] phoneId)
    {
        var res = await Data.Hospitals.DeletePhons(phoneId);
            if(res.Success)
        return Ok(res);
            return BadRequest(res);
    }
}
