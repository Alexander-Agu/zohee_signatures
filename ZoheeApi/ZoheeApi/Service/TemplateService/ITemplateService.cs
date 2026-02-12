using ZoheeApi.Entities;

namespace ZoheeApi.Service.TemplateService
{
    public interface ITemplateService
    {
        public Task<string> CreateTemplateAsync(
        string documentTitle,
        IFormFile file
    );

        public Task<List<Template>> GetTemplatesAsync();
    }
}
