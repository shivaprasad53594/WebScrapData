using Azure.Messaging;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using WebScrapData.Data;
using WebScrapData.Models;
using static System.Net.WebRequestMethods;

namespace WebScrapData.Controllers
{
    public class WebScrabController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ApplicationDbContext _db;
        public string url { get; set; }
        public WebScrabController(ApplicationDbContext db, IHttpClientFactory httpClient)
        {
            _db = db;
            _httpClient = httpClient;

        }
        public IActionResult Index()
        {
            //List<Book> Books = _db.Books.ToList();
            //return View(Books);
            return View();
        }
        public IActionResult GetData()
        {
            List<Book> Books = _db.Books.ToList();
            return View(Books);
        }

        [HttpPost]
        public async Task<IActionResult> ScrapeData(IFormCollection form)
        {
            Book bookList = new Book();

            try
            {
                string apiUrl = "https://localhost:7230/api/DataScrap?url=" + form["URL"];
                var client = _httpClient.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    List<Book>? books = JsonConvert.DeserializeObject<List<Book>>(data);

                    if (books != null)
                    {
                        foreach (var item in books)
                        {
                            bookList = new Book()
                            {
                                Id = item.Id,
                                Title = item.Title,
                                Price = item.Price,
                            };
                            _db.Books.Add(bookList);
                        }
                        _db.SaveChanges();
                    }
                    return RedirectToAction("GetData");
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

        static void exportToCSV(List<Book> books)
        {
            using (var writer = new StreamWriter("books.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(books);
            }
        }
    }
}
