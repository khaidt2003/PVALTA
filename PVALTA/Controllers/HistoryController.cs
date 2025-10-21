using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVALTA.Model.Dto;
using PVALTA.Service;

namespace PVALTA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _service;
        public HistoryController(IHistoryService service) => _service = service;

        [HttpGet("range")]
        public async Task<ActionResult<RankListResponse>> GetByRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int? currentUserId,
            [FromQuery] int? topN,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            var req = new RankingBoardRequest{ 
                StartDate= startDate, 
                EndDate = endDate,
                currentUserId = currentUserId, 
                TopN = topN,
                pageIndex = page, 
                pageSize = pageSize };
            var result = await _service.GetByRangeAsync(req);
            return Ok(result);
        }
    }
}
