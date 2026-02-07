namespace SP_API_MCP.DTOs
{
    public sealed record DocSearchResponse(
        string Query,
        IReadOnlyList<DocSearchResult> Results
    );
}
