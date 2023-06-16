﻿using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;
using DomainModel.Contracts;
using DomainModel.Models.MedicalSpecialteis;
using DomainModel.Helpers;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order = 09)]
[ApiController]
[ApiVersion("1.0")]
public class MedicalSpecialtyController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public MedicalSpecialtyController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 1

    [HttpPost("add", Order = 0901)]
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

    // ============================= get ============================= 4

    [HttpGet(Order = 0901)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.MedicalSpecialteis.ReadById(id, lang);
        return Ok(result);
    }


    [HttpGet("names", Order = 0911)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? hosId, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        Expression<Func<MedicalSpecialtyTranslation, bool>> filterExpression;
        if (hosId.HasValue)
        {
            filterExpression = f =>
            f.LangCode == lang && f.MedicalSpecialty != null &&
            f.MedicalSpecialty.Hospitals.Where(hos => hos.Id == hosId).FirstOrDefault()!.Id == hosId;
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.MedicalSpecialteis.GenericReadAll(filterExpression, null!, page, pageSize);
        return Ok(result);
    }

    [HttpGet("all", Order = 0912)]
    public async Task<IActionResult> GetAll([FromQuery(Name = "hosid")] int? baseId, [FromQuery] bool? appearance, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang = null)
    {
        var resutl = await Data.MedicalSpecialteis.ReadAll(baseId, appearance, status, lang, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }

    [HttpGet("search", Order = 0914)]
    public async Task<IActionResult> Search(bool? active, [FromQuery(Name = "hosId")] int? baseId, [FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang = Constants.BaseLang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.MedicalSpecialteis.GenericSearchByText<MedicalSpecialtyTranslation>(baseId,
                t => t.Name.Contains(name),
                ho => ho.MedicalSpecialty != null && ho.MedicalSpecialty.Hospitals.Any(z=>z.Id == baseId),
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.MedicalSpecialteis.SearchByNameOrCode(active,searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }

    // ============================= put ============================= 4

    [HttpPut("edit/{id}", Order = 0920)]
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

    [HttpPut("edit-translations/{buildId?}", Order = 0922)]
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
        MedicalSpecialty hospital = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.MedicalSpecialteis.GenericUpdateSinglePropertyById(id.Value, hospital, p => p.IsDeleted));
    }

    // ============================= delete ============================= 2

    [HttpDelete("delete-translat", Order = 0930)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        MedicalSpecialtyTranslation entity = new();
        var res = new Response();
        res = await Data.MedicalSpecialteis.GenericDelete(entity, t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class