using DomainModel.Contracts;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class LanguageController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public LanguageController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] Language model)
    {
        var res = await Data.Generic.GenericCreate(model);

        if (res.Success)
        {
            return Created("add lang", res);
        }
        return BadRequest(res);
    }

    // ============================= get ============================= 

    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] string? code)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest(new Error("400", "can not add with Null Or Empty "));

        var result = await Data.Generic.GenericReadById<Language>(f => f.Code.Equals(code), null);

        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] string? code, int? pageSize, int? page)
    {
        Expression<Func<Language, bool>> expression;

        if (code != null)
        {
            expression = f => f.Code.Equals(code);
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
    public async Task<IActionResult> Update([FromForm] Language model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] params string[] code)
    {
        var res = await Data.Generic.GenericDelete<Language>(t => code.Contains(t.Code));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class