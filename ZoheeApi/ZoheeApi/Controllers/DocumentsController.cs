using System;
using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Repository;
using ZoheeApi.Entities;
using System.Threading.Tasks;
using ZoheeApi.Service.DocumentService;
using ZoheeApi.Dtos;

namespace ZoheeApi.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController(IDocumentService documentService) : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] string recipients,
            [FromForm] string documentTitle,
            [FromForm] IFormFile file)
        {

            string response = await documentService.SaveDocumentAsync(recipients, documentTitle, file); 
            return Ok(new { response });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await documentService.GetAllDocumentsAsync());
        }


        [HttpGet("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine("uploads", fileName);

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/pdf");
        }


        [HttpGet("initial-file/{fileName}")]
        public IActionResult GetInitialFile(string fileName)
        {
            var path = Path.Combine("templates", fileName);

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/pdf");
        }


        [HttpPut("sign/{email}/{documentId}/{userId}/{filename}")]
        public async Task<IActionResult> SignDocument(
            int userId,
            string email,
            int documentId,
            string filename,
            [FromForm] IFormFile file) // <--- Add [FromForm] here
        {
            try
            {
                var result = await documentService.SignDocumentAsync(email, documentId, userId, filename, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // This will help you see the REAL error in your browser response
                return StatusCode(500, ex.Message);
            }
        }
    }
}
