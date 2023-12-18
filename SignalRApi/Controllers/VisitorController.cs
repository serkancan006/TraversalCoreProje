using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.DAL;
using SignalRApi.Model;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorService _visitorService;

        public VisitorController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        [HttpGet]
        public IActionResult CreateVisitor()
        {
            Random random = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newVisitor = new Visitor()
                    {
                        City = item,
                        CityVisitCount = random.Next(100, 2000),
                        VisitDate = DateTime.Now.AddDays(x)
                    };
                    _visitorService.SaveVisitor(newVisitor).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });
            return Ok("Ziyaretçiler Başarılı Şekilde EKlendi.");
        }
    }
}


//mssql
//Create Procedure SignalRPivot
//as
//select tarih,[1],[2],[3],[4],[5] from (select [city],CityVisitCount,Cast([VisitDate] as Date) as tarih from Visitors) as visitTable Pivot (Sum(CityVisitCount) for City in([1],[2],[3],[4],[5])) as pivottable order by tarih asc