namespace WebAPI.Controllers.version1;

[Route("api/dashboard/statistics")]
[ApiController]
[ApiVersion("1.0")]
public class MainPageControlPanelController : ControllerBase
{
    private readonly IStatisticsRepository dashboardStatistics;

    public MainPageControlPanelController(IStatisticsRepository dashboardStatistics)
    {
        this.dashboardStatistics = dashboardStatistics;
    }

    [HttpGet("CSDP-count")]
    public async Task<IActionResult> Get_CSDP_Counts()
    {
        var res = await dashboardStatistics.FetchCSDPCounts();
        return Ok(res);
    }
    
    [HttpGet("booking-statistics")]
    public async Task<IActionResult> GetBookingStatistics([FromQuery] int year)
    {
        var res = await dashboardStatistics.BookingStatistics(year);
        return Ok(res);
    }

}
