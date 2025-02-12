using System.Net.Http;

namespace Advent_of_Code.Http;

public static class AocClient
{
    public static Task<HttpResponseMessage> GetAsync(Uri requestUri, params KeyValuePair<string, string>[] cookies)
    {
        var handler = new HttpClientHandler { UseCookies = false };
        var client = new HttpClient(handler);
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        
        if (cookies.Length != 0)
        {
            request.Headers.Add("Cookie", string.Join(";", cookies.Select(kvp => $"{kvp.Key}={kvp.Value}")));
        }
        return client.SendAsync(request);
    }
}
