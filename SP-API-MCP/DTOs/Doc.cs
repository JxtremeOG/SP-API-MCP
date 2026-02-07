namespace SP_API_MCP.DTOs
{
    public class Doc
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
