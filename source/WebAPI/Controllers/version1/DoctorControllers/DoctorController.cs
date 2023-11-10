using DomainModel.Contracts;
using DomainModel.Entities.DoctorEntities;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using DomainModel.Models.Doctors;

namespace WebAPI.Controllers.version1.DoctorControllers;

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
    public async Task<IActionResult> GetAll([FromQuery] int? hosId, int? specialtyId, byte? dayId, byte? genderId, short? degreeId, bool? appearanceOnSite, string? status, int? pageSize, int? page, string? lang)
    {
        var resutl = await Data.Doctors.ReadAll(hosId, specialtyId, dayId, genderId, degreeId, appearanceOnSite, status, lang, pageSize, page);
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
        else if (!string.IsNullOrEmpty(searchTerm))
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

    [HttpGet("find-doctor_update", Order = 0914)]
    public async Task<IActionResult> FindDoctor2([FromQuery] int? hosId, int? specialtyId, int? docId, int? workingPeriodId, byte? day, short? doctorsDegreeId, byte? gender, int? page, int? pageSize, string? lang)
    {
        var resutl = await Data.Doctors.FindDoctor2(hosId, specialtyId, docId, workingPeriodId, day, doctorsDegreeId, gender, page, pageSize, lang);
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

    #region workHos
    [HttpPost("WorkHospital-add", Order = 0801)]
    public async Task<IActionResult> AddDocInHos([FromForm] DoctorsWorkHospital model)
    {
        var res = await Data.Generic.GenericCreate(model);
        int id = 0;

        if (res.Success)
        {
            if (res.Value is not null)
                id = res.Value.DoctorId;
            var response = new ResponseId(res.Success, res.Message, id);
            return Created("", response);
        }
        return BadRequest(res);
    }

    [HttpDelete("WorkHospital-delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int doctorId, int hospitalId)
    {
        var res = await Data.Generic.GenericDelete<DoctorsWorkHospital>(f => f.DoctorId == doctorId && f.HospitalId == hospitalId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }
    #endregion

    #region Specialty
    [HttpPost("specialy-add", Order = 0801)]
    public async Task<IActionResult> AddSpecialtyToDoc([FromForm] SpecialtiesDoctor model)
    {
        var res = await Data.Generic.GenericCreate(model);
        int id = 0;

        if (res.Success)
        {
            if (res.Value is not null)
                id = res.Value.DoctorId;
            var response = new ResponseId(res.Success, res.Message, id);
            return Created("", response);
        }
        return BadRequest(res);
    }

    [HttpDelete("specialy-delete", Order = 0830)]
    public async Task<IActionResult> DeleteSpecialty([FromQuery] int doctorId, int specialtyId)
    {
        var res = await Data.Generic.GenericDelete<SpecialtiesDoctor>(f => f.DoctorId == doctorId && f.MedicalSpecialtyId == specialtyId);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    #endregion



}// end class