using DomainModel.Contracts;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CurrencyController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public CurrencyController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] Currency model)
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

        var result = await Data.Generic.GenericReadById<Currency>(f => f.Id.Equals(id), null);

        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] string? currencyCode, int? pageSize, int? page)
    {
        Expression<Func<Currency, bool>> expression;
        if (currencyCode != null)
        {
            expression = f => f.Id.Equals(currencyCode);
        }
        else
            expression = null!;
        var result = await Data.Generic.GenericReadAll(expression, null, page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(result);
    }

    // ============================= put ============================= 

    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> Update([FromForm] Currency model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] params int[] id)
    {
        var res = await Data.Generic.GenericDelete<Currency>(t => id.Contains(t.Id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class