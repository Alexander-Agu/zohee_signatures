using System;
using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Repository;
using ZoheeApi.Entities;
using ZoheeApi.Service;
using System.Threading.Tasks;

namespace ZoheeApi.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController(IDocumentService documentService) : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] string userName,
            [FromForm] string phoneNumber,
            [FromForm] string documentTitle,
            [FromForm] string email,
            [FromForm] IFormFile file)
        {

            string response = await documentService.SaveDocumentAsync(userName, phoneNumber, email, documentTitle, file); 
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
    }
}
