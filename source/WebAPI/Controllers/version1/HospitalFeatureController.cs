using DataAccess;
using DomainModel.Contracts;
using DomainModel.Entities.HosInfo;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class HospitalFeatureController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public HospitalFeatureController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] HospitalFeature model)
    {

        var name = await DataAccessImageService.SaveSingleImage(file?.OpenReadStream());
        model.Photo = name;

        var res = await Data.Generic.GenericCreate(model);
        int id = 0;

        if (res.Success)
        {
            if (res.Value is not null)
                id = res.Value.Id;
            var response = new ResponseId(res.Success, res.Message, id);
            return Created("", response);
        }
        return BadRequest(res);

    }


    // ============================= get ============================= 


    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] int id, string? lang)
    {
        if (id < 1) return BadRequest(new Error("400", "can not assign 0"));

        Expression<Func<HospitalFeature, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.HospitalFeatureTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.HospitalFeatureTranslations;

        var result = await Data.Generic.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? hosId, int? pageSize, int? page, string? lang)
    {
        Expression<Func<HospitalFeature, bool>>? expression;
        if (hosId.HasValue)
        {
            expression = f => f.HospitalId == hosId.Value;
        }
        else expression = null;


        var result = await Data.Generic.GenericReadAllWihInclude(expression, o => o.Id, inc => inc.HospitalFeatureTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(result);
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0920)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] HospitalFeature model, int id)
    {
        Response<HospitalFeature?> response;

        if (file == null)
        {
            response = await Data.Hospitals.UpdateHospitalFeature(model, id, null);
        }
        else
            response = await Data.Hospitals.UpdateHospitalFeature(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<HospitalFeatureTranslation> translations)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(translations);

        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] int? parentId, [FromQuery] params int[] translteId)
    {
        Expression<Func<HospitalFeatureTranslation, bool>> expression;

        if (parentId.HasValue && parentId > 0)
            expression = t => t.FeatureId.Equals(parentId);
        else
            expression = t => translteId.Contains(t.Id);

        var res = await Data.Generic.GenericDelete(expression, translteId);

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        if (id < 1)
            return BadRequest("id less than 0");
        var res = await Data.Generic.GenericDelete<HospitalFeature>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class
