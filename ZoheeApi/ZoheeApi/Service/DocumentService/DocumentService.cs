
using System.Reflection.Metadata;
using System.Text.Json;
using FIN.Service.EmailServices;
using Microsoft.EntityFrameworkCore;
using ZoheeApi.Dtos;
using ZoheeApi.Entities;
using ZoheeApi.Repository;
using ZoheeApi.Service.UserService;                                                                 

namespace ZoheeApi.Service.DocumentService
{
    public class DocumentService(ZoheeContext _context, IUserService userService, IEmailService emailService) : IDocumentService
    {
        public async Task<List<Documents>> GetAllDocumentsAsync()
        {
            return await _context.documents.ToListAsync();
        }

        public async Task<string> SaveDocumentAsync(string users, string documentTitle, IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

            string Clean(string s) => string.Join("_", s.Split(Path.GetInvalidFileNameChars()));
            var usersM = JsonSerializer.Deserialize<List<CreateUser>>(users);

            if (usersM == null || usersM.Count == 0) throw new Exception("No users provided");

            var safeTitle = Clean(documentTitle);
            string fileName = usersM.Count == 1
                ? $"{Clean(usersM[0].Name)}_{Clean(usersM[0].Email)}_{safeTitle}"
                : $"{safeTitle}";

            var fullPath = Path.Combine(uploadsPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 1. Create the Document and attach Users to it (The "Graph")
            var doc = new Documents
            {
                DocumentTitle = documentTitle,
                FileName = fileName,
                IsSigned = false,
                Users = new List<User>() // Initialize the list
            };

            foreach (var u in usersM)
            {
                doc.Users.Add(new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone
                });
            }

            // Save everything in ONE go
            _context.documents.Add(doc);
            await _context.SaveChangesAsync();

            foreach (var savedUser in doc.Users)
            {
                await emailService.SendSignatureRequestEmailAsync(
                    savedUser.Email,
                    doc.FileName,
                    doc.Id,
                    savedUser.Id
                );
            }

            return fileName;
        }



        public async Task<Documents> SignDocumentAsync(string email, int documentId, int userId, string filename, IFormFile file)
        {
            // 1. Find the document and the specific user
            var doc = await _context.documents
                .FirstOrDefaultAsync(d => d.Id == documentId);

            var user = await _context.users.FindAsync(userId);

            if (user == null) return doc;

            if (doc == null || user == null)
                throw new Exception("Document or Signer not found");

            // 2. Overwrite the file with the signed version
            var path = Path.Combine("uploads", filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 3. Mark THIS user as having signed
            user.HasSigned = true;

            // We save here so the 'Any' check below sees the updated user
            await _context.SaveChangesAsync();

            // 4. Update Document Status
            // The document is fully signed ONLY IF there are NO users left who haven't signed
            bool anyPendingSigners = await _context.users
                .AnyAsync(x => x.DocumentId == documentId && !x.HasSigned);

            doc.IsSigned = !anyPendingSigners;

            await _context.SaveChangesAsync();

            await emailService.SendDocumentSignedNotificationAsync("theonlyagu@gmail.com", user.Name, filename);

            return doc;
        }

    }
}
