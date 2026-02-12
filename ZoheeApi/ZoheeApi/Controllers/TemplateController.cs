using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Service.DocumentService;
using ZoheeApi.Service.TemplateService;

namespace ZoheeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController(ITemplateService templateService) : ControllerBase
    {

        [HttpPost("create-template")]
        public async Task<IActionResult> CreateTemplate(
            [FromForm] string templateTitle,
            [FromForm] IFormFile file)
        {

            string response = await templateService.CreateTemplateAsync(templateTitle, file);
            return Ok(new { response });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await templateService.GetTemplatesAsync());
        }


        [HttpGet("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine("templates", fileName);

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/pdf");
        }
    }
}
