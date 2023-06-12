using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Models;
using DomainModel.Models.Hospitals;
using System;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;
[Route("api/[controller]", Order = 01)]
[ApiController]
[ApiVersion("1.0")]
public class HospitalController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public HospitalController(IUnitOfWork data)
    {
        this.Data = data;
    }

    // ============================= post ============================= 1

    [HttpPost("add", Order = 0101)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] HospitalDto model)
    {
        ResponseId? response;

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

    // ============================= get ============================= 4

    [HttpGet(Order = 0101)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Hospitals.ReadHospitalById(id, lang);
        return Ok(result);
    }

    [HttpGet("names", Order = 0111)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] bool? active, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {

        Expression<Func<HospitalTranslation, bool>> filterExpression;
        if (active.HasValue)
        {
            if (active.Value)
                filterExpression = f => f.LangCode == lang && f.Hospital != null && !f.Hospital.IsDeleted;
            else
                filterExpression = f => f.LangCode == lang && f.Hospital != null && f.Hospital.IsDeleted;
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Hospitals.GenericReadAll(filterExpression, (hos) =>
        new HospitalTranslation
        {
            Id = hos.Id, LangCode = hos.LangCode,Name = hos.Name,
            HospitalId = hos.HospitalId,
        },page,pageSize);
        return Ok(result);
    }

    [HttpGet("all", Order = 0112)]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int? pageSize=null, [FromQuery] int page = 1, [FromQuery] string? lang = null)
    {
        var resutl = await Data.Hospitals.ReadAllHospitals(status, lang, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }

    [HttpGet("search", Order = 0114)]
    public async Task<IActionResult> Search([FromQuery] string? name,bool? active, [FromQuery] string? searchTerm, [FromQuery] string? lang, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Expression<Func<HospitalTranslation, bool>> filterExpression;
            if (active.HasValue)
            {
                if (active.Value)
                    filterExpression = f => f.Name.Contains(name) && f.Hospital != null && !f.Hospital.IsDeleted;
                else
                    filterExpression = f => f.Name.Contains(name) && f.Hospital != null && f.Hospital.IsDeleted;
            }
            else
                filterExpression = f => f.Name.Contains(name);

            var result = await Data.Hospitals.GenericSearchByText(filterExpression, (hos) =>
            new HospitalTranslation
            {
                Id = hos.Id,
                LangCode = hos.LangCode,
                Name = hos.Name,
                HospitalId = hos.HospitalId,
            },page,pageSize);


            return Ok(result);
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Hospitals.SearchByHospitalNameOrCode(searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }

    // ============================= put ============================= 4

    [HttpPut("edit/{id}", Order = 0120)]
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

    [HttpPut("edit-translations/{hosid?}", Order = 0122)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<HospitalTranslation> translations, int? hosid)
    {
        Response response;

        response = await Data.Hospitals.GenericUpdate(translations);
        if (response.Success)
            return Created("fawzy", response);
        return BadRequest(response);

    }

    [HttpPut("edit-phons/{hosid?}", Order = 0123)]
    public async Task<IActionResult> Add_EditPhons([FromBody] List<HospitalPhoneNumber> phons, int? hosid)
    {
        Response response;

        response = await Data.Hospitals.GenericUpdate(phons);
        if (response.Success)
            return Created("fawzy", response);
        return BadRequest(response);
    }

    [HttpPut("deactivate", Order = 0125)]
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
        Hospital hospital = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Hospitals.GenericUpdateSinglePropertyById(id.Value, hospital, p => p.IsDeleted));
    }

    // ============================= delete ============================= 2

    [HttpDelete("delete-translat", Order = 0130)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        HospitalTranslation entity = new();
        var res = new Response();
        res = await Data.Hospitals.GenericDelete(entity, t => translteId.Contains(t.Id), translteId);
        // res = await Data.Hospitals.DeleteTranslat(translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete-phone", Order = 0131)]
    public async Task<IActionResult> DeletePhones([FromQuery] params int[] phoneId)
    {
        HospitalPhoneNumber entity = new();
        var res = new Response();
        res = await Data.Hospitals.GenericDelete(entity, t => phoneId.Contains(t.Id), phoneId);
        //var res = await Data.Hospitals.DeletePhons(phoneId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }
}

/*
 ====================================================================================================
    [HttpPut("edit-image/{hosId}", Order = 0126)]
    public async Task<IActionResult> UpdateTrans([FromForm] IFormFile file, int hosId)
    {

        var response = await Data.Hospitals.UpdateAnImage(file.OpenReadStream(), hosId);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut("edit-body/{id}", Order = 0127)]
    public async Task<IActionResult> UpdateSingle([FromBody] HospitalDto model, int id)
    {
        Response<HospitalDto?> response;
        response = await Data.Hospitals.UpdateHospital(model, id, null);
        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

====================================================================================================

[HttpPut("add-translations/{id}")]
public async Task<IActionResult> AddTranslationsV1([FromBody] List<HospitalTranslation> model, int id)
{
    Response<HospitalDto?> response;

    foreach (var translation in model)
    {
        translation.HospitalId = id;
    }

    response = await Data.Hospitals.GAddTranslations(model);
    if (response.Success)
        response.Value = await Data.Hospitals.ReadHospitalById(id);

    return Created("fawzy", response);
}

====================================================================================================

[HttpPut("add-translations-v1/{id}")]
public async Task<IActionResult> AddTranslations([FromBody] List<HospitalTranslation> model, int id)
{
    var response = await Data.Hospitals.AddTranslations(model, id);

    return Created("fawzy", response);
}
*/
