namespace ZoheeApi.Entities
{
    public class Documents
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public string FileName {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public bool IsSigned { get; set; } = false;
        public bool IsTemplate { get; set; } = false;
    }
}
