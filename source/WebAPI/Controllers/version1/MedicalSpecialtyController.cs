using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;
using DomainModel.Contracts;
using DomainModel.Models.MedicalSpecialteis;
using DomainModel.Helpers;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order = 08)]
[ApiController]
[ApiVersion("1.0")]
public class MedicalSpecialtyController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public MedicalSpecialtyController(IUnitOfWork data)
    {
        Data = data;
    }



    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] MedicalSpecialtyDto model)
    {

        ResponseId response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Data.MedicalSpecialteis.CreateWithImage(model);
        }
        else
            response = await Data.MedicalSpecialteis.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }


    // ============================= get ============================= 


    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.MedicalSpecialteis.ReadById(id, lang);
        return Ok(result);
    }



    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? doctorId, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        Expression<Func<MedicalSpecialtyTranslation, bool>> filterExpression;
        if (doctorId.HasValue)
        {
            filterExpression = f =>
            f.LangCode == lang && f.MedicalSpecialty != null &&
            f.MedicalSpecialty.SpecialtiesDoctors.Any(doc => doc.DoctorId == doctorId);
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.MedicalSpecialteis.GenericReadAll(filterExpression, null!, page, pageSize);
        return Ok(result);
    }



    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery(Name = "hosid")] int? parentId, [FromQuery] bool? appearance, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        var resutl = await Data.MedicalSpecialteis.ReadAll(parentId, appearance, status, lang, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }



    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery(Name = "hosId")] int? parentId, [FromQuery] string? searchTerm, [FromQuery] string? name, bool? active, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.MedicalSpecialteis.GenericSearchByText<MedicalSpecialtyTranslation>(parentId,
                t => t.Name.Contains(name),
                ho => ho.MedicalSpecialty != null && ho.MedicalSpecialty.Hospitals.Any(z=>z.Id == parentId),
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.MedicalSpecialteis.SearchByNameOrCode(active,searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] MedicalSpecialtyDto model, int id)
    {
        Response<MedicalSpecialtyDto?> response;

        if (file == null)
        {
            response = await Data.MedicalSpecialteis.Update(model, id, null);
        }
        else
            response = await Data.MedicalSpecialteis.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }



    [HttpPut("edit-translations/{buildId?}", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<MedicalSpecialtyTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.MedicalSpecialteis.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.MedicalSpecialteis.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }



    [HttpPut("deactivate", Order = 0825)]
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
        MedicalSpecialty entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.MedicalSpecialteis.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }


    // ============================= delete ============================= 


    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        var res = new Response();
        res = await Data.MedicalSpecialteis.GenericDelete<MedicalSpecialtyTranslation>(t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class