using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NotesWise.API.Services.Models;

namespace NotesWise.API.Services
{
    public class AiService : IAiService
    {
        readonly HttpClient _httpClient;
        readonly string _openAiKey;
        readonly string _geminiApiKey;
        // readonly JsonSerializerOptions _jsonOptions;
        public AiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _openAiKey = configuration["OpenAi:ApiKey"] ?? "";
            _geminiApiKey = configuration["Gemini:ApiKey"] ?? "";
        }

        //public async Task<string> GenerateSummaryAsync(string content)// versão GEMINI
        //{
        //    var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_geminiApiKey}";
        //
        //    // Monta o request no formato esperado pelo Gemini
        //    var request = new
        //    {
        //        contents = new[]
        //        {
        //            new
        //            {
        //                parts = new[]
        //                {
        //                    new { text = $"Resuma este texto de forma concisa: {content}" }
        //                }
        //            }
        //        }
        //    };
        //
        //    var json = JsonSerializer.Serialize(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //
        //    var httpRequest = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = new Uri(url),
        //        Content = httpContent
        //    };
        //
        //    var response = await _httpClient.SendAsync(httpRequest);
        //
        //    var responseText = await response.Content.ReadAsStringAsync();
        //
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return $"Erro {response.StatusCode}: {responseText}";
        //    }
        //
        //    var geminiAiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseText);
        //    var summary = geminiAiResponse?.candidates.FirstOrDefault()?.content?.parts?.First().text;
        //
        //    return summary ?? "No summary available.";
        //}

        public async Task<string> GenerateSummaryAsync(string content)
        {
            var request = new OpenAiRequest
            {
                input = $"Resuma este texto de forma consica: {content}",
                model = "gpt-4o-mini",
            };
        
            var json = JsonSerializer.Serialize(request);
        
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/responses"),
                Headers =
                {
                    { "Authorization", $"Bearer {_openAiKey}" },
                },
                Content = httpContent
            };
        
            var response = await _httpClient.SendAsync(httpRequest);
        
            var responseText = await response.Content.ReadAsStringAsync();
        
            var openAiResponse = JsonSerializer.Deserialize<OpenAiResponse>(responseText);
        
            var summary = openAiResponse?.output?.FirstOrDefault()?.content?.FirstOrDefault()?.text ?? "No summary available.";
        
            return summary;
        }
    }
}