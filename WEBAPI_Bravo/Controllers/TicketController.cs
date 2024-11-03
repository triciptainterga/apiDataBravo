using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;

namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketControllerController : ControllerBase
    {
        private readonly BravoContext _context;
        private readonly string[] _validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
        private readonly long _maxFileSize = 10 * 1024 * 1024; // 10 MB in bytes


        public TicketControllerController(BravoContext context)
            {
                _context = context;
            }


        [HttpGet("GetDataPriority")]
        public async Task<ActionResult<BRAPriority>> GetDataPriority()
        {
            var BRAPriority = await _context.BRAPrioritys.ToListAsync();

            if (BRAPriority == null)
            {
                return StatusCode(400, NotFound());
            }
            return StatusCode(201, BRAPriority);


        }

            [HttpGet("GetDataCategory")]
            public async Task<ActionResult<MCategory>> GetDataCategory()
            {
                var Category = await _context.MCategories.Where( x=> x.Na == "Y").OrderByDescending(x => x.Name).ToListAsync();

                if (Category == null)
                {
                    return StatusCode(400, NotFound());
                }
                return StatusCode(201, Category);

            }
        [HttpGet("GetDataSubCategory")]
        public async Task<ActionResult<MCategory>> GetDataSubCategory(string categoryId)
        {

           // SELECT* FROM mSubCategoryLv1 WHERE CategoryID = @TrxID AND NA = 'Y' ORDER BY SubName ASC
            var SubCategory = await _context.MSubCategoryLv1s.Where(x => x.Na == "Y" && x.CategoryId == categoryId).OrderByDescending(x => x.SubName).ToListAsync();

            if (SubCategory == null)
            {
                return StatusCode(400, NotFound());
            }
            return StatusCode(201, SubCategory);

        }

        [HttpGet("GetDataKantor")]
        public async Task<ActionResult<BraNamaKantor>> GetDataKantor(string searchText)
        {

            // SELECT* FROM BRA_Nama_Kantor WHERE(NamaKantor LIKE '%' + @TrxID + '%' OR EMAIL LIKE '%' + @TrxID + '%' OR Telepon LIKE '%' + @TrxID + '%') ORDER BY NamaKantor ASC

            var likeSearch = $"%{searchText}%";

            
            var result = await _context.BraNamaKantors
                 .Where(x => EF.Functions.Like(x.NamaKantor, likeSearch) || EF.Functions.Like(x.Email, likeSearch) ||EF.Functions.Like(x.Telepon, likeSearch))
                 .OrderByDescending(x => x.NamaKantor)
                 .ToListAsync();

            if (result == null)
            {
                return StatusCode(400, NotFound());
            }
            return StatusCode(201, result);

        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {

            long size = files.Sum(f => f.Length);

            if (size > _maxFileSize)
            {
                return BadRequest("Total file size exceeds the 10 MB limit.");
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // Validate file type
                    var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant().TrimStart('.');
                    if (!_validFileTypes.Contains(fileExtension))
                    {
                        return BadRequest($"Invalid file type: {fileExtension}. Allowed types are: {string.Join(", ", _validFileTypes)}.");
                    }

                    // Ensure the file size is within the limit
                    if (formFile.Length > _maxFileSize)
                    {
                        return BadRequest("File size exceeds the 10 MB limit.");
                    }

                    // Generate a unique file name
                    var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{new Random().Next(1000000, 9999999)}{Path.GetExtension(formFile.FileName)}";
                    var filePath = Path.Combine(Path.GetTempPath(), uniqueFileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket request)
       
        {
            var listTickets = new List<Ticket>();
            string strTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string strGenesysNumber, strThreadID, strAccount, strChannel;

            if (request.GenesysNumber == "0")
            {
                strThreadID = strTime + new Random().Next(1000000, 9999999);
                strGenesysNumber = strTime + new Random().Next(100000000, 999999999);
                strAccount = request.Account;
                strChannel = request.Channel;
            }
            else
            {
                strGenesysNumber = request.GenesysNumber;
                strThreadID = request.ThreadID;
                strAccount = request.Account;
                strChannel = request.Channel;
            }

            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC BRA_CreateTicket_DK @TicketNumber, @GenesysNumber, @ThreadID, @Account, @Channel, @CustomerID, @UserName, @Priority, @Status, @Subject, @Kategori, @SubKategori, @NoAju, @NilaiTransaksi, @Kantor, @Pertanyaan, @Jawaban, @Posisi,@NamaPerusahaan, @EmailPerusahaan,@TeleponPerusahaan,@NPWPPerusahaan , @Action",
                    new SqlParameter("@TicketNumber", strTime),
                    new SqlParameter("@GenesysNumber", strGenesysNumber),
                    new SqlParameter("@ThreadID", strThreadID),
                    new SqlParameter("@Account", strAccount),
                    new SqlParameter("@Channel", strChannel),
                    new SqlParameter("@CustomerID", request.CustomerID),
                    new SqlParameter("@UserName", request.UserName),
                    new SqlParameter("@Priority", request.Priority),
                    new SqlParameter("@Status", request.Status),
                    new SqlParameter("@Subject", request.Subject),
                    new SqlParameter("@Kategori", request.Kategori),
                    new SqlParameter("@SubKategori", request.SubKategori),
                    new SqlParameter("@NoAju", request.NoAju),
                    new SqlParameter("@NilaiTransaksi", request.NilaiTransaksi),
                    new SqlParameter("@Kantor", request.Kantor),
                    new SqlParameter("@Pertanyaan", Uri.EscapeDataString(request.Pertanyaan)),
                    new SqlParameter("@Jawaban", Uri.EscapeDataString(request.Jawaban)),
                    new SqlParameter("@Posisi", request.Posisi),
                    new SqlParameter("@NamaPerusahaan", request.NamaPerusahaan),
                    new SqlParameter("@EmailPerusahaan", request.EmailPerusahaan),
                    new SqlParameter("@TeleponPerusahaan", request.TeleponPerusahaan),
                    new SqlParameter("@NPWPPerusahaan", request.NPWPPerusahaan),
                    new SqlParameter("@Action", request.Action)
                );

                var ticket = new Ticket
                {
                    TicketNumber = strTime,
                    GenesysNumber = strGenesysNumber,
                    ThreadID = strThreadID,
                    Account = strAccount,
                    Channel = strChannel,
                    CustomerID = request.CustomerID,
                    UserName = request.UserName,
                    Priority = request.Priority,
                    Status = request.Status,
                    Subject = request.Subject,
                    Kategori = request.Kategori,
                    SubKategori = request.SubKategori,
                    NoAju = request.NoAju,
                    NilaiTransaksi = request.NilaiTransaksi,
                    Kantor = request.Kantor,
                    Pertanyaan = Uri.EscapeDataString(request.Pertanyaan),
                    Jawaban = Uri.EscapeDataString(request.Jawaban),
                    Posisi = request.Posisi,
                    Action = request.Action
                };

                listTickets.Add(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());

            }
            

            //var js = new JavaScriptSerializer();
            return StatusCode(201, listTickets);
        }
        public class Ticket
        {


            public string AccountId { get; set; }
            public string ContactId { get; set; }
            public string TicketNumber { get; set; }
            public string GenesysNumber { get; set; }
            public string ThreadID { get; set; }
            public string Account { get; set; }
            public string Channel { get; set; }
            public string CustomerID { get; set; }
            public string UserName { get; set; }
            public string Priority { get; set; }
            public string Status { get; set; }
            public string Subject { get; set; }
            public string Kategori { get; set; }
            public string SubKategori { get; set; }
            public string NoAju { get; set; }
            public string NilaiTransaksi { get; set; }
            public string Kantor { get; set; }
            public string Pertanyaan { get; set; }
            public string Jawaban { get; set; }
            public string Posisi { get; set; }
            public string NamaPerusahaan { get; set; }
            public string EmailPerusahaan { get; set; }
            public string TeleponPerusahaan { get; set; }
            public string NPWPPerusahaan { get; set; }
            public string Action { get; set; }
        }
    }
   

   
}
