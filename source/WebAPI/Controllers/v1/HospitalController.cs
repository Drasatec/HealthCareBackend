using DomainModel.Models.Hospitals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class HospitalController : ControllerBase
{
    public IUnitOfWork Data { get; }
    private IHospitalRepository HospitalRepo;

    public HospitalController(IUnitOfWork data, IHospitalRepository hospitalRepository)
    {
        HospitalRepo = hospitalRepository;
        Data = data;
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAll(string lang = "ar")
    {
        //var hospitals = await HospitalRepo.GetAllHospitals(lang);

        var hospitals = await Data.Hospitals.ReadAll(x => x.Id == 1, new[] { "HospitalsContactData", "HospitalTranslations" });
        var result = HospitalDto.ToList(hospitals);
        return Ok(result);
    }
}
