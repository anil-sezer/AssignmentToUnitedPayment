using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;
using Serilog;
using UPay.Application.Boilerplate;
using UPay.Application.Services.LoginAsMerchant.Dto;
using UPay.Domain.Consts;

namespace UPay.Application.Services.LoginAsMerchant;

public class LoginAsMerchantAppService : AppServiceBase, ILoginAsMerchantAppService
{
    private readonly HttpClient _httpClient;

    public LoginAsMerchantAppService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MerchantModelFromJwt> LoginAndGetModelAsync() // todo: Store login info at cache, reuse it
    {
        // todo: I should get these secret stuff from Vault
        var requestData = new
        {
            ApiKey = PaymentConsts.ApiKey,
            Email = "murat.karayilan@dotto.com.tr",
            Lang = "TR"
        };
        
        var apiResponse = await HandleRestApiCallAsync<MerchantLoginResponse>(requestData, ThirdPartyApiSettings.Url_MerchantAuthentication);

        if (apiResponse.StatusCode != 200)
        {
            Log.Fatal("{ClassName}.{MethodName}() failed. StatusCode: {StatusCode}, ResponseMessage: {ResponseMessage}", nameof(LoginAsMerchantAppService), nameof(LoginAndGetModelAsync), apiResponse.StatusCode, apiResponse.ResponseMessage);
            throw new HttpRequestException();
        }

        return ExtractMerchantModelFromResponse(apiResponse);
    }

    private static MerchantModelFromJwt ExtractMerchantModelFromResponse(MerchantLoginResponse apiResponse)
    {
        var json = DecodeJwt(apiResponse.Result.Token);
        
        var result =  JsonConvert.DeserializeObject<MerchantModelFromJwt>(json) ?? throw new InvalidOperationException();
        result.Token = apiResponse.Result.Token;
        
        return result;
    }
    private static string DecodeJwt(string jwt)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.ReadJwtToken(jwt);
        var payload = token.Payload.SerializeToJson();
        return payload;
    }

    // todo: Merge all of the code below with ThirdPartyApiCallHandler

    private async Task<TResponse> HandleRestApiCallAsync<TResponse>(object requestData, string callUrl)
    {
        var retries = 0;
        while (retries < ThirdPartyApiSettings.Rule_MaxRetries)
        {
            try
            {
                var result = await RestApiCallAsync(requestData, callUrl);

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

    private async Task<string> RestApiCallAsync(object requestData, string callUrl)
    {

        var requestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(callUrl, requestContent);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseContent))
            throw new HttpRequestException();

        return responseContent;
    }
}
