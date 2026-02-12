using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Dtos;
using ZoheeApi.Entities;

namespace ZoheeApi.Service.DocumentService
{
    public interface IDocumentService
    {
        public Task<string> SaveDocumentAsync(
                string users,
                string documentTitle,
                IFormFile file

            );


        public Task<List<Documents>> GetAllDocumentsAsync();

        public Task<Documents> SignDocumentAsync(string email, int documentId, int userId, string filename, IFormFile file);
    }
}
