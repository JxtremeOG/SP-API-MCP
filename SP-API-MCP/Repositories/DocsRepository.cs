using Microsoft.EntityFrameworkCore;
using SP_API_MCP.DTOs;
using SP_API_MCP.Repository;

namespace SP_API_MCP.Repositories
{
    public class DocsRepository
    {
        private readonly DocsDBContext _dbContext;

        public DocsRepository(DocsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Doc>> SearchWithQuery(string query, int limit)
        {
            var results = await _dbContext.Docs
                .FromSqlInterpolated($@"
                    SELECT d.Id, d.Path, d.Title,
                           snippet(docs_fts, 1, '', '', '...', 32) AS Content
                    FROM docs_fts
                    JOIN docs d ON d.Id = docs_fts.rowid
                    WHERE docs_fts MATCH {query}
                    ORDER BY bm25(docs_fts)
                    LIMIT {limit}")
                .AsNoTracking()
                .ToListAsync();

            return results;
        }

        public async Task<Doc?> GetDocById(int id)
        {
            var doc = await _dbContext.Docs.FindAsync(id);
            return doc;
        }
    }
}
