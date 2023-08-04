using DomainModel.Entities.TranslationModels;
using DomainModel.Models.Dtos;
using DomainModel.Models;
using System.Linq.Expressions;
using DomainModel.Contracts;
using DomainModel.Models.Doctors;
using DomainModel.Services;
using DomainModel.Helpers;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class DoctorController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public DoctorController(IUnitOfWork data)
    {
        Data = data;
    }

    #region Doctor

    // ============================= post ============================= 
    [HttpPost("add", Order = 0901)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] DoctorDto model)
    {

        Response<DoctorDto> response;

        if (model == null)
        {
            return BadRequest(new Error("400", "clinic is requerd"));
        }
        if (file == null)
        {
            response = await Data.Doctors.CreateWithImage(model);
        }
        else
            response = await Data.Doctors.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }

    // ============================= get ============================= 


    [HttpGet(Order = 0901)]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] string? lang)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Doctors.ReadById(id, lang);
        return Ok(result);
    }

    [HttpGet("names", Order = 0911)]
    public async Task<IActionResult> GetAllNames([FromQuery] int? hosId, int? specialtyId, string? lang, int? page, int? pageSize)
    {
        if (lang == null)
        {
            return BadRequest(new Error("400", "lang is requied"));
        }

        var result = await Data.Doctors.ReadDoctorsNames(hosId, specialtyId, lang, page, pageSize);
        //Expression<Func<DoctorTranslation, bool>> filterExpression;
        //if (specialtyId.HasValue)
        //{
        //    filterExpression = f =>
        //    f.LangCode == lang &&
        //    f.Doctor != null &&
        //    f.Doctor.SpecialtiesDoctors.Any(s => s.MedicalSpecialtyId == specialtyId);
        //}
        //else
        //    filterExpression = f => f.LangCode == lang;

        //var result = await Data.Doctors.GenericReadAll(filterExpression, null!, page, pageSize);
        return Ok(result);
    }



    [HttpGet("all", Order = 0912)]
    public async Task<IActionResult> GetAll(int? hosId, int? specialtyId, [FromQuery] bool? appearanceOnSite, [FromQuery] string? status, [FromQuery] int? pageSize, [FromQuery] int? page, [FromQuery] string? lang)
    {
        var resutl = await Data.Doctors.ReadAll(hosId, specialtyId, appearanceOnSite, status, lang, pageSize, page);
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
            return Ok(await Data.Doctors.GenericSearchByText<DoctorTranslation>(parentId,
                t => t.FullName.Contains(name),
                ho => ho.Doctor != null && ho.Doctor.SpecialtiesDoctors.Any(s => s.MedicalSpecialtyId.Equals(parentId)),
                page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
            return Ok(await Data.Doctors.SearchByNameOrCode(active, searchTerm, lang, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    [HttpGet("find-doctor", Order = 0914)]
    public async Task<IActionResult> FindDoctor([FromQuery] int? hosId, int? specialtyId, int? docId, int? workingPeriodId, byte? day, short? doctorsDegreeId, byte? gender, int? page, int? pageSize, string? lang)
    {
        var resutl = await Data.Doctors.FindDoctor(hosId, specialtyId, docId, workingPeriodId, day, doctorsDegreeId, gender, page, pageSize, lang);
        if (resutl is null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0920)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] DoctorDto model, int id)
    {
        Response<DoctorDto?> response;

        if (file == null)
        {
            response = await Data.Doctors.Update(model, id, null);
        }
        else
            response = await Data.Doctors.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }


    [HttpPut("edit-translations/{buildId?}", Order = 0922)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<DoctorTranslation> translations, int? buildId)
    {
        Response response;

        response = await Data.Doctors.GenericUpdate(translations);
        if (response.Success && buildId.HasValue)
        {
            var entity = await Data.Doctors.ReadById(buildId);
            return Created("fawzy", entity);
        }
        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    [HttpPut("deactivate", Order = 0925)]
    public async Task<IActionResult> EditSingleProp([FromQuery] int? id, [FromQuery] string status)
    {
        if (!id.HasValue)
        {
            return BadRequest(new Response(false, "id field is requerd"));
        }
        bool isDeleted;
        if (status == "active")
            isDeleted = false;
        else if (status == "inactive")
            isDeleted = true;
        else
        {
            return BadRequest(new Response(false, "The status field in this context allows the values 'active' or 'inactive' "));
        }
        Doctor entity = new() { Id = id.Value, IsDeleted = isDeleted };
        return Ok(await Data.Doctors.GenericUpdateSinglePropertyById(id.Value, entity, p => p.IsDeleted));
    }


    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0930)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] params int[] translteId)
    {
        var res = new Response();
        res = await Data.Doctors.GenericDelete<DoctorTranslation>(t => translteId.Contains(t.Id), translteId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    #endregion



}// end class