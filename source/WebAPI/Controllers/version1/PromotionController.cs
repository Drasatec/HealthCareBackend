﻿using DataAccess;
using DomainModel.Contracts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Models;
using System.Linq.Expressions;

namespace WebAPI.Controllers.version1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PromotionController : ControllerBase
{
    private readonly IUnitOfWork Data;

    public PromotionController(IUnitOfWork data)
    {
        Data = data;
    }

    // ============================= post ============================= 


    [HttpPost("add", Order = 0801)]
    public async Task<IActionResult> AddSingle([FromForm] IFormFile? file, [FromForm] Promotion model)
    {
        if (file is not null)
        {
            var name = await DataAccessImageService.SaveSingleImage(file.OpenReadStream());
            model.Photo = name;
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


    [HttpGet(Order = 0801)]
    public async Task<IActionResult> GetById([FromQuery] int id, string? lang)
    {
        if (id < 1) return BadRequest(new Error("400", "can not assign 0"));

        Expression<Func<Promotion, object>>? filterExpression;
        if (lang != null)
        {
            filterExpression = inc => inc.PromotionsTranslations.Where(l => l.LangCode == lang);
        }
        else
            filterExpression = inc => inc.PromotionsTranslations;

        var result = await Data.Generic.GenericReadById(i => i.Id == id, filterExpression);

        return Ok(result);
    }


    [HttpGet("names", Order = 0811)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, int? page, int? pageSize)
    {

        var result = await Data.Generic.GenericReadAll<PromotionsTranslation>(t => t.LangCode.Equals(lang), null, page, pageSize);
        return Ok(result);
    }


    [HttpGet("all", Order = 0812)]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize, int? page, string? lang)
    {

        var result = await Data.Generic.GenericReadAllWihInclude<Promotion>(null, o => o.Id, inc => inc.PromotionsTranslations.Where(l => l.LangCode == lang), page, pageSize);

        if (result == null)
        {
            return Ok(new Response(true, "no content"));
        }


        return Ok(result);
    }


    //[HttpGet("search", Order = 0814)]
    //public async Task<IActionResult> Search([FromQuery] string? searchTerm, string? name, int? page, int? pageSize, string? lang)
    //{
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        return Ok(await Data.Generic.GenericSearchByText<PromotionsTranslation>(t => t.Title.Contains(name), null, page, pageSize));
    //    }
    //    else if (!string.IsNullOrEmpty(searchTerm) && lang != null)
    //    {
    //        Expression<Func<Promotion, bool>> filter = f => f.PromotionsTranslations.Any(t => t.Title.Contains(searchTerm));
    //        Expression<Func<Promotion, object>> include = i => i.PromotionsTranslations.Where(l => l.LangCode == lang);

    //        return Ok(await Data.Generic.GenericSearchByText(filter, include, page, pageSize));
    //    }
    //    return BadRequest(new Error("400", "name or searchTerm with lang is required"));
    //}


    // ============================= put ============================= 


    [HttpPut("edit", Order = 0820)]
    public async Task<IActionResult> UpdateSingleWithImage([FromForm] IFormFile? file, [FromForm] Promotion model)
    {
        Response response;

        if (file is not null)
        {
            var current = await Data.Generic.GenericReadById<Promotion>(i => i.Id == model.Id);
            if (current != null)
            {
                // if photo in database is null
                if (string.IsNullOrEmpty(current.Photo))
                {
                    model.Photo = await DataAccessImageService.SaveSingleImage(file.OpenReadStream());
                    response = await Data.Generic.GenericUpdate(model, null);
                }
                else
                {
                    _ = DataAccessImageService.UpdateSingleImage(file.OpenReadStream(), current.Photo);
                    response = await Data.Generic.GenericUpdate(model, p => p.Photo!);
                }
            }
            else
                response = new Response(false,"not found");

            var name = await DataAccessImageService.SaveSingleImage(file.OpenReadStream());
            model.Photo = name;
        }
        else
            response = await Data.Generic.GenericUpdate(model, p => p.Photo!);



        if (!response.Success)
            return BadRequest(response);

        return Created("drasat", response);
    }

    [HttpPut("edit-translations", Order = 0822)]
    public async Task<IActionResult> Add_EditTranslations([FromForm] List<PromotionsTranslation> translations)
    {
        Response response;

        response = await Data.Generic.GenericUpdate(translations);

        if (response.Success)
            return Created("fawzy", response);

        return BadRequest(response);
    }

    // ============================= delete ============================= 

    [HttpDelete("delete-translat", Order = 0830)]
    public async Task<IActionResult> DeleteTraslate([FromQuery] int? parentId, [FromQuery] params int[] translteId)
    {
        Expression<Func<PromotionsTranslation, bool>> expression;

        if (parentId.HasValue)
            expression = t => t.PromotionId.Equals(parentId);
        else
            expression = t => translteId.Contains(t.Id);

        var res = await Data.Generic.GenericDelete(expression, translteId);

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

    [HttpDelete("delete", Order = 0830)]
    public async Task<IActionResult> Delete([FromQuery] short id)
    {
        var res = await Data.Generic.GenericDelete<Promotion>(t => t.Id.Equals(id));
        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }

}// end class
