using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WEBAPI_Bravo.Controllers
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AvayaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AvayaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // POST /api/avaya/login
        [HttpPost("cms-login")]
        public async Task<IActionResult> PostCmsLogin([FromBody] LoginRequest login)
        {
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            using var httpClient = new HttpClient(handler);
            var url = "https://10.216.206.40:8443/CMSWeb/rest/login/login";

            var payload = new
            {
                userName = login.userName,
                password = login.password
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var response = await httpClient.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, responseBody);
                }

                return Ok(JsonConvert.DeserializeObject<object>(responseBody));
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }
    }
}
