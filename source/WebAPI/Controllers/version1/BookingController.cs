﻿using DomainModel.Contracts;
using DomainModel.Models;
using DomainModel.Models.Bookings;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public BookingController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 

    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] BookingRequestDto model)
    {
        if (model == null)
        {
            return BadRequest(new Error("400", "model is requerd"));
        }

        var entity = (Booking)model;
        var res = await Data.Generic.GenericCreate(entity);
        long id = 0;

        if (res.Success)
        {
            if (res.Value is not null)
                id = res.Value.Id;
            var response = new ResponseLongId(res.Success, res.Message, id);
            return Created("", response);
        }
        return BadRequest(res);
    }

    // ============================= get ============================= 


    // this method get data from DoctorRepository
    [HttpGet(Order = 0801)]
    public async Task<IActionResult> Get([FromQuery] int? id, int? hosId, int? specialtyId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId, string? lang)
    {
        if (docId < 1 || ClinicId < 1 || typeVisitId < 1 || workingPeriodId < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        if (lang != null)
            return Ok(await Data.Appointments.ReadAllAppointments(id,hosId, specialtyId, ClinicId,docId, typeVisitId, workingPeriodId, patientId, bookStatusId , lang));
        else
            return BadRequest(new Error("400", "The lang field is required"));
    }

    // ============================= put ============================= 

    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> Update([FromForm] BookingRequestDto model)
    {
        Response response;
        var entity = (Booking)model;
        response = await Data.Generic.GenericUpdate(entity, en=>en.PatientId,en=>en.HospitalId);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    // ============================= delete ============================= 

    [HttpDelete(Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int? patientId, [FromQuery] params long[] id)
    {
        Expression<Func<Booking, bool>> expression;

        if (patientId.HasValue)
            expression = t => t.PatientId.Equals(patientId.Value);
        else
            expression = t => id.Contains(t.Id);


        var res = await Data.Generic.GenericDelete(expression);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class