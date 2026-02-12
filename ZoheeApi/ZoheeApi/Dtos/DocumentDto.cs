namespace ZoheeApi.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public bool IsSigned { get; set; } = false;
    }
}
