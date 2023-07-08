using DomainModel.Models.MedicalSpecialteis;
using DomainModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using DomainModel.Contracts;
using DomainModel.Models.Client;

namespace WebAPI.Controllers.version1;

[Route("api/HosClient")]
[ApiController]
[ApiVersion("1.0")]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public ClientController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] HotClientDto model)
    {

        ResponseId response;

        if (model == null)
        {
            return BadRequest(new Error("400", "Hospital is requerd"));
        }
        if (file == null)
        {
            response = await Data.HosClients.CreateWithImage(model);
        }
        else
            response = await Data.HosClients.CreateWithImage(model, file.OpenReadStream());

        return Created("fawzy", response);
    }


    // ============================= get ============================= 


    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        if (id < 1)
            return BadRequest(new Error("400", "can not assign 0"));

        var result = await Data.HosClients.ReadById(id);
        return Ok(result);
    }



    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] int? page, [FromQuery] int? pageSize)
    {

        var result = await Data.HosClients.GenericReadAll<HosClient>(null, null!, page, pageSize);
        var dto = HotClientDto.ToList(result);
        return Ok(dto);
    }

    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] byte? status, int? pageSize, int? page)
    {
        var resutl = await Data.HosClients.ReadAll(status, pageSize, page);
        if (resutl == null)
        {
            return Ok(new Response(true, "no content"));
        }
        return Ok(resutl);
    }



    [HttpGet("search", Order = 0814)]
    public async Task<IActionResult> Search([FromQuery] string? searchTerm, string? name, byte? status, int? page, int? pageSize)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return Ok(await Data.HosClients.GenericSearchByText<HosClient>(
                n => n.NameEn.Contains(name) || n.NameOriginalLang.Contains(name),
                null, page, pageSize));
        }
        else if (!string.IsNullOrEmpty(searchTerm))
            return Ok(await Data.HosClients.SearchByNameOrCode(status, searchTerm, page, pageSize));

        return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    }


    // ============================= put ============================= 


    [HttpPut("edit/{id}", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] HotClientDto model, int id)
    {
        Response<HotClientDto?> response;

        if (file == null)
        {
            response = await Data.HosClients.Update(model, id, null);
        }
        else
            response = await Data.HosClients.Update(model, id, file.OpenReadStream());

        if (!response.Success)
            return BadRequest(response);

        return Created("https//fawzy", response);
    }


    [HttpPut("status", Order = 0825)]
    public async Task<IActionResult> EditSingleProp([FromQuery] int id, byte? status)
    {
        if (id < 0 && status.HasValue)
        {
            HosClient entity = new() { Id = id, ClientStatus = status.Value };
            return Ok(await Data.HosClients.GenericUpdateSinglePropertyById(id, entity, p => p.ClientStatus));
        }
        else
            return BadRequest();
    }


    // ============================= delete ============================= 


}// end class