namespace SP_API_MCP.DTOs
{
    public sealed record DocSearchResult(
        int Id,
        string Path,
        string Title,
        string Snippet
    );
}
