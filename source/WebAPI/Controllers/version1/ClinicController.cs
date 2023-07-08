using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;
using DomainModel.Models.Dtos;
using DomainModel.Contracts;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order = 0901)]
[ApiController]
[ApiVersion("1.0")]
public class ClinicController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public ClinicController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0901)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] ClinicDto model)
    {

        ResponseId response;

        if (model == null)
        {
            return BadRequest(new Error("400", "clinic is requerd"));
        }
        if (file == null)
        {
            response = await Data.Clinics.CreateWithImage(model);
        }
        else
            response = await Data.Clinics.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }


    // ============================= get ============================= 


    [HttpGet(Order = 0901)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Clinics.ReadById(id, lang);
        return Ok(result);
    }



    [HttpGet("names", Order = 0911)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang,int? hosId,  int? specialtyId, int? page, int? pageSize)
    {
        Expression<Func<ClinicTranslation, bool>> filterExpression;
        if (specialtyId.HasValue && hosId.HasValue)
        {
            filterExpression = f =>
            f.LangCode == lang && f.Clinic != null &&
            f.Clinic.SpecialtyId == specialtyId &&
            f.Clinic.HospitalId == hosId;
        }

        else if (specialtyId.HasValue)
        {
            filterExpression = f =>
            f.LangCode == lang && f.Clinic != null &&
            f.Clinic.SpecialtyId == specialtyId;
        }

        else if (hosId.HasValue)
        {
            filterExpression = f =>
            f.LangCode == lang && f.Clinic != null &&
            f.Clinic.HospitalId == hosId;
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Clinics.GenericReadAll(filterExpression, null!, page, pageSize);
        return Ok(result);
    }



    [HttpGet("all", Order = 0912)]
    public async Task<IActionResult> GetAll([FromQuery(Name = "specialtyId")] int? parentId, [FromQuery] bool? appearance, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        var resutl = await Data.Clinics.ReadAll(parentId, appearance, status, lang, pageSize, page);
        if (resutl is null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }



    [HttpGet("search", Order = 0914)]
    public async Task<IActionResult> Search([FromQuery(Name = "specialtyId")] int? parentId, [FromQuery] string? searchTerm, [FromQuery] string? name, bool? active, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.Clinics.GenericSearchByText<ClinicTranslation>(parentId,
                t => t.Name.Contains(name),
                ho => ho.Clinic != null && ho.Clinic.SpecialtyId.Equals(parentId),
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Clinics.SearchByNameOrCode(active, searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0920)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] ClinicDto model, int id)
    {
        Response<ClinicDto?> response;

        if (file == null)
        {
            response = await Data.Clinics.Update(model, id, null);
        }
        else
            response = await Data.Clinics.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }



    [HttpPut("edit-translations/{buildId?}", Order = 0922)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<ClinicTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Clinics.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Clinics.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }



    [HttpPut("deactivate", Order = 0925)]
    public async Task<IActionResult> EditSingleProp([FromQuery] int? id, [FromQuery] string status)
    {
        if (!id.HasValue)
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }
        bool isDeleted;
        if (status == "active")
            isDeleted = false;
        else if (status == "inactive")
            isDeleted = true;
        else
        {
            return BadRequest(new Response(false, "The status field in this context allows the values 'active' or 'inactive' "));
        }
        Clinic entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Clinics.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }


    // ============================= delete ============================= 


    [HttpDelete("delete-translat", Order = 0930)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        var res = new Response();
        res = await Data.Clinics.GenericDelete<ClinicTranslation>(t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class