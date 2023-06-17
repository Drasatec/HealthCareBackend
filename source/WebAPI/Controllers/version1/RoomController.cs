using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Models;
using DomainModel.Models.Rooms;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]", Order = 04)]
[ApiController]
[ApiVersion("1.0")]
public class RoomController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public RoomController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 1

    [HttpPost("add", Order = 0401)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] RoomDto model)
    {

        ResponseId response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Room is requerd"));
        }
        if (file == null)
        {
            response = await Data.Rooms.CreateWithImage(model);
        }
        else
            response = await Data.Rooms.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    // ============================= get ============================= 4

    [HttpGet(Order = 0401)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Rooms.ReadById(id, lang);
        return Ok(result);
    }

    [HttpGet("names", Order = 0411)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? floorId, [FromQuery] int? page, [FromQuery] int? pageSize )
    {

        Expression<Func<RoomTranslation, bool>> filterExpression;
        if (floorId.HasValue)
        {
            filterExpression = f => f.LangCode == lang && f.Room != null && f.Room.FloorId.Equals(floorId);
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Floors.GenericReadAll(filterExpression, (hos) =>
        new RoomTranslation
        {
            Id = hos.Id,
            LangCode = hos.LangCode,
            Name = hos.Name,
            RoomId = hos.RoomId,
        }, page, pageSize);
        return Ok(result);
    }

    [HttpGet("all", Order = 0412)]
    public async Task<IActionResult> GetAll([FromQuery] int? roomTypeId, [FromQuery(Name = "floorId")] int? parentId, [FromQuery(Name = "isFloorActive")] bool? isBaseActive, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int page , [FromQuery] string? lang)
    {
        var resutl = await Data.Rooms.ReadAll(roomTypeId, parentId, isBaseActive, status, lang, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }


    [HttpGet("search", Order = 0414)]
    public async Task<IActionResult> Search([FromQuery(Name = "floorId")] int? parentId, [FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] string? lang, bool? active, [FromQuery] int? page , [FromQuery] int? pageSize)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.Rooms.GenericSearchByText<RoomTranslation>(
                parentId,
                t => t.Name.Contains(name),
                en => en.Room != null && en.Room.FloorId.Equals(parentId),
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Rooms.SearchByNameOrCode(active,searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }

    // ============================= put ============================= 4

    [HttpPut("edit/{id}", Order = 0420)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] RoomDto model, int id)
    {
        Response<RoomDto?> response;

        if (file == null)
        {
            response = await Data.Rooms.Update(model, id, null);
        }
        else
            response = await Data.Rooms.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpPut("edit-translations/{buildId?}", Order = 0422)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<RoomTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Rooms.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Rooms.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    [HttpPut("deactivate", Order = 0425)]
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
        HosRoom entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Rooms.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }

    // ============================= delete ============================= 2

    [HttpDelete("delete-translat", Order = 0430)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        RoomTranslation entity = new();
        var res = new Response();
        res = await Data.Rooms.GenericDelete(entity, t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class