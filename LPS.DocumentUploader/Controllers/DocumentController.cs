using AutoMapper;
using LPS.DocumentUploader.Application.Services.Documents;
using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LPS.DocumentUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentAppService _documentAppService;
        private IMapper? _mapper;

        public DocumentController(IDocumentAppService documentAppService, IMapper mapper)
        {
            _documentAppService = documentAppService;
            _mapper = mapper;
        }

        [HttpPost("UploadFile")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile([FromForm] DocumentViewModel model, string userEmail)
        {
            try
            {
                var documentDto = _mapper.Map<DocumentDto>(model);

                string filePath = await _documentAppService.UploadFile(documentDto, userEmail);
                return Ok($"File uploaded successfully. FilePath: {filePath}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
