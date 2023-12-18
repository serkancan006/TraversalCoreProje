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


//Postgresql
//select * From crosstab ( 'Select "VisitDate","City","CityVisitCount" From "Visitors" Order By 1,2' ) As ct("VisitDate" TimeStamp,City1 int ,City2 int ,City3 int ,City4 int, City5 int);


//Mssql -> tam cross table değil corss table yapısı mssql de bulunmamaktadır
//SELECT*
//FROM(
//    SELECT VisitDate, City, CityVisitCount,
//           'City' + CAST(ROW_NUMBER() OVER(PARTITION BY VisitDate ORDER BY City) AS VARCHAR(10)) AS CityNumber
//    FROM Visitors
//) AS SourceTable
//PIVOT (
//    MAX(CityVisitCount)
//    FOR CityNumber IN ([City1], [City2], [City3], [City4], [City5])
//) AS PivotTable
//ORDER BY VisitDate;


//Update "Visitors" Set "VisitDate" = '2023-03-16' where "VisitorID" > 148 and "VisitorID" <= 153
//dogru çalışması  için her 5 veride bir DateTimeverilerinini saatlarini ortak formata sokman gerek ya ortak formata sokulur yada saatden bagımsız yapılır örnegin Date , DateTime olmaz

