﻿using DomainModel.Contracts;
using DomainModel.Models;
using DomainModel.Models.Bookings;
using DomainModel.Models.Common;
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
    public async Task<IActionResult> AddSingle([FromForm] BookingRequestDto model, [FromQuery] int? clinicId, [FromQuery] string? lang)
    {
        if (model == null)
        {
            return BadRequest(new Error("400", "model is requerd"));
        }

        var res = await Data.Appointments.CreateAppointments(model);
        long id = 0;

        if (res != null && res.Success)
        {
            if (clinicId.HasValue && lang is not null)
            {
                return Ok(await Data.Clinics.ClinicByIdWithParentsNames(clinicId, lang));
            }
            else
            {

                if (res.Id.HasValue)
                    id = res.Id.Value;
                var response = new ResponseLongId(res.Success, res.Message, id);
                return Created("", response);
            }
        }

        return BadRequest(res);
    }
    

    [HttpPost("add-body", Order = 0801)]
    public async Task<IActionResult> AddSingleFromBody([FromBody] BookingRequestDto model)
    {
        var res = await Data.Appointments.CreateAppointments(model);
        if (res != null && res.Success)
        {
            return Ok(res);
        }
        return BadRequest(res);
    }

    // ============================= get ============================= 

    [HttpGet("get-id")]
    public async Task<IActionResult> GetByid([FromQuery] int Id, string? lang)
    {
        //return Ok(filterOptions);
        var res = await Data.Appointments.ReadAppointmentById(Id, lang);
        if(res == null)
        {
            return BadRequest(new Response(false, "pleas check you inputs"));
        }
        return Ok(res);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AppointmentFilterOptions filterOptions, [FromQuery] PaginationOptions pageOptions, string? lang)
    {
        //return Ok(filterOptions);
        var res = await Data.Appointments.ReadAllAppointments(filterOptions, pageOptions, lang);
        if(res == null)
        {
            return BadRequest(new Response(false, "pleas check you inputs"));
        }
        return Ok(res);
    }

    // ============================= put ============================= 

    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> Update([FromForm] BookingRequestDto model)
    {
        Response response;
        var entity = (Booking)model;
        response = await Data.Generic.GenericUpdate(entity, en => en.PatientId, en => en.HospitalId);

        if (!response.Success)
            return BadRequest(response);

        return Created("", response);
    }

    [HttpPut("edit-status", Order = 0925)]
    public async Task<IActionResult> EditSingleProp([FromQuery] long? bookingId, short? statusId, string statusReason = "")
    {
        if (!bookingId.HasValue || !statusId.HasValue)
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }

        Booking entity = new() { Id = bookingId.Value, BookingStatusId = statusId.Value, StatusReason = statusReason };
        return Ok(await Data.Clinics.GenericUpdatePropertiesById((int)bookingId.Value, entity, sId=>sId.BookingStatusId, sr => sr.StatusReason));
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