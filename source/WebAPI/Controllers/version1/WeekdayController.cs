using DomainModel.Contracts;
using DomainModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class WeekdayController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public WeekdayController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] Weekday model)
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
    public async Task<IActionResult> GetById([FromQuery] int? id)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Generic.GenericReadById<Weekday>(f => f.Id.Equals(id), null);

        return Ok(result);
    }

    [HttpGet("day",Order = 0801)]
    public async Task<IActionResult> GetByDayNumber([FromQuery] byte? day)
    {
        if (day < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        Expression<Func<Weekday, bool>> filter;

        if (day.HasValue)
            filter = f => f.DayNumber.Equals(day);
        else
           return BadRequest(new Error("400", "can not assign null"));

        var result = await Data.Generic.GenericReadAll(filter, null,null,null);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, int? page, int? pageSize)
    {
        var result = await Data.Generic.GenericReadAll<Weekday>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }

    // not working
    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] bool? isActive, int? pageSize, int? page, string? lang)
    {

        var result = await Data.Generic.GenericReadAll<Weekday>(t => t.LangCode.Equals(lang), null, page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(result);
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> Update([FromForm] Weekday model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    // ============================= delete ============================= 

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var res = await Data.Generic.GenericDelete<Weekday>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class