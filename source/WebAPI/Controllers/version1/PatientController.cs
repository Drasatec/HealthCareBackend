﻿using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Patients;
using DomainModel.Models.Users;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PatientController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public PatientController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0901)]
    public async Task<IActionResult> AddSingleFromAdmin([FromForm] IFormFile? file, [FromForm] PatientDto model)
    {

        ResponseId response;

        if (file == null)
        {
            response = await Data.Patients.CreateWithImage(model);
        }
        else
            response = await Data.Patients.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    //[Authorize(Roles = "User")]
    [HttpPost("add-patient-data", Order = 0901)]
    public async Task<IActionResult> AddSingleFromPatient([FromForm] PatientDto model, [FromForm] string? userId)
    {
        var response = new Response<ApplicationUser>();
        if (string.IsNullOrEmpty(userId))
        {
            userId = User.FindFirst("uid")?.Value;
        }

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new Response<int>(false, "user id id is required"));
        }

        var res = await Data.Patients.CreateFromPatient(model, userId);
        if(res.Success)
        {
            return Created("rahma.care", (res));
        }
        else
            return BadRequest(new ResponseId(false, "We could not save the patient data",0));

        //if (res.Success)
        //{
        //    response.Value = await Data.Generic.GenericReadSingle<User, ApplicationUser>(u => u.Id == userId, (user) =>
        //    new ApplicationUser
        //    {
        //        Id = userId,
        //        Email = user.Email,
        //        FullName = user.FullName,
        //        UserName = user.UserName,
        //    });
        //    return Created("rahma.care", response);
        //}
        //else
        //    return BadRequest(new Response<int>(false, "We could not save the patient data"));
    }

    // ============================= get ============================= 

    [HttpGet(Order = 0901)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Patients.ReadById(id, lang);
        return Ok(result);
    }


    [HttpGet("names", Order = 0911)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, int? patientId, int? page, int? pageSize)
    {
        Expression<Func<PatientTranslation, bool>> filterExpression;
        if (patientId.HasValue)
        {
            filterExpression = f => f.PatientId== patientId && f.LangCode == lang;
        }
        else
            filterExpression = f => f.LangCode == lang;

        var result = await Data.Patients.GenericReadAll(filterExpression, null!, page, pageSize);
        return Ok(result);
    }

    [HttpGet("all", Order = 0912)]
    public async Task<IActionResult> GetAll([FromQuery(Name = "specialtyId")] int? parentId, bool? appearance, byte? patientStatus, int? pageSize, int? page, string? lang)
    {
        var resutl = await Data.Patients.ReadAll(parentId, appearance, patientStatus, lang, pageSize, page);
        if (resutl is null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }


    [HttpGet("search", Order = 0914)]
    public async Task<IActionResult> Search([FromQuery(Name = "specialtyId")] int? parentId, [FromQuery] string? searchTerm, [FromQuery] string? name, bool? active, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string? lang)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.Patients.GenericSearchByText<PatientTranslation>(parentId,
                t => t.FullName.Contains(name),
                null,
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Patients.SearchByNameOrCode(active, searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0920)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] PatientDto model, int id)
    {
        Response<PatientDto?> response;

        if (file == null)
        {
            response = await Data.Patients.Update(model, id, null);
        }
        else
            response = await Data.Patients.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }



    [HttpPut("edit-translations/{buildId?}", Order = 0922)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<PatientTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Patients.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Patients.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }



    [HttpPut("edit-status", Order = 0925)]
    public async Task<IActionResult> EditSingleProp([FromQuery] int patientId, [FromQuery] byte patientStatus)
    {
        if (patientId < 1 || patientStatus < 1)
        {
            return BadRequest(new Response(false, "id field is requerd and value > 0"));
        }

        Patient entity = new() { Id = patientId, PatientStatus = patientStatus };

        return Ok(await Data.Patients.GenericUpdateSinglePropertyById(patientId, entity, p => p.PatientStatus!));
    }


    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0930)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        var res = new Response();
        res = await Data.Patients.GenericDelete<PatientTranslation>(t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class