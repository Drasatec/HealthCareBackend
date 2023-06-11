﻿using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Models;
using DomainModel.Models.Floors;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order =03)]
[ApiController]
[ApiVersion("1.0")]
public class FloorController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public FloorController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 1

    [HttpPost("add", Order = 0301)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] FloorDto model)
    {

        Response response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Data.Floors.CreateWithImage(model);
        }
        else
            response = await Data.Floors.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    // ============================= get ============================= 4

    [HttpGet(Order = 0301)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Floors.ReadById(id, lang);
        return Ok(result);
    }

    [HttpGet("names", Order = 0311)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] bool? active, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {

        Expression<Func<FloorTranslation, bool>> filterExpression;
        if (active.HasValue)
        {
            if (active.Value)
                filterExpression = f => f.LangCode == lang && f.Floor != null && !f.Floor.IsDeleted;
            else
                filterExpression = f => f.LangCode == lang && f.Floor != null && f.Floor.IsDeleted;
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Hospitals.GenericReadAll(filterExpression,null, page, pageSize);
        return Ok(result);
    }

    [HttpGet("all", Order = 0312)]
    public async Task<IActionResult> GetAll([FromQuery] bool? isBuildActive, [FromQuery] string status = "active", [FromQuery] string? lang = null, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {
        var resutl = await Data.Floors.ReadAll(isBuildActive,status, lang, page, pageSize);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }

    [HttpGet("search", Order = 0314)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] string? lang, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {
        if (!string.IsNullOrEmpty(name))
            return Ok(await Data.Floors.SearchByName(name));
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Floors.SearchByNameOrCode(searchTerm, lang, page,pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }

    // ============================= put ============================= 4

    [HttpPut("edit/{id}", Order = 0320)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] FloorDto model, int id)
    {
        Response<FloorDto?> response;

        if (file == null)
        {
            response = await Data.Floors.Update(model, id, null);
        }
        else
            response = await Data.Floors.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpPut("edit-translations/{buildId?}", Order = 0322)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<FloorTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Floors.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Floors.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    [HttpPut("deactivate", Order = 0325)]
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
        HosFloor hospital = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Floors.GenericUpdateSinglePropertyById(id.Value, hospital, p => p.IsDeleted));
    }

    // ============================= delete ============================= 2

    [HttpDelete("delete-translat", Order = 0330)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        FloorTranslation entity = new();
        var res = new Response();
        res = await Data.Floors.GenericDelete(entity, t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class