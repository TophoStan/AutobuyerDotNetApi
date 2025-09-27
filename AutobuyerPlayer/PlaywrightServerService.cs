using System.Net.Http.Json;
using AutobuyerPlayer.Contracts;
using Environment;

namespace AutobuyerPlayer;

public class PlaywrightServerService :  IPlaywrightServerService
{
    private readonly HttpClient _httpClient;

    public PlaywrightServerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExecutePlaywrightResponse?> Execute(ExecutePlaywrightRequest request)
    {
        var urlBuilder = new UriBuilder(EnvironmentExtensions.GetPlaywrightServer());
        var result = await _httpClient.PostAsJsonAsync(urlBuilder.Uri, request);
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var body = await result.Content.ReadFromJsonAsync<ExecutePlaywrightResponse>();
        return body;
    }
}