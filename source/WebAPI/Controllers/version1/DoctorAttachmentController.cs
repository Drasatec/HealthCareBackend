using DomainModel.Contracts;
using DomainModel.Helpers;
using DomainModel.Models;
using DomainModel.Models.Doctors;
using DomainModel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/doctor")]
[ApiController]
[ApiVersion("1.0")]
public class DoctorAttachmentController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public DoctorAttachmentController(IUnitOfWork data)
    {
        this.Data = data;
    }

    [HttpPost("add_attachment", Order = 0901)]
    public async Task<IActionResult> AddSingleAttachment([FromForm] IFormFile? file, [FromForm] DoctorAttachment model)
    {

        if (file != null)
        {
            var fileName = Helper.GenerateFileName(file.FileName);
            _ = FileService.SaveSingleFile(file.OpenReadStream(), fileName);
            model.AttachFileName = fileName;
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

    [HttpGet("get_attachment", Order = 09011)]
    public async Task<IActionResult> GetByAttachmentId([FromQuery] int id)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.Generic.GenericReadById<DoctorAttachment>(f => f.Id == id, null);
        return Ok(result);
    }

    [HttpPut("edit_attachment", Order = 09201)]
    public async Task<IActionResult> UpdateAttachment([FromForm] IFormFile? file, [FromForm] DoctorAttachment model)
    {
        Response<DoctorDto?> response;

        if (file == null)
        {
            response = await Data.Doctors.UpdateAttachment(model, "", null);
        }
        else
            response = await Data.Doctors.UpdateAttachment(model, file.FileName, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }

    [HttpDelete("delete_attachment", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await Data.Generic.GenericReadById<DoctorAttachment>(f => f.Id == id, null);
        if (result != null)
        {
            if (!string.IsNullOrEmpty(result.AttachFileName))
                FileService.RemoveFile(result.AttachFileName);
        }
        else
            return BadRequest();

        var res = await Data.Generic.GenericDelete<DoctorAttachment>(f => f.Id == id);
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }
}
