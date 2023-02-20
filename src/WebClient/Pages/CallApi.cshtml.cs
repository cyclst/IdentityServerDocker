using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{

    public class CallApiModel : PageModel
    {
        private IConfiguration _configuration;

        public string Json = string.Empty;

        public CallApiModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync($"{_configuration["Api:Uri"]}/identity");

            var parsed = JsonDocument.Parse(content);
            var formatted = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });

            Json = formatted;
        }
    }
}
