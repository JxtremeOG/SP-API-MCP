namespace SP_API_MCP.DTOs
{
    public sealed record GetDocResponse(
        int Id,
        string Path,
        string Title,
        string Content
    );
}
