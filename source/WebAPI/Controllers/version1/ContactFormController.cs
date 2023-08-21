using DomainModel.Contracts;
using DomainModel.Entities.HosInfo;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ContactFormController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public ContactFormController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] ContactForm model)
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
    public async Task<IActionResult> GetById([FromQuery] int? id)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Generic.GenericReadById<ContactForm>(f => f.Id.Equals(id), null);

        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? hosId, int? pageSize, int? page)
    {
        Expression<Func<ContactForm, bool>> expression;
        if (hosId != null)
        {
            expression = f => f.HospitalId.Equals(hosId);
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

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] params int[] id)
    {
        var res = await Data.Generic.GenericDelete<ContactForm>(t => id.Contains(t.Id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class
