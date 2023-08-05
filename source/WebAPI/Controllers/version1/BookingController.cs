using DomainModel.Contracts;
using DomainModel.Models;
using DomainModel.Models.Bookings;
using System.Linq.Expressions;
using System.Security.Cryptography;

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

    // ============================= get ============================= 

    [HttpGet(Order = 0801)]
    public async Task<IActionResult> Get([FromQuery] long? id, int? hosId, int? specialtyId, int? ClinicId, int? docId, int? typeVisitId, int? workingPeriodId, int? patientId, short? bookStatusId, byte? dayNumber, string? lang, int? page, int? pageSize)
    {
        //if (docId < 1 || ClinicId < 1 || typeVisitId < 1 || workingPeriodId < 1)
        //    return BadRequest(new Error("400", "can not assign 0"));

        //if (lang != null)
        return Ok(await Data.Appointments.ReadAllAppointments(id, hosId, specialtyId, ClinicId, docId, typeVisitId, workingPeriodId, patientId, bookStatusId, dayNumber, lang, page, pageSize));

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
    public async Task<IActionResult> EditSingleProp([FromQuery] long? bookingId, short? statusId)
    {
        if (!bookingId.HasValue || !statusId.HasValue)
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }

        Booking entity = new() { Id = bookingId.Value, BookingStatusId = statusId.Value };
        return Ok(await Data.Clinics.GenericUpdateSinglePropertyById((int)bookingId.Value, entity, p => p.BookingStatusId));
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