using Microsoft.AspNetCore.Mvc;
using ZoheeApi.Entities;

namespace ZoheeApi.Service
{
    public interface IDocumentService
    {
        public Task<string> SaveDocumentAsync(
                string userName,
                string phoneNumber,
                string email,
                string documentTitle,
                IFormFile file

            );


        public Task<List<Documents>> GetAllDocumentsAsync();

        public Task<Documents> SignDocumentAsync(string filename)
    }
}
