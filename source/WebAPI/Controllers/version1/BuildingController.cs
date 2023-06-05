using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers.version1;

[Route("api",Order =03)]
[ApiController]
[ApiVersion("1.0")]

public class BuildingController : ControllerBase
{
    public BuildingController()
    {
        
    }

    [HttpGet("building/1", Order = 0301)]
    public IActionResult _1GetById1([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("2.....B");
    }


    [HttpGet("buildings/2/{id?}", Order = 0302)]
    //[SwaggerOperation(OperationId = "2", Tags = new[] { "bUpdate" })]
    public IActionResult GetById6([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("3.....A");
    }

    [HttpGet("building/3", Order = 0303)]
    public IActionResult _1GetById5([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("2.....B");
    }

    [HttpGet("building/4", Order = 0304)]
    
    public IActionResult GetById2([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("3.....A");
    }

    [HttpGet("building/5", Order = 0305)]
    [SwaggerOperation(Tags = new[] { "Update" })]

    public IActionResult _1GetById3([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("2.....B");
    }


    [HttpGet("building/6", Order = 0306)]
    [SwaggerOperation(Tags = new[] { "Update" })]
    public IActionResult GetById4([FromQuery] int id, [FromQuery] string? lang)
    {

        return Ok("3.....A");
    }
    





}
