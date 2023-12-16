using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TraversalCoreProje.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    public class BookingHotelSearchController : Controller
    {
        [Area("Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/hotels/search?units=metric&dest_id=-553173&dest_type=city&room_number=1&checkin_date=2024-05-19&order_by=popularity&locale=en-gb&adults_number=2&checkout_date=2024-05-20&filter_by_currency=AED&page_number=0&children_number=2&children_ages=5%2C0&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&include_adjacency=true"),
                Headers =
                {
                    { "X-RapidAPI-Key", "82362fa074msh9b6232aa5077b7bp167c8ajsnad639a2b7584" },
                    { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var bodyReplace = body.Replace(".", "");
                var values = JsonConvert.DeserializeObject<BookingHotelSearchViewModel>(bodyReplace);
                return View(values.result);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetCityDestID()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCityDestID(string p)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={p}&locale=en-gb"),
                Headers =
                {
                    { "X-RapidAPI-Key", "82362fa074msh9b6232aa5077b7bp167c8ajsnad639a2b7584" },
                    { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                return View();
            }
        }




    }
}
