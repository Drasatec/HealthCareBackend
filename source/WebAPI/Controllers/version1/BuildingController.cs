using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Buildings;
namespace WebAPI.Controllers.version1;

[Route("api",Order =02)]
[ApiController]
[ApiVersion("1.0")]
public class BuildingController : ControllerBase
{
    public IUnitOfWork Data { get; }

    public BuildingController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 1

    [HttpPost("building/add", Order = 0201)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] BuildingDto model)
    {

        Response response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Data.Buildings.CreateWithImage(model);
        }
        else
            response = await Data.Buildings.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    // ============================= get ============================= 4

    [HttpGet("building/", Order = 0201)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Buildings.ReadById(id, lang);
        return Ok(result);
    }

    [HttpGet("building/names", Order = 0211)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang)
    {
        var result = await Data.Buildings.GenericReadAll<BuildingTranslation>(f => f.LangCode == lang, (b) =>
        new BuildingTranslation
        {
            Id = b.Id,
            Name = b.Name,
            BuildeingId = b.BuildeingId,
            LangCode = b.LangCode,
        });
        return Ok(result);
    }

    [HttpGet("buildings/all", Order = 0212)]
    public async Task<IActionResult> GetAll([FromQuery] bool? isHosActive,[FromQuery] string status = "active", [FromQuery] string? lang = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var hospitals = await Data.Buildings.ReadAll(isHosActive, status, lang, page, pageSize);
        return Ok(hospitals);
    }

    [HttpGet("buildings/search", Order = 0214)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
            return Ok(await Data.Buildings.SearchByName(name));
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Buildings.SearchByNameOrCode(searchTerm, lang));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }

    // ============================= put ============================= 4

    [HttpPut("building/edit/{id}", Order = 0220)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] BuildingDto model, int id)
    {
        Response<BuildingDto?> response;

        if (file == null)
        {
            response = await Data.Buildings.UpdateHospital(model, id, null);
        }
        else
            response = await Data.Buildings.UpdateHospital(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpPut("building/edit-translations/{buildId?}", Order = 0222)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<BuildingTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Buildings.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Buildings.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    [HttpPut("building/deactivate", Order = 0225)]
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
        return Ok(await Data.Buildings.GenericUpdateSinglePropertyById(id.Value, hospital, p => p.IsDeleted));
    }

    // ============================= delete ============================= 2

    [HttpDelete("building/delete-translat", Order = 0230)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        BuildingTranslation entity = new();
        var res = new Response();
        res = await Data.Buildings.GenericDelete(entity, t => translteId.Contains(t.Id), translteId);
        // res = await Data.Buildings.DeleteTranslat(translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class
