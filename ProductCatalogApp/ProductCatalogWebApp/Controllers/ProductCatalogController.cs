using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogWebApp.Models;

namespace ProductCatalogWebApp.Controllers
{
    public class ProductCatalogController : Controller
    {
        private string baseURL = "http://localhost:5059/";

        public async Task<IActionResult> Index()
        {
            List<Category> listCategory = new List<Category>();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/categories/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync("GetCategories");

                if (getData.IsSuccessStatusCode)
                {
                    string result = await getData.Content.ReadAsStringAsync();
                    listCategory = JsonSerializer.Deserialize<List<Category>>(result);
                }
                else
                {
                    return View("Error page");
                }
            }
            return View(listCategory);
        }
    }
}