using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.ExternalServices;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;


namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCHController : ControllerBase
    {
        private readonly BravoContext _context;
        private readonly IConfiguration _configuration;
        private readonly ISCHService _SCHService;

        public SCHController(BravoContext context, IConfiguration configuration, SCHService  SCHService)
        {
            _context = context;
            _configuration = configuration;
            _SCHService = SCHService;
        }

       

        

      
    }




}



public class DataModel
{
    public string Date { get; set; }

    [JsonPropertyName("Time Zone")]
    public string TimeZone { get; set; }
    [JsonPropertyName("Split/Skill")]
    public string SplitSkill { get; set; }
    public List<string> Totals { get; set; }
    public List<Interval> Intervals { get; set; }
}

public class Interval
{
    [JsonPropertyName("Time Start")]
    public string TimeStart { get; set; }
   
    public string Separator { get; set; }
    [JsonPropertyName("Time End")]
    public string TimeEnd { get; set; }
    [JsonPropertyName("Target %")]
    public string TargetPercentage { get; set; }
    [JsonPropertyName("Target Seconds")]
    public string TargetSeconds { get; set; }
    [JsonPropertyName("% Within Service Level")]
    public string PercentWithinServiceLevel { get; set; }
    public string Arrivals { get; set; }
    [JsonPropertyName("Aban Calls")]
    public string AbanCalls { get; set; }
    [JsonPropertyName("Flow Out")]
    public string FlowOut { get; set; }

    [JsonPropertyName("Avg Aban Time")]
    public string AvgAbanTime { get; set; }

    [JsonPropertyName("Avg Speed Ans")]
    public string AvgSpeedAns { get; set; }
    [JsonPropertyName("Max Delay")]
    public string MaxDelay { get; set; }
    [JsonPropertyName("Avg Hold Time")]
    public string AvgHoldTime { get; set; }
    [JsonPropertyName("Interrupt Deliveries")]
    public string InterruptDeliveries { get; set; }
    public string Acceptable { get; set; }
}

