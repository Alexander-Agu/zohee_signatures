using System;
using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Repository;
using ZoheeApi.Entities;

namespace ZoheeApi.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController(ZoheeContext _context) : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] string userName,
            [FromForm] string phoneNumber,
            [FromForm] string documentTitle,
            [FromForm] IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string Clean(string s) =>
                string.Join("_", s.Split(Path.GetInvalidFileNameChars()));

            var safeName = Clean(userName);
            var safePhone = Clean(phoneNumber);
            var safeTitle = Clean(documentTitle);

            var fileName = $"{safeName}_{safePhone}_{safeTitle}.pdf";

            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var doc = new Documents()
            {
                Name = userName,
                Phone = phoneNumber,
                DocumentTitle = documentTitle,
                FileName = fileName
            };

            _context.documents.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new { fileName }); // only send what frontend needs
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.documents.ToList());
        }

        [HttpGet("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine("uploads", fileName);

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/pdf");
        }

        [HttpGet("documents/{id}")]
        public async Task<IActionResult> ViewPdf(int id)
        {
            var doc = await _context.documents.FindAsync(id);
            if (doc == null)
                return NotFound();

            var uploadsPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "uploads"
            );

            var filePath = Path.Combine(
                uploadsPath,
                doc.FileName   // ✅ THIS is the key fix
            );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File not found: {doc.FileName}");
            }

            return PhysicalFile(
                filePath,
                "application/pdf",
                enableRangeProcessing: true
            );
        }
    }
}
