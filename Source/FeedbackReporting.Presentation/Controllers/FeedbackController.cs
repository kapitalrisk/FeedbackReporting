using FeedbackReporting.Domain.Constants;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.UseCases;
using FeedbackReporting.Presentation.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace FeedbackReporting.Presentation.Controllers
{
    [ApiController]
    [Route("feedback")]
    public class FeedbackController : ControllerBase
    {
        private int _maxFileSize = 10000; // to put in configuration
        private readonly ILogger<FeedbackController> _logger;
        private readonly ICreateFeedbackUseCase _createFeedbackUseCase;
        private readonly IGetFeedbackByIdUseCase _getFeedbackByIdUseCase;
        private readonly IAttachDocumentToFeedbackUseCase _attachDocumentToFeedbackUseCase;
        private readonly IGetFeedbackAttachmentsZipUseCase _getFeedbackAttachmentsUseCase;

        public FeedbackController(ILogger<FeedbackController> logger, 
            ICreateFeedbackUseCase createFeedbackUseCase, 
            IGetFeedbackByIdUseCase getFeedbackByIdUseCase, 
            IAttachDocumentToFeedbackUseCase attachDocumentToFeedbackUseCase,
            IGetFeedbackAttachmentsZipUseCase getFeedbackAttachmentsUseCase)
        {
            _logger = logger;
            _createFeedbackUseCase = createFeedbackUseCase;
            _getFeedbackByIdUseCase = getFeedbackByIdUseCase;
            _attachDocumentToFeedbackUseCase = attachDocumentToFeedbackUseCase;
            _getFeedbackAttachmentsUseCase = getFeedbackAttachmentsUseCase;
        }

        [HttpPost]
        [Route("create")]
        [AuthorizedRoles(UserRoles.Admin, UserRoles.User)]
        public async Task<IActionResult> Create([FromBody] FeedbackRessource ressource)
        {
            var result = await _createFeedbackUseCase.ExecuteAsync(ressource);

            if (result == Database.InsertionErrorReturnValue)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(UserRoles.Admin)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _getFeedbackByIdUseCase.ExecuteAsync(id);

            if (result == null)
                return NotFound(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("attachement/{feedbackId}")]
        [AuthorizedRoles(UserRoles.Admin, UserRoles.User)]
        public async Task<IActionResult> AttachDocument(int feedbackId, IFormFile attachment)
        {
            if (attachment.Length > _maxFileSize)
                return BadRequest($"Cannot attach files larger than {_maxFileSize} bytes");

            var memoryStream = new MemoryStream();
            await attachment.CopyToAsync(memoryStream);
            var result = await _attachDocumentToFeedbackUseCase.ExecuteAsync(new FeedbackAttachmentRessource { FeedbackId = feedbackId, Data = memoryStream.ToArray() });

            return Ok(result);
        }

        [HttpGet]
        [Route("attachement/{feedbackId}")]
        [AuthorizedRoles(UserRoles.Admin)]
        public async Task<IActionResult> GetFeedbackAttachmentZip(int feedbackId)
        {
            var result = await _getFeedbackAttachmentsUseCase.ExecuteAsync(feedbackId);

            return File(result.ToArray(), "application/zip", $"attachments_{feedbackId}.zip");
        }
    }
}
