using DataAccess.Contexts;
using DomainModel.Contracts;
using DomainModel.Entities.SettingsEntities;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ConfirmationOptionController : ControllerBase
{
    private readonly IUnitOfWork Data;
    // private readonly AppDbContext context;

    public ConfirmationOptionController(IUnitOfWork data)
    {
        Data = data;
        // this.context = context;
    }

    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] ConfirmationOption model)
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

        var result = await Data.Generic.GenericReadById<ConfirmationOption>(f => f.Code.Equals(code), null);

        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] string? code, int? pageSize, int? page)
    {
        Expression<Func<ConfirmationOption, bool>> expression;

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
    public async Task<IActionResult> Update([FromForm] ConfirmationOption model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, cp1 => cp1.Code,cp2=>cp2.Chosen);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    [HttpPut("edit-chosen", Order = 0925)]
    public async Task<IActionResult> EditSingleProp([FromQuery] string? code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }
        await Data.Generic.UpdateSinglePropertyInEntities<ConfirmationOption>(
                filter: option => option.Chosen == true,
                updateAction: option =>
                {
                    option.Chosen = false;
                });

        ConfirmationOption entity = new() { Code = code, Chosen = true };
        var res = await Data.Doctors.GenericUpdateSinglePropertyById(0, entity, p => p.Chosen);
        if (res!= null && res.Success)
        {
            res.Message= $"Update on entity with code: {code}";
            return Ok(res);
        }
        return BadRequest(res);
    }

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] params string[] code)
    {
        var res = await Data.Generic.GenericDelete<ConfirmationOption>(t => code.Contains(t.Code));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }





}// end class