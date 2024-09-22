using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;
using static WEBAPI_Bravo.Controllers.TicketControllerController;

namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoipController : ControllerBase
    {
        private readonly BravoContext _context;

        public VoipController(BravoContext context)
        {
            _context = context;
        }

        // GET: api/MCustomers
        [HttpGet("GetMCustomersByHPOrEmailVoip")]
        public async Task<ActionResult<IEnumerable<DataCustomerVoip>>> GetMCustomersByHPOrEmail(string searchInput)
        {
            try
            {
                var customers = await _context.MCustomers
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{searchInput}%") ||
                         EF.Functions.Like(x.Email.ToLower(), $"%{searchInput}%") ||
                         EF.Functions.Like(x.Telepon.ToLower(), $"%{searchInput}%"))

                 .Select(c => new DataCustomerVoip
                 {
                     CustomerId = c.CustomerId,
                     Name = c.Name,
                     Phone = c.Hp,
                     Email = c.Email,
                     NamePerusahaan = c.NamaPerusahaan,
                     Facebook = c.Facebook,
                     Instagram = c.Instagram,
                     Twitter = c.Twitter,
                     ContactId = c.ContactDynamicId,
                 })
                    .ToListAsync();
                return Ok(customers); // Return 200 OK with the list of customers
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // Example: _logger.LogError(ex, "An error occurred while fetching customers.");

                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }
        }


        //[HttpGet("GetDataBraNamaPerusahaanVoip")]
        //public async Task<ActionResult<IEnumerable<BraNamaPerusahaan>>> GetDataBraNamaPerusahaan(string InputData)
        //{
        //    try
        //    {
        //        var customers = await _context.BraNamaPerusahaans
        //      .Where(x => x.Nama_Perusahaan.Contains(InputData) || x.NomorTelepon.Contains(InputData) )
        //      .ToListAsync();
        //        return Ok(customers);
        //    }
        //    catch (Exception ex)
        //    {


        //        return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
        //    }




        //}
        //[HttpPost("createTicketDataVoip")]
        //public async Task<IActionResult> createTicketDataVoip([FromBody] TicketVoip request)

        //{
        //    var listTickets = new List<TicketVoip>();
        //    string strTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //    string strGenesysNumber, strThreadID, strAccount, strChannel;

        //    if (request.GenesysNumber == "0")
        //    {
        //        strThreadID = strTime + new Random().Next(1000000, 9999999);
        //        strGenesysNumber = strTime + new Random().Next(100000000, 999999999);
        //        strAccount = request.Account;
        //        strChannel = request.Channel;
        //    }
        //    else
        //    {
        //        strGenesysNumber = request.GenesysNumber;
        //        strThreadID = request.ThreadID;
        //        strAccount = request.Account;
        //        strChannel = request.Channel;
        //    }

        //    try
        //    {
        //        var result = await _context.Database.ExecuteSqlRawAsync(
        //            "EXEC BRA_CreateTicket_DK @TicketNumber, @GenesysNumber, @ThreadID, @Account, @Channel, @CustomerID, @UserName, @Priority, @Status, @Subject, @Kategori, @SubKategori, @NoAju, @NilaiTransaksi, @Kantor, @Pertanyaan, @Jawaban, @Posisi,@NamaPerusahaan, @EmailPerusahaan,@TeleponPerusahaan,@NPWPPerusahaan , @Action",
        //            new SqlParameter("@TicketNumber", strTime),
        //            new SqlParameter("@GenesysNumber", strGenesysNumber),
        //            new SqlParameter("@ThreadID", strThreadID),
        //            new SqlParameter("@Account", strAccount),
        //            new SqlParameter("@Channel", strChannel),
        //            new SqlParameter("@CustomerID", request.CustomerID),
        //            new SqlParameter("@UserName", request.UserName),
        //            new SqlParameter("@Priority", request.Priority),
        //            new SqlParameter("@Status", request.Status),
        //            new SqlParameter("@Subject", request.Subject),
        //            new SqlParameter("@Kategori", request.Kategori),
        //            new SqlParameter("@SubKategori", request.SubKategori),
        //            new SqlParameter("@NoAju", request.NoAju),
        //            new SqlParameter("@NilaiTransaksi", request.NilaiTransaksi),
        //            new SqlParameter("@Kantor", request.Kantor),
        //            new SqlParameter("@Pertanyaan", Uri.EscapeDataString(request.Pertanyaan)),
        //            new SqlParameter("@Jawaban", Uri.EscapeDataString(request.Jawaban)),
        //            new SqlParameter("@Posisi", request.Posisi),
        //            new SqlParameter("@NamaPerusahaan", request.NamaPerusahaan),
        //            new SqlParameter("@EmailPerusahaan", request.EmailPerusahaan),
        //            new SqlParameter("@TeleponPerusahaan", request.TeleponPerusahaan),
        //            new SqlParameter("@NPWPPerusahaan", request.NPWPPerusahaan),
        //            new SqlParameter("@PolisNumber", request.PolisNumber),
        //            new SqlParameter("@Action", request.Action)
        //        );

        //        var ticket = new TicketVoip
        //        {
        //            TicketNumber = strTime,
        //            GenesysNumber = strGenesysNumber,
        //            ThreadID = strThreadID,
        //            Account = strAccount,
        //            Channel = strChannel,
        //            CustomerID = request.CustomerID,
        //            UserName = request.UserName,
        //            Priority = request.Priority,
        //            Status = request.Status,
        //            Subject = request.Subject,
        //            Kategori = request.Kategori,
        //            SubKategori = request.SubKategori,
        //            NoAju = request.NoAju,
        //            NilaiTransaksi = request.NilaiTransaksi,
        //            Kantor = request.Kantor,
        //            Pertanyaan = Uri.EscapeDataString(request.Pertanyaan),
        //            Jawaban = Uri.EscapeDataString(request.Jawaban),
        //            Posisi = request.Posisi,
        //            Action = request.Action
        //        };

        //        listTickets.Add(ticket);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(400, ex.ToString());

        //    }


        //    //var js = new JavaScriptSerializer();
        //    return StatusCode(201, listTickets);
        //}

        [HttpPost("PostCustomerVoip")]
        public async Task<ActionResult> PostCustomerVoip([FromBody] PostDataCustomerVoip contact)
        {
            var contactIdParam = new SqlParameter("@ContactId", contact.ContactId);
            var nameParam = new SqlParameter("@Name", contact.Name);
            var phoneParam = new SqlParameter("@BusinessPhone", contact.NomorTelpon);
            var addressParam = new SqlParameter("@Email", contact.Email); 
            var polisParam = new SqlParameter("@Extension", contact.Extension);
            var Type = new SqlParameter("@Type", contact.Type);

            // Call the stored procedure
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC BRA_InsertContactMobile @ContactId, @Name, @BusinessPhone,@Email,@Extension,@Type",
                    contactIdParam, nameParam, phoneParam, addressParam,polisParam, Type);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception here (consider using a logging framework)
                return StatusCode(500, "An error occurred while inserting the contact.");
            }

            return CreatedAtAction(nameof(PostCustomerVoip), new { id = contact.ContactId }, contact);
        }
    }
}



public partial class PostDataCustomerVoip
{

    public string ContactId { get; set; }
    public string Name { get; set; }
    public string NomorTelpon { get; set; }
    public string Email { get; set; }
    public string Extension { get; set; } 
    public string Type { get; set; }
}

public partial class DataCustomerVoip
{
    public string CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Type { get; set; }
    public string NamePerusahaan { get; set; }
    public string Facebook { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string Extension { get; set; }
    public string ContactId { get; set; }
}


