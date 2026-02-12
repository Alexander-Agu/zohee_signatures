using Microsoft.EntityFrameworkCore;
using ZoheeApi.Entities;
using ZoheeApi.Repository;

namespace ZoheeApi.Service.TemplateService
{
    public class TemplateService(ZoheeContext context) : ITemplateService
    {
        public async Task<string> CreateTemplateAsync(string documentTitle, IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "templates");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string Clean(string s) =>
                string.Join("_", s.Split(Path.GetInvalidFileNameChars()));


            var fileName = $"{documentTitle}.pdf";

            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var doc = new Template()
            {
                TemplateTitle = documentTitle,
                FileName = fileName,
            };

            context.templates.Add(doc);
            await context.SaveChangesAsync();

            return fileName;
        }

        public async Task<List<Template>> GetTemplatesAsync()
        {
            return await context.templates.ToListAsync();
        }
    }
}
