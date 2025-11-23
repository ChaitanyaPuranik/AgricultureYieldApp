namespace Frontend.Service;

using Frontend.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class YieldPredictionService
{
    private readonly HttpClient _http;

    public YieldPredictionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<double> PredictYieldAsync(YieldInput input)
    {
        string json = JsonSerializer.Serialize(input);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("http://localhost:8000/predict", content);
        response.EnsureSuccessStatusCode();

        string result = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(result);
        return doc.RootElement.GetProperty("predicted_yield").GetDouble();
    }
}

