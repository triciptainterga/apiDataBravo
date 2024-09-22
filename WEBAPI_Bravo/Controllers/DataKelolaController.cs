using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class DataKelolaController : ControllerBase
    {
        private readonly BravoContext _context;

        public DataKelolaController(BravoContext context)
        {
            _context = context;
        }

        // GET: api/MCustomers
        [HttpGet("GetMCustomersByHPOrEmaildk")]
        public async Task<ActionResult<IEnumerable<MCustomer>>> GetMCustomersByHPOrEmail(string InputData)
        {
            try
            {
                var customers = await _context.MCustomers.Where(x => x.Hp == InputData || x.Email == InputData).ToListAsync();
                return Ok(customers); // Return 200 OK with the list of customers
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // Example: _logger.LogError(ex, "An error occurred while fetching customers.");

                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }
        }


        [HttpGet("GetDataType")]
        public async Task<ActionResult<IEnumerable<BraCustomerType>>> GetDataType()
        {
            try
            {
                var customers = await _context.BraCustomerTypes
              .ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }




        }

        [HttpGet("GetSourceType")]
        public async Task<ActionResult<IEnumerable<MSourceType>>> GetSourceType()
        {
            try
            {
                var customers = await _context.MSourceTypes
              .ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500,ex.ToString()); // Return 500 Internal Server Error
            }




        }


        [HttpGet("ListDataCustomerUnderCompany")]
        public async Task<ActionResult<IEnumerable<MCustomer>>> ListDataCustomerUnderCompany(string input= "Sinar Abadi Jaya,tbk")
        {
            try
            {
                var customers = await _context.MCustomers
                .Where (x => x.GroupId == input || x.NamaPerusahaan == input)
              .ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }




        }

        [HttpGet("ListDataChannel")]
        public async Task<ActionResult<IEnumerable<CustomerChannel>>> ListDataChannel(string CustomerID= "24091720493517")
        {
            try
            {
                var customers = await _context.CustomerChannels
                    .Where (x => x.CustomerID == CustomerID)
              .ToListAsync();

                return StatusCode(200,customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }




        }

        [HttpPost("CreateCustomerChannel")]
        public async Task<ActionResult<CustomerChannel>> CreateCustomerChannel([FromBody] CustomerChannel customerChannel)
        {
            if (customerChannel == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                _context.CustomerChannels.Add(customerChannel);
                await _context.SaveChangesAsync();

                return StatusCode(201, customerChannel);
            }
           
            catch (Exception ex)
            {
                return StatusCode(500,ex.ToString() );
            }
        }
        [HttpPut("UpdateCustomerChannel")]
        public async Task<ActionResult> UpdateCustomerChannel( [FromBody] CustomerChannel customerChannel)
        {
            if (customerChannel == null )
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var existingChannel = await _context.CustomerChannels.FindAsync(customerChannel.ID);
                if (existingChannel == null)
                {
                    return NotFound($"CustomerChannel with ID {customerChannel.ID} not found.");
                }

                // Update properties
                existingChannel.CustomerID = customerChannel.CustomerID;
                existingChannel.FlagChannel = customerChannel.FlagChannel;
                existingChannel.ValueChannel = customerChannel.ValueChannel;
                existingChannel.DateUpdate = customerChannel.DateUpdate;
                existingChannel.UserUpdate = customerChannel.UserUpdate;
                existingChannel.Status = customerChannel.Status;

                _context.Entry(existingChannel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return StatusCode(201, customerChannel); // 204 No Content
            }
           
          
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        // DELETE
        [HttpDelete("DeleteCustomerChannel/{id}")]
        public async Task<ActionResult> DeleteCustomerChannel(int id)
        {
            var customerChannel = await _context.CustomerChannels.FindAsync(id);
            if (customerChannel == null)
            {
                return NotFound($"CustomerChannel with ID {id} not found.");
            }

            _context.CustomerChannels.Remove(customerChannel);
            await _context.SaveChangesAsync();

            return StatusCode(201, customerChannel); 
        }
        [HttpGet("GetDataBraNamaPerusahaandk")]
        public async Task<ActionResult<IEnumerable<BraNamaPerusahaan>>> GetDataBraNamaPerusahaan(string InputData= "0809894444")
        {
            try
            {
                var customers = await _context.BraNamaPerusahaans
              .Where(x => x.Nama_Perusahaan.Contains(InputData) || x.NomorTelepon.Contains(InputData))
              .ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }




        }
        [HttpPost("createTicketDataDk")]
        public async Task<IActionResult> createTicketDataDk([FromBody] Ticket request)

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


        [HttpPost("PostCustomer")]
        public async Task<ActionResult> PostCustomer([FromBody] DataCustomer contact)
        {
            var contactIdParam = new SqlParameter("@ContactId", contact.ContactId);
            var nameParam = new SqlParameter("@Name", contact.CusTomerName);
            var phoneParam = new SqlParameter("@BusinessPhone", contact.CusTomerPhone);
            var addressParam = new SqlParameter("@Email", contact.CusTomerEmail);
            var fbParam = new SqlParameter("@FacebookId", contact.Facebook);
            var igParam = new SqlParameter("@InstagramId", contact.Instagram);
            var twiterParam = new SqlParameter("@TwitterId", contact.Twitter);
            var Npwp = new SqlParameter("@NPWP", contact.NPWP);
            var @Accountid = new SqlParameter("@Accountid", contact.Accountid);

            // Call the stored procedure
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertContact @ContactId, @Name, @BusinessPhone, @Email, @FacebookId, @InstagramId, @TwitterId,@NPWP,@Accountid",
                    contactIdParam, nameParam, phoneParam, addressParam, fbParam, igParam, twiterParam, Npwp, Accountid);
            }
            catch (Exception ex)
            {
                // Log the exception here (consider using a logging framework)
                return StatusCode(500, ex.ToString());
            }

            return CreatedAtAction(nameof(PostCustomer), new { id = contact.ContactId }, contact);
        }

        [HttpPut("UpdatePerusahaan")]
        public async Task<ActionResult<BraNamaPerusahaan>> UpdatePerusahaan( [FromBody] BraNamaPerusahaan perusahaan)
        {
            if (perusahaan == null )
            {
                return BadRequest("Invalid data.");
            }

            // Additional validation can be added here if needed
            if (string.IsNullOrWhiteSpace(perusahaan.Nama_Perusahaan)) // Assuming 'Name' is a property of BraNamaPerusahaan
            {
                return BadRequest("The 'Name' field is required.");
            }

            try
            {
                var existingPerusahaan = await _context.BraNamaPerusahaans.FindAsync(perusahaan.Id);
                if (existingPerusahaan == null)
                {
                    return NotFound($"Perusahaan with ID {perusahaan.Id} not found.");
                }

                // Update properties
                existingPerusahaan.Nama_Perusahaan = perusahaan.Nama_Perusahaan;
                existingPerusahaan.PerusahaanID = perusahaan.PerusahaanID; 
                existingPerusahaan.NPWP = perusahaan.NPWP; 
                existingPerusahaan.NomorTelepon = perusahaan.NomorTelepon; 
                existingPerusahaan.Email = perusahaan.Email; 
                existingPerusahaan.Type = perusahaan.Type; 
               
       
      
        _context.Entry(existingPerusahaan).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return StatusCode(201, perusahaan); // 204 No Content
            }
           
          
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, ex.ToString());
            }
        }
        [HttpPost("CustomerFetch")]
        public async Task<ActionResult> CustomerFetch([FromBody] CustomerFetch contact)
        {
            var contactIdParam = new SqlParameter("@TrxCustomerID", contact.CustomerID);
            var nameParam = new SqlParameter("@TrxName", contact.Name);
            var emailParam = new SqlParameter("@TrxEmail", contact.Email);
            var Phone = new SqlParameter("@TrxPhone", contact.Phone);
            var CusTomerType = new SqlParameter("@TrxCusTomerType", contact.CusTomerType);
            var CusTomerPerusahaan = new SqlParameter("@TrxCusTomerPerusahaan", contact.CusTomerPerusahaan);
            var NIK = new SqlParameter("@TrxNIK", contact.NIK);
            var Npwp = new SqlParameter("@TrxFacebook", contact.Facebook);
            var TrxInstagram = new SqlParameter("@TrxInstagram", contact.Instagram);
            var TrxTwitter = new SqlParameter("@TrxTwitter", contact.Twitter);
            var TrxNPWP = new SqlParameter("@TrxNPWP", contact.NPWP);
            var TrxAddress = new SqlParameter("@TrxAddress", contact.Address);
            var TrxUserName = new SqlParameter("@TrxUserName", contact.UserName);

            // Call the stored procedure
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC BRA_Customer_Fetch @TrxCustomerID, @TrxName, @TrxEmail, @Email, @TrxPhone, @TrxCusTomerType, @TrxCusTomerPerusahaan,@TrxNIK,@TrxFacebook",
                    "@TrxInstagram,@TrxTwitter,@TrxNPWP,@TrxAddress,@TrxUserName",
                    contactIdParam, nameParam, emailParam, Phone, CusTomerType, CusTomerPerusahaan, NIK, Npwp, TrxInstagram, TrxTwitter, TrxNPWP, TrxAddress, TrxUserName);
            }
            catch (Exception ex)
            {
                // Log the exception here (consider using a logging framework)
                return StatusCode(500, ex.ToString());
            }

            return CreatedAtAction(nameof(PostCustomer), contact);
        }

        [HttpPost("CreatePerusahaandk")]
        public async Task<ActionResult<BraNamaPerusahaan>> CreatePerusahaan([FromBody] BraNamaPerusahaan perusahaan)
        {
            if (perusahaan == null)
            {
                return BadRequest("Invalid data.");

            }

            try
            {

                _context.BraNamaPerusahaans.Add(perusahaan);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreatePerusahaan), new { id = perusahaan.Id }, perusahaan);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error"); // 500 Internal Server Error
            }


        }


    }
}
public partial class TicketDk
{


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
public partial class DataCustomer
{
  
    public string CusTomerName { get; set; }
    public string CusTomerEmail { get; set; }
    public string CusTomerPhone { get; set; }
    public string Facebook { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string NPWP { get; set; }
    public string ContactId { get; set; }
    public string Accountid { get; set; }
}

public partial class CustomerFetch
{

    public string CustomerID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CusTomerType { get; set; }
    public string CusTomerPerusahaan { get; set; }
    public string NIK { get; set; }
    public string Facebook { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string NPWP { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
}

