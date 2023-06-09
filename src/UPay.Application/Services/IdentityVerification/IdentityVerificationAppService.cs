using System.Text;
using Serilog;
using UPay.Application.Boilerplate;
using UPay.Domain.Consts;

namespace UPay.Application.Services.IdentityVerification;

public class IdentityVerificationAppService : AppServiceBase, IIdentityVerificationAppService
{
    private readonly HttpClient _httpClient;

    public IdentityVerificationAppService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/soap+xml");
    }
    
    private const string Tckn = "tckn";
    private const string Name = "Name";
    private const string Surname = "Surname";
    private const string BirthYear = "BirthYear";
    private const string RequestBase = @$"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
              <soap12:Body>
                <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                  <TCKimlikNo>{Tckn}</TCKimlikNo>
                  <Ad>{Name}</Ad>
                  <Soyad>{Surname}</Soyad>
                  <DogumYili>{BirthYear}</DogumYili>
                </TCKimlikNoDogrula>
              </soap12:Body>
            </soap12:Envelope>";
    private const string NegativeResult = "<TCKimlikNoDogrulaResult>false</TCKimlikNoDogrulaResult>";
    private const string PositiveResult = "<TCKimlikNoDogrulaResult>true</TCKimlikNoDogrulaResult>";

    // todo: Bunu thirdPartyApiCallHandler'a taşı, oradan çağır
    public async Task<bool> ValidateTcknAsync(string name, string surname, DateTime birthday, long tckn)
    {
        var request = RequestBase
            .Replace(Name, name)
            .Replace(Surname, surname)
            .Replace(Tckn, tckn.ToString())
            .Replace(BirthYear, birthday.Year.ToString());

        var httpContent = new StringContent(request, Encoding.UTF8, "application/soap+xml");
        var response = await _httpClient.PostAsync(ThirdPartyApiSettings.Url_TcknVerification, httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        Log.Information("TCKN validation service response: {Response}", responseContent);

        if (responseContent.Contains(PositiveResult))
            return true;

        if (responseContent.Contains(NegativeResult))
            return false;
        
        // todo: Exception middleware'i ekle, Log'u onun içinden yap, duplike mesaj oluyor böyle
        Log.Warning("Unexpected response from TCKN validation service. Response: {Response}", responseContent);
        throw new Exception("Unexpected response from TCKN validation service.");
    }
}
