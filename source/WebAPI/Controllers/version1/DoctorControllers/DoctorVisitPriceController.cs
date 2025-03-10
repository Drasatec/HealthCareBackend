﻿using DomainModel.Contracts;
using DomainModel.Entities.DoctorEntities;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1.DoctorControllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class DoctorVisitPriceController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public DoctorVisitPriceController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] DoctorVisitPrice model)
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


    // this method get data from DoctorRepository
    [HttpGet(Order = 0801)]
    public async Task<IActionResult> Get([FromQuery] int? id, int? docId, int? priceCategoryId, int? typeVisitId, int? price, string? lang)
    {
        if (docId < 1 || priceCategoryId < 1 || typeVisitId < 1 || price < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        if (lang != null)
            return Ok(await Data.Doctors.ReadDoctorVisitPrices(id, docId, priceCategoryId, typeVisitId, price, lang));
        else
            return BadRequest(new Error("400", "The lang field is required"));
    }

    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] DoctorVisitPrice model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int? docId, [FromQuery] params int[] id)
    {
        Expression<Func<DoctorVisitPrice, bool>> expression;

        if (docId.HasValue)
            expression = t => t.DoctorId.Equals(docId);
        else
            expression = t => id.Contains(t.Id);


        var res = await Data.Generic.GenericDelete(expression);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class