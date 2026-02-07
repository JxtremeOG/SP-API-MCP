using ModelContextProtocol.Server;
using SP_API_MCP.DTOs;
using SP_API_MCP.Repositories;
using System.ComponentModel;

namespace SP_API_MCP.Tools
{
    [McpServerToolType]
    public sealed class DocsTools
    {
        private readonly DocsRepository _docsRepository;

        public DocsTools(DocsRepository docsRepository)
        {
            _docsRepository = docsRepository;
        }

        [McpServerTool]
        [Description("Search the Substance Painter Python API docs with fast keyword search. Returns the best matches with snippets.")]
        public async Task<DocSearchResponse> DocsSearch(
            [Description("The query to search for.")] string query, 
            [Description("The number of results to return.")] int limit)
        {
            var docs = await _docsRepository.SearchWithQuery(query, limit);

            var results = docs.Select(d => new DocSearchResult(
                d.Id,
                d.Path,
                d.Title,
                d.Content
            )).ToList();

            return new DocSearchResponse(query, results);
        }

        [McpServerTool]
        [Description("Get a specific document by its ID. Returns the full document content.")]
        public async Task<GetDocResponse> GetDocById([Description("The ID of the document to get.")] int id)
        {
            var doc = await _docsRepository.GetDocById(id);

            if (doc is null)
                throw new KeyNotFoundException($"Document with ID {id} was not found.");

            return new GetDocResponse(doc.Id, doc.Path, doc.Title, doc.Content);
        }
    }
}
