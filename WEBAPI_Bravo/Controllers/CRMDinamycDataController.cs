using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.ExternalServices;

namespace WEBAPI_Bravo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CRMDinamycDataController : ControllerBase
    {
        private readonly ExternalApiService _externalApiService;

        public CRMDinamycDataController(ExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet("GetDataContactAll")]
        public async Task<IActionResult> GetDataContactAll()
        {
            var contacts = await _externalApiService.GetContactsAsync();

            JsonDocument jsonDoc = JsonDocument.Parse(contacts);
            JsonElement root = jsonDoc.RootElement;
           JsonElement datasElement = root.GetProperty("Datas");           
            return Ok(datasElement);
        }


        //[HttpGet("GetDataContactByTop")]
        //public async Task<IActionResult> GetDataContactByTop(int jumlah)
        //{
        //    var contacts = await _externalApiService.GetContactsTopAsync(jumlah);

        //    JsonDocument jsonDoc = JsonDocument.Parse(contacts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}

        //[HttpGet("GetDataContactByEmail")]
        //public async Task<IActionResult> GetDataContactByTop(string email)
        //{
        //    var contacts = await _externalApiService.GetContactsbyEmail(email);

        //    JsonDocument jsonDoc = JsonDocument.Parse(contacts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}

        //[HttpGet("GetDataContactByEmailAndNotelpn")]
        //public async Task<IActionResult> GetContactsbyNotelpnAndEmail(string email,string notelp)
        //{
        //    var contacts = await _externalApiService.GetContactsbyNotelpnAndEmail(notelp, email);

        //    JsonDocument jsonDoc = JsonDocument.Parse(contacts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}
        //[HttpGet("GetDataContactByNotelpn")]
        //public async Task<IActionResult> GetDataContactByNotelpn(string notelp)
        //{
        //    var contacts = await _externalApiService.GetContactsbyNotelpn(notelp);

        //    JsonDocument jsonDoc = JsonDocument.Parse(contacts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}
        //[HttpGet("GetDataAccountAll")]
        //public async Task<IActionResult> GetDataAccountAll(int year)
        //{
        //    var Accounts = await _externalApiService.GetAccountAsync(year);

        //    JsonDocument jsonDoc = JsonDocument.Parse(Accounts);
        //    JsonElement root = jsonDoc.RootElement;
        //   JsonElement datasElement = root.GetProperty("Datas");  
            

        //    return Ok(datasElement);
        //}


        //[HttpGet("GetDataAccountByTop")]
        //public async Task<IActionResult> GetDataAccountByTop(int jumlah)
        //{
        //    var Accounts = await _externalApiService.GetAccountTopAsync(jumlah);

        //    JsonDocument jsonDoc = JsonDocument.Parse(Accounts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}

        //[HttpGet("GetDataAccountByName")]
        //public async Task<IActionResult> GetDataAccountByTop(string name)
        //{
        //    var Accounts = await _externalApiService.GetAccountbyName(name);

        //    JsonDocument jsonDoc = JsonDocument.Parse(Accounts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}

        //[HttpGet("GetAccountByNamaAndNpwp")]
        //public async Task<IActionResult> GetAccountbyNamaAndNpwp(string name,string npwp)
        //{
        //    var Accounts = await _externalApiService.GetAccountbyNamaAndNpwp(name, npwp);

        //    JsonDocument jsonDoc = JsonDocument.Parse(Accounts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}
        //[HttpGet("GetAccountbynpwp")]
        //public async Task<IActionResult> GetAccountbynpwp(string npwp)
        //{
        //    var Accounts = await _externalApiService.GetAccountbynpwp(npwp);

        //    JsonDocument jsonDoc = JsonDocument.Parse(Accounts);
        //    JsonElement root = jsonDoc.RootElement;

        //    // Extract the "Datas" property
        //    JsonElement datasElement = root.GetProperty("Datas");


           

        //    return Ok(datasElement);
        //}

       //[HttpPost("PostDataContact")]
       // public async Task<IActionResult> PostDataContact(contact Data)
       // {
       //     var Accounts = await _externalApiService.PostContactAsync(Data);

            


 

       //     return Ok(JsonDocument.Parse(Accounts));
       // }

        //[HttpPost("PostDataAccount")]
        //public async Task<IActionResult> PostDataAccount(account Data)
        //{
        //    var Accounts = await _externalApiService.PostAccountAsync(Data);

            




        //    return Ok(JsonDocument.Parse(Accounts));
        //}
        //[HttpPost("PostDataCase")]
        //public async Task<IActionResult> PostDataCase(caseData Data)
        //{
        //    var Accounts = await _externalApiService.PostCaseAsync(Data);

            




        //    return Ok(JsonDocument.Parse(Accounts));
        //}


    }
    //public class DinamicDataController : ControllerBase
    //{
    //    private readonly ExternalApiService _externalApiService;

    //}
    //public DinamicDataController(ExternalApiService externalApiService)
    //{
    //    _externalApiService = externalApiService;
    //}

}
