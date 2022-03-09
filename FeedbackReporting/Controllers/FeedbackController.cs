using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FeedbackReporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackService feedbackService)
        {
            _logger = logger;
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] FeedbackRessource ressource)
        {
            var result = await _feedbackService.Insert(ressource);
            return Ok(result);
        }

        [HttpGet]
        [Route("{reportId}")]
        public async Task<IActionResult> Get(int reportId)
        {
            var result = await _feedbackService.GetById(reportId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
