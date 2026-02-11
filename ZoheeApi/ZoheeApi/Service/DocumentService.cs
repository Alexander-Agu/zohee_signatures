
using Microsoft.EntityFrameworkCore;
using ZoheeApi.Entities;
using ZoheeApi.Repository;

namespace ZoheeApi.Service
{
    public class DocumentService(ZoheeContext _context) : IDocumentService
    {
        public async Task<string> CreateTemplate(string documentTitle, IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string Clean(string s) =>
                string.Join("_", s.Split(Path.GetInvalidFileNameChars()));


            var fileName = $"{documentTitle}.pdf";

            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var doc = new Documents()
            {
                DocumentTitle = documentTitle,
                FileName = fileName,
                IsTemplate = true
            };

            _context.documents.Add(doc);
            await _context.SaveChangesAsync();

            return fileName;
        }

        public async Task<List<Documents>> GetAllDocumentsAsync()
        {
            return await _context.documents.ToListAsync();
        }

        public async Task<string> SaveDocumentAsync(string userName, string phoneNumber, string email, string documentTitle, IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string Clean(string s) =>
                string.Join("_", s.Split(Path.GetInvalidFileNameChars()));

            var safeName = Clean(userName);
            var safeEmail = Clean(email);
            var safeTitle = Clean(documentTitle);

            var fileName = $"{safeName}_{safeEmail}_{safeTitle}.pdf";

            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var doc = new Documents()
            {
                Name = userName,
                Phone = phoneNumber,
                DocumentTitle = documentTitle,
                FileName = fileName,
                IsTemplate = false
            };

            _context.documents.Add(doc);
            await _context.SaveChangesAsync();

            return fileName;
        }

        public async Task<Documents> SignDocumentAsync(string filename, IFormFile file)
        {
            var doc = await _context.documents
                .FirstOrDefaultAsync(d => d.FileName == filename);

            if (doc == null)
                throw new Exception("Document not found");

            
            var path = Path.Combine("uploads", filename);

            // Overring the file
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

   
            doc.IsSigned = true;

            await _context.SaveChangesAsync();

            return doc;
        }

    }
}
