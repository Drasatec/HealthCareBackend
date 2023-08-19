using DomainModel.Contracts;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class EmployeeStatusController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public EmployeeStatusController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] EmployeesStatus model)
    {
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

        Expression<Func<EmployeesStatus, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.EmployeesStatusTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.EmployeesStatusTranslations;

        var result = await Data.Generic.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, int? page, int? pageSize)
    {

        var result = await Data.Generic.GenericReadAll<EmployeesStatusTranslation>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize, int? page, string? lang)
    {

        var result = await Data.Generic.GenericReadAllWihInclude<EmployeesStatus>(null, o => o.Id, inc => inc.EmployeesStatusTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }


        return Ok(result);
    }


    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, string? name, int? page, int? pageSize, string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.Generic.GenericSearchByText<EmployeesStatusTranslation>(t => t.StatusName.Contains(name), null, page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
        {
            Expression<Func<EmployeesStatus, bool>> filter = f => f.EmployeesStatusTranslations.Any(t => t.StatusName.Contains(searchTerm));
            Expression<Func<EmployeesStatus, object>> include = i => i.EmployeesStatusTranslations.Where(l => l.LangCode == lang);

            return Ok(await Data.Generic.GenericSearchByText(filter, include, page, pageSize));
        }
        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] EmployeesStatus model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, c => c.CreateOn!);

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<EmployeesStatusTranslation> translations)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(translations);

        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] short? parentId, [FromQuery] params int[] translteId)
    {
        Expression<Func<EmployeesStatusTranslation, bool>> expression;

        if (parentId.HasValue)
            expression = t => t.EmployeeStatusId.Equals(parentId);
        else
            expression = t => translteId.Contains(t.Id);

        var res = await Data.Generic.GenericDelete(expression, translteId);

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] short id)
    {
        var res = await Data.Generic.GenericDelete<EmployeesStatus>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class