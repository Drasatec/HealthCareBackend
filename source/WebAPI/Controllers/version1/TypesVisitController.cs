using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class TypesVisitController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public TypesVisitController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] TypesVisit model)
    {

        if (model == null)
        {
            return BadRequest(new Error("400", "model is requerd"));
        }
        var res = await Data.TypesVisits.GenericCreate(model);
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

        Expression<Func<TypesVisit, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.TypesVisitTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.TypesVisitTranslations;

        var result = await Data.TypesVisits.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, [FromQuery] int? page, [FromQuery] int? pageSize)
    {

        var result = await Data.TypesVisits.GenericReadAll<TypesVisitTranslation>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] bool? isActive, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        Expression<Func<TypesVisit, bool>>? filter;
        if (isActive is not null)
        {
            filter = f => f.IsDeleted == !isActive;
        }
        else filter = null;
        //filter = isActive != null ? (filter = f => f.IsDeleted == !isActive) : null;

        var result = await Data.TypesVisits.GenericReadAllWihInclude<TypesVisit>(filter, o => o.Id, inc => inc.TypesVisitTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }

        //var res = new PagedResponse<TypesVisitDto>()
        //{
        //    Total = result.Total,
        //    Page = result.Page,
        //    PageSize = result.PageSize,
        //    Data = TypesVisitDto.ToList(result.Data)
        //};

        return Ok(result);
    }


    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, [FromQuery] string? name, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.TypesVisits.GenericSearchByText<TypesVisitTranslation>(t => t.Name.Contains(name), null, page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
        {
            Expression<Func<TypesVisit, bool>> filter = f => f.CodeNumber.Contains(searchTerm) || f.TypesVisitTranslations.Any(t => t.Name.Contains(searchTerm));
            Expression<Func<TypesVisit, object>> include = i => i.TypesVisitTranslations.Where(l => l.LangCode == lang);

            return Ok(await Data.TypesVisits.GenericSearchByText(filter, include, page, pageSize));
        }
        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] TypesVisit model)
    {
        Response response;

       // TypesVisit entity = model;

        response = await Data.TypesVisits.GenericUpdate(model, c=>c.CreateOn!, d=>d.IsDeleted);

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }



    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<TypesVisitTranslation> translations)
    {
        Response response;

        response = await Data.TypesVisits.GenericUpdate(translations);

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
        TypesVisit entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.TypesVisits.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }

    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] int? parentId, [FromQuery] params int[] translteId)
    {
        // var res = new Response();
        Expression<Func<TypesVisitTranslation, bool>> expression;

        if (parentId.HasValue)
            expression = t => t.TypeVisitId.Equals(parentId);
        else
            expression = t => translteId.Contains(t.Id);

        var res = await Data.TypesVisits.GenericDelete(expression, translteId);

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        //var res = new Response();
        //res = await Data.TypesVisits.GenericDelete<TypesVisitTranslation>(t => t.TypesVisitId.Equals(id));
        var res = await Data.TypesVisits.GenericDelete<TypesVisit>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class