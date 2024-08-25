using FluentResults;
using Markdig;
using MELWebsite.Models;
using System.Net.Http.Json;

namespace MELWebsite.Services
{
    public interface IMDFilesServices
    {
        Task<Result<MDIndex>> GetIndex();
        Task<Result<string>> GetHTMLContent(string file);
    }

    public class MDFilesServices(
            HttpClient httpClient
            ) : IMDFilesServices
    {
        private readonly HttpClient _httpClient = httpClient;

        private const string MdBase = "md/";
        private const string MdFakeExt = ".json";
        private const string IndexFile = "md/index.json";

        public async Task<Result<MDIndex>> GetIndex()
        {
            var result = await _httpClient.GetFromJsonAsync<MDIndex>(IndexFile);
            return Result.Ok(result ?? new());
        }

        public async Task<Result<string>> GetHTMLContent(string file)
        {
            var mdContent = await _httpClient.GetStringAsync($"{MdBase}{file}");
            var result = Markdown.ToHtml(mdContent ?? string.Empty);
            return Result.Ok(result);
        }
    }
}
