namespace WebApplication1.Model
{
    public class Incident
    {
        public required string Category { get; set; }
        public required string Description { get; set; }
        public string? Comments { get; set; }
        public string? SupportUser { get; set; }
        public required string Status { get; set; }
    }
}
