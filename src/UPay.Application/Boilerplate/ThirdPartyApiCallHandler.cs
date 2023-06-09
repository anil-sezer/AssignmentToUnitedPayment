using System.Text;
using Newtonsoft.Json;
using Serilog;
using UPay.Domain.Consts;

namespace UPay.Application.Boilerplate;

public class ThirdPartyApiCallHandler
{
    private readonly HttpClient _httpClient;

    public ThirdPartyApiCallHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> HandleRestApiCallAsync<TResponse>(object requestData, HttpMethod method, string callUrl, Dictionary<string, string> headers)
    {
        var retries = 0;
        while (retries < ThirdPartyApiSettings.Rule_MaxRetries)
        {
            try
            {
                var result = await RestApiCallAsync(requestData, method, callUrl, headers);

                if (!string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<TResponse>(result, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch (HttpRequestException)
            {
                Log.Information("Error occured while calling the API. Will retry {RetryCount} times", Math.Abs(ThirdPartyApiSettings.Rule_MaxRetries - retries));
            }
            catch (JsonSerializationException e)
            {
                Log.Fatal("Error occured while deserializing the response from the API. App cannot continue. Exception: {@Exception}", e);
                throw;
            }
            
            retries++;
            await Task.Delay(ThirdPartyApiSettings.Rule_DelayBetweenRetriesAsMilliSeconds);
        }

        Log.Fatal("Error occured while calling the API. Max retry count({MaxRetries}) exceeded", ThirdPartyApiSettings.Rule_MaxRetries);
        throw new HttpRequestException();
    }

    private async Task<string> RestApiCallAsync(object requestData,  HttpMethod method, string callUrl, Dictionary<string, string> headers)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        foreach (var header in headers)
        {
            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        HttpResponseMessage response;
        if (method == HttpMethod.Post)
            response = await _httpClient.PostAsync(callUrl, requestContent);
        else
            throw new NotImplementedException("Only POST method is implemented for now.");
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent))
            throw new HttpRequestException();

        return responseContent;
    }
}
