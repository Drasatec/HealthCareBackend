using DomainModel.Contracts;
using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1.DoctorControllers;

[Route("api/DoctorWorkPeriod")]
[ApiController]
[ApiVersion("1.0")]
public class DoctorWorkPeriodController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public DoctorWorkPeriodController(IUnitOfWork data)
    {
        Data = data;
    }


    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] DoctorWorkPeriod model)
    {
        var res = await Data.Generic.GenericCreate(model);

        if (res.Success && res.Value is not null)
        {
            var response = new ResponseId(res.Success, res.Message, res.Value.Id);
            return Created("", response);
        }
        return BadRequest(res);
    }


    // ============================= get ============================= 


    [HttpGet(Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? id, int? docId, int? hosId, int? clinicId, int? periodId, byte? day, string? lang, int? pageSize, int? page)
    {

        var result = await Data.Doctors.ReadDoctorWorkPeriod(id, docId, hosId, clinicId, periodId, day, lang);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }

        return Ok(result);
    }


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> Update([FromForm] DoctorWorkPeriod model)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(model, null);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }


    // ============================= delete ============================= 


    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int? id, int? hosId, int? docId, int? clinicId)
    {
        //Response res;
        Expression<Func<DoctorWorkPeriod, bool>> filter;
        if (id.HasValue)
        {
            filter = f => f.Id.Equals(id);
        }

        else if (docId.HasValue && hosId.HasValue)
        {
            filter = f => f.DoctorId.Equals(docId) && f.HospitalId.Equals(hosId);
        }

        else if (clinicId.HasValue && hosId.HasValue)
        {
            filter = f => f.ClinicId.Equals(clinicId) && f.HospitalId.Equals(hosId);
        }

        else if (clinicId.HasValue && docId.HasValue)
        {
            filter = f => f.ClinicId.Equals(clinicId) && f.DoctorId.Equals(docId);
        }

        else if (hosId.HasValue && clinicId.HasValue && docId.HasValue)
        {
            filter = f => f.HospitalId.Equals(hosId) && f.ClinicId.Equals(clinicId) && f.DoctorId.Equals(docId);
        }
        else
            return BadRequest(new Response(false, "pleas choose  id or (docId with hosId) or (clinicId with hosId) or (clinicId with docId) or (hosId,clinicId,docId)"));

        var res = await Data.Generic.GenericDelete(filter);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class