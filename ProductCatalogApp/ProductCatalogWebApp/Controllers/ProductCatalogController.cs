using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogWebApp.Models;

namespace ProductCatalogWebApp.Controllers
{
    public class ProductCatalogController : Controller
    {
        private string baseURL = "http://localhost:5059/"; public async Task<IActionResult> Index()
        {
            List<Category> listCategory = new List<Category>();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); HttpResponseMessage getData = await _httpClient.GetAsync("api/categories/");
                if (getData.IsSuccessStatusCode)
                {
                    string result = await getData.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var deserializedData = JsonSerializer.Deserialize<List<Category>>(result, options);
                    if (deserializedData != null)
                    {
                        listCategory = deserializedData;
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = $"Failed to fetch data. Status: {getData.StatusCode}";
                    return View("Error");
                }
            }
            return View(listCategory);
        }
    }
}