using DomainModel.Contracts;
using DomainModel.Entities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class SsntypeController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public SsntypeController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] Ssntype model)
    {

        if (model == null)
        {
            return BadRequest(new Error("400", "model is requerd"));
        }
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
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1) return BadRequest(new Error("400", "can not assign 0"));

        Expression<Func<Ssntype, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.SsntypesTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.SsntypesTranslations;

        var result = await Data.Generic.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? page, [FromQuery] int? pageSize)
    {

        var result = await Data.Generic.GenericReadAll<SsntypesTranslation>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        var result = await Data.Generic.GenericReadAllWihInclude<Ssntype>(null, o => o.Id, inc => inc.SsntypesTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }

        return Ok(result);
    }

    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.Generic.GenericSearchByText<SsntypesTranslation>(t => t.Name.Contains(name), null, page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
        {
            Expression<Func<Ssntype, bool>> filter = f => f.CodeNumber.Contains(searchTerm) || f.SsntypesTranslations.Any(t => t.Name.Contains(searchTerm));
            Expression<Func<Ssntype, object>> include = i => i.SsntypesTranslations.Where(l => l.LangCode == lang);

            return Ok(await Data.Generic.GenericSearchByText(filter, include, page, pageSize));
        }
        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] Ssntype model)
    {
        Response response;

        Ssntype entity = model;

        response = await Data.Generic.GenericUpdate(entity, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }


    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<SsntypesTranslation> translations)
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
        Expression<Func<SsntypesTranslation, bool>> expression;

        if (parentId.HasValue)
            expression = t => t.SsntypeId.Equals(parentId);
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
        var res = await Data.Generic.GenericDelete<Ssntype>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class