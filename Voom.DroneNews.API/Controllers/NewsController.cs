using Microsoft.AspNetCore.Mvc;
using Voom.DroneNews.API.Models.Dto;
using Voom.DroneNews.API.Services.Interfaces;

namespace Voom.DroneNews.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService; 
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService; 
        }

        [HttpGet]
        public IActionResult Search([FromQuery] SearchNewsRequestDto request)
        {
            try
            {
                var result = _newsService.SearchNews(request.Query, request.Skip, request.Take);

                if(result == null || result.Count == 0) 
                {
                    return NotFound();  
                }

                return Ok(new SearchNewsResponseDto() { Articles = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}