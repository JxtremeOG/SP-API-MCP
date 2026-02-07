using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SP_API_MCP.Repositories;
using SP_API_MCP.Repository;
using SP_API_MCP.Tools;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddNLog();

var spIndexPath = Environment.GetEnvironmentVariable("SP_API_INDEX_PATH");

if (string.IsNullOrWhiteSpace(spIndexPath))
{
    throw new InvalidOperationException(
        "The SP_API_INDEX_PATH environment variable is not set. " +
        "It must point to the sp_api_index.sqlite database file.");
}

builder.Services.AddDbContext<DocsDBContext>(options =>
    options.UseSqlite($"Data Source={spIndexPath}"));

builder.Services.AddScoped<DocsRepository>();

builder.Services
    .AddMcpServer()
    .WithToolsFromAssembly(typeof(DocsTools).Assembly)
    .WithStdioServerTransport();

await builder.Build().RunAsync();