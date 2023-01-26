using Microsoft.AspNetCore.Mvc;
using Web3Demo.Models.Services;
using Web3Demo.Models.UIContracts;

namespace Web3Demo.Controllers
{
    [ApiController]
    [Route("api/v1/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly DocumentsService _documentsService;

        public DocumentsController(ILogger<DocumentsController> logger, DocumentsService documentsService)
        {
            _logger = logger;
            _documentsService = documentsService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessFileByUrl([FromBody] ProcessFileByUrlRequestModel processFileByUrlRequestModel)
        { 
            var result = await _documentsService.ProcessFileByUrl(processFileByUrlRequestModel.Token, processFileByUrlRequestModel);
            return Ok(result);
        } 
    } 
}