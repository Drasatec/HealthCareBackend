using DomainModel.Contracts;
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

    // ============================= post ============================= 



    [HttpPost("add", Order = 0301)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] FloorDto model)
    {

        ResponseId response;

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


    // ============================= get ============================= 


    [HttpGet(Order = 0301)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Floors.ReadById(id, lang);
        return Ok(result);
    }



    [HttpGet("names", Order = 0311)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? buildId, [FromQuery] int page = 1, [FromQuery] int pageSize = Constants.PageSize)
    {

        Expression<Func<FloorTranslation, bool>> filterExpression;
        if (buildId.HasValue)
        {
            filterExpression = f => f.LangCode == lang && f.Floor != null && f.Floor.BuildId.Equals(buildId);
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Floors.GenericReadAll(filterExpression, (hos) =>
        new FloorTranslation
        {
            Id = hos.Id,
            LangCode = hos.LangCode,
            Name = hos.Name,
            FloorId = hos.FloorId,
        }, page, pageSize);
        return Ok(result);
    }



    [HttpGet("all", Order = 0312)]
    public async Task<IActionResult> GetAll([FromQuery(Name = "buildId")] int? parentId, [FromQuery(Name = "isBuildActive")] bool? isBaseActive, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int page , [FromQuery] string? lang)
    {
        var resutl = await Data.Floors.ReadAll(parentId, isBaseActive, status, lang, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }



    [HttpGet("search", Order = 0314)]
    public async Task<IActionResult> Search([FromQuery(Name = "buildId")] int? parentId, [FromQuery] string? searchTerm, [FromQuery] string? name, bool? active, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            //return Ok(await Data.Floors.SearchByName(name));
            return Ok(await Data.Floors.GenericSearchByText<FloorTranslation>(parentId,
                t => t.Name.Contains(name),
                ho => ho.Floor != null && ho.Floor.BuildId.Equals(parentId),
                page,pageSize));

        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Floors.SearchByNameOrCode(active,searchTerm, lang, page,pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }



    // ============================= put ============================= 



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
        HosFloor entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Floors.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }


    // ============================= delete ============================= 


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