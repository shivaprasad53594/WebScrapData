using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using WebScrapData.Data;
using WebScrapData.Models;

namespace WebScrapData.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ApplicationDbContext _db;
        public string url { get; set; }
        public HomeController(ApplicationDbContext db, IHttpClientFactory httpClient)
        {
            _db = db;
            _httpClient = httpClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPhoneData()
        {
            List<Mobile> Books = _db.Mobiles.ToList();
            return View(Books);
        }

        [HttpPost]
        public async Task<IActionResult> PhoneDetails(IFormCollection form)
        {
            
            try
            {
                string apiUrl = "https://localhost:7230/api/SmartPhonesData?url=" + form["URL"];
                var client = _httpClient.CreateClient();
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    List<Mobile>? books = JsonConvert.DeserializeObject<List<Mobile>>(data);

                    if (books != null)
                    {
                        foreach (var item in books)
                        {
                            var bookList = new Mobile()
                            {
                                Id = item.Id,
                                MobileName = item.MobileName,
                                Price = item.Price,
                            };
                            _db.Mobiles.Add(bookList);
                        }
                        _db.SaveChanges();
                    }
                    return RedirectToAction("GetPhoneData");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve data from the Web API.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        public IActionResult Reliance()
        {
            return View();
        }

        public IActionResult GetRelianceData()
        {
            List<Reliance> Books = _db.Reliance.ToList();
            return View(Books);
        }

        [HttpPost]
        public async Task<IActionResult> RelianceDetails(IFormCollection form)
        {
            try
            {
                string apiUrl = "https://localhost:7230/api/Reliance?url=" + form["URL"];
                var client = _httpClient.CreateClient();
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    List<Reliance>? books = JsonConvert.DeserializeObject<List<Reliance>>(data);

                    if (books != null)
                    {
                        foreach (var item in books)
                        {
                            var bookList = new Reliance()
                            {
                                Id = item.Id,
                                MobileName = item.MobileName,
                                Price = item.Price,
                            };
                            _db.Reliance.Add(bookList);
                        }
                        _db.SaveChanges();
                    }
                    return RedirectToAction("GetRelianceData");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve data from the Web API.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        public IActionResult Shopclues()
        {
            return View();
        }

        public IActionResult GetShopcluesData()
        {
            List<Shopclues> Books = _db.Shopclues.ToList();
            return View(Books);
        }

        [HttpPost]
        public async Task<IActionResult> ShopcluesDetails(IFormCollection form)
        {
            try
            {
                string apiUrl = "https://localhost:7230/api/Shopclues?url=" + form["URL"];
                var client = _httpClient.CreateClient();
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    List<Shopclues>? books = JsonConvert.DeserializeObject<List<Shopclues>>(data);

                    if (books != null)
                    {
                        foreach (var item in books)
                        {
                            var bookList = new Shopclues()
                            {
                                Id = item.Id,
                                MobileName = item.MobileName,
                                Price = item.Price,
                            };
                            _db.Shopclues.Add(bookList);
                        }
                        _db.SaveChanges();
                    }
                    return RedirectToAction("GetShopcluesData");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve data from the Web API.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
