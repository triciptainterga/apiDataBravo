using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.ExternalServices;
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
        private readonly ExternalApiService _externalApiService;

        public DataKelolaController(BravoContext context, ExternalApiService externalApiService)
        {
            _context = context;
            _externalApiService = externalApiService;
        }

        // GET: api/MCustomers
        [HttpGet("GetMCustomersByHPOrEmaildk")]
        public async Task<ActionResult<IEnumerable<MCustomer>>> GetMCustomersByHPOrEmail(string InputData)
        {
            try
            {


                //  var customers = (await from mCustomer in _context.MCustomers
                //                         join channel in _context.CustomerChannels
                //                         on mCustomer.CustomerId equals channel.CustomerID into customerChannels
                //                         from channel in customerChannels.DefaultIfEmpty()
                //                         where channel.ValueChannel == inputData ||
                //                               mCustomer.Hp.ToLower().Contains(inputData) ||
                //                               mCustomer.Email.ToLower().Contains(inputData) ||
                //                               mCustomer.Instagram.ToLower().Contains(inputData) ||
                //                               mCustomer.Facebook.ToLower().Contains(inputData) ||
                //                               mCustomer.Twitter.ToLower().Contains(inputData) ||
                //                               mCustomer.PolisNumber.ToLower().Contains(inputData)


                //                          .Select(c => new
                //                          {


                //                              Title = c.Tittle,
                //                              CustomerId = c.CustomerId,
                //                              Noktp = c.Noktp,
                //                              Name = c.Name,
                //                              Birth = c.Birth,
                //                              Gender = c.JenisKelamin,
                //                              CompanyName = c.NamaPerusahaan,
                //                              Phone = c.Telepon,
                //                              Email = c.Email,
                //                              Password = c.Password,
                //                              Address = c.Alamat,
                //                              City = c.City,
                //                              Region = c.Region,
                //                              SocialNetwork = c.NetworkSocial,
                //                              CustomerStatus = c.CusStatus,
                //                              Facebook = c.Facebook,
                //                              Twitter = c.Twitter,
                //                              Kaskus = c.Kaskus,
                //                              HomePhone = c.Home,
                //                              OfficePhone = c.Office,
                //                              Mobile = c.Hp,
                //                              OtherContacts = c.Others,
                //                              Message = c.Pesan,
                //                              //AlamatIp = c.AlamatIp,
                //                              SiteId = c.SiteId,
                //                              SitePassword = c.SitePassword,
                //                              // SiteIp = c.SiteIp,
                //                              Login = c.Login,
                //                              Path = c.Path,
                //                              CreatedBy = c.UserCreateCustomer,
                //                              CreatedDate = c.DateCreateCustomer,
                //                              Source = c.SourceCreate,
                //                              UpdatedBy = c.UserUpdate,
                //                              UpdatedDate = c.DateUpdate,
                //                              UpdateStatus = c.StatusUpdated,
                //                              LastUpdatedDate = c.DateLastUpdated,
                //                              Status = c.Status,
                //                              Relations = c.Relations,
                //                              CompanyId = c.CompId,
                //                              AccountNumber = c.NomorRekening,
                //                              Branch = c.Cabang,
                //                              Nik = c.Nik,
                //                              AccountId = c.Cif,
                //                              GroupId = c.GroupId,
                //                              Na = c.Na,
                //                              Instagram = c.Instagram,
                //                              CityBilling = c.Kota,
                //                              Province = c.Provinsi,
                //                              PolicyNumber = c.PolisNumber,
                //                              BillingCity = c.BillingKota,
                //                              BillingProvince = c.BillingProvinsi,
                //                              AutoGenerateId = c.AutoGenerateId,
                //                              NPWP = c.NPWP,
                //                              ContactDynamicId = c.ContactDynamicId
                //                          })
                //.ToListAsync());




                //  var customers = await _context.MCustomers.Where(x => x.Hp == InputData || x.Email == InputData).ToListAsync();
                var customers = (from c in _context.MCustomers
                               join channel in _context.CustomerChannels
                               on c.CustomerId equals channel.CustomerID into customerChannels
                               from channel in customerChannels.DefaultIfEmpty()
                               where channel.ValueChannel == InputData
                                 //||
                                 //      c.Hp.ToLower().Contains(inputData) ||
                                 //      c.Email.ToLower().Contains(inputData) ||
                                 //      c.Instagram.ToLower().Contains(inputData) ||
                                 //      c.Facebook.ToLower().Contains(inputData) ||
                                 //      c.Twitter.ToLower().Contains(inputData) ||
                                 //      c.PolisNumber.ToLower().Contains(inputData)
                                 select new
                               {
                                   Title = c.Tittle,
                                   CustomerId = c.CustomerId,
                                   Noktp = c.Noktp,
                                   Name = c.Name,
                                   Birth = c.Birth,
                                   Gender = c.JenisKelamin,
                                   CompanyName = c.NamaPerusahaan,
                                   Phone = c.Telepon,
                                   Email = c.Email,
                                   Password = c.Password,
                                   Address = c.Alamat,
                                   City = c.City,
                                   Region = c.Region,
                                   SocialNetwork = c.NetworkSocial,
                                   CustomerStatus = c.CusStatus,
                                   Facebook = c.Facebook,
                                   Twitter = c.Twitter,
                                   Kaskus = c.Kaskus,
                                   HomePhone = c.Home,
                                   OfficePhone = c.Office,
                                   Mobile = c.Hp,
                                   OtherContacts = c.Others,
                                   Message = c.Pesan,
                                   //AlamatIp = c.AlamatIp,
                                   SiteId = c.SiteId,
                                   SitePassword = c.SitePassword,
                                   // SiteIp = c.SiteIp,
                                   Login = c.Login,
                                   Path = c.Path,
                                   CreatedBy = c.UserCreateCustomer,
                                   CreatedDate = c.DateCreateCustomer,
                                   Source = c.SourceCreate,
                                   UpdatedBy = c.UserUpdate,
                                   UpdatedDate = c.DateUpdate,
                                   UpdateStatus = c.StatusUpdated,
                                   LastUpdatedDate = c.DateLastUpdated,
                                   Status = c.Status,
                                   Relations = c.Relations,
                                   CompanyId = c.CompId,
                                   AccountNumber = c.NomorRekening,
                                   Branch = c.Cabang,
                                   Nik = c.Nik,
                                   AccountId = c.Cif,
                                   GroupId = c.GroupId,
                                   Na = c.Na,
                                   Instagram = c.Instagram,
                                   CityBilling = c.Kota,
                                   Province = c.Provinsi,
                                   PolicyNumber = c.PolisNumber,
                                   BillingCity = c.BillingKota,
                                   BillingProvince = c.BillingProvinsi,
                                   AutoGenerateId = c.AutoGenerateId,
                                   NPWP = c.NPWP,
                                   ContactDynamicId = c.ContactDynamicId
                               })
                              
              
               .ToList();

                return Ok(customers); // Return 200 OK with the list of customers
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // Example: _logger.LogError(ex, "An error occurred while fetching customers.");

                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }
        }


        //[HttpGet("GetDataType")]
        //public async Task<ActionResult<IEnumerable<BraCustomerType>>> GetDataType()
        //{
        //    try
        //    {
        //        var customers = await _context.BraCustomerTypes
        //      .ToListAsync();
        //        return Ok(customers);
        //    }
        //    catch (Exception ex)
        //    {


        //        return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
        //    }




        //}

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


                return StatusCode(500, ex.ToString()); // Return 500 Internal Server Error
            }




        }


        [HttpGet("ListDataCustomerUnderCompany")]
        public async Task<ActionResult<IEnumerable<MCustomer>>> ListDataCustomerUnderCompany(string input = "Sinar Abadi Jaya,tbk")
        {
            try
            {
                var customers = await _context.MCustomers
                .Where(x => x.GroupId == input || x.NamaPerusahaan == input)
              .ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }




        }

        [HttpGet("ListDataChannel")]
        public async Task<ActionResult<IEnumerable<CustomerChannel>>> ListDataChannel(string CustomerID = "24091720493517")
        {
            try
            {
                var customers = await _context.CustomerChannels
                    .Where(x => x.CustomerID == CustomerID)
              .ToListAsync();

                return StatusCode(200, customers);
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
                return StatusCode(500, ex.ToString());
            }
        }
        [HttpPut("UpdateCustomerChannel")]
        public async Task<ActionResult> UpdateCustomerChannel([FromBody] CustomerChannel customerChannel)
        {
            if (customerChannel == null)
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
        public async Task<ActionResult<IEnumerable<BraNamaPerusahaan>>> GetDataBraNamaPerusahaan(string InputData = "0809894444")
        {
            try
            {
                //  var customers = await _context.BraNamaPerusahaans
                //.Where(x => x.Nama_Perusahaan.ToLower().Contains(InputData.ToLower()) || x.NomorTelepon.ToLower().Contains(InputData.ToLower()))
                //.ToListAsync();

                var customers = await _context.BraNamaPerusahaans
                .Where(x => EF.Functions.Like(x.Nama_Perusahaan.ToLower(), $"%{InputData}%") ||
                         EF.Functions.Like(x.Email.ToLower(), $"%{InputData}%") ||
                         EF.Functions.Like(x.NomorTelepon.ToLower(), $"%{InputData}%")).ToListAsync();


                if (customers.Count == 0)
                {

                    return StatusCode(404, "Data not fond."); // Return 500 Internal Server Error
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, ex.ToString()); // Return 500 Internal Server Error
            }




        }

        [HttpGet("GetDataPerusahaanByAccountId")]
        public async Task<ActionResult<IEnumerable<BraNamaPerusahaan>>> GetDataPerusahaanByAccountId(string AccountId)
        {
            try
            {
                //  var customers = await _context.BraNamaPerusahaans
                //.Where(x => x.Nama_Perusahaan.ToLower().Contains(InputData.ToLower()) || x.NomorTelepon.ToLower().Contains(InputData.ToLower()))
                //.ToListAsync();

                var customers = await _context.BraNamaPerusahaans
                .Where(x => x.AccountID == AccountId).ToListAsync();


                if (customers.Count == 0)
                {

                    return StatusCode(404, "Data not fond."); // Return 500 Internal Server Error
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {


                return StatusCode(500, ex.ToString()); // Return 500 Internal Server Error
            }




        }
        [HttpPost("createTicketDataDk")]
        public async Task<IActionResult> createTicketDataDk([FromBody] Ticket request)

        {
            var listTickets = new List<Ticket>();
            string strTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string strGenesysNumber, strThreadID, strAccount, strChannel;

            if (string.IsNullOrEmpty(request.GenesysNumber))
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
                if (request.Action == "2")
                {

                    var categorydec = _context.TrmCategories.Where(x => x.CategoryId == request.Kategori).Select(x => x.Name).FirstOrDefault();
                    var Subcategorydec = _context.MSubCategoryLv1s.Where(x => x.SubCategory1Id == request.SubKategori).Select(x => x.SubName).FirstOrDefault();

                    var _caseDataDk = new caseData();
                    _caseDataDk.NamaKantor = request.Kantor;
                    _caseDataDk.NamaKategory = categorydec;
                    _caseDataDk.NamaSubCategory = Subcategorydec;
                    _caseDataDk.AccountId = Guid.Parse(request.AccountId);
                    _caseDataDk.ContactId = Guid.Parse(request.ContactId);
                    _caseDataDk.Origin = request.Channel;
                    _caseDataDk.Priority = request.Priority;
                    _caseDataDk.Judul = request.Subject;
                    _caseDataDk.NoTicket = request.TicketNumber;


                    var Accounts = await _externalApiService.PostCaseAsync(_caseDataDk);

                    bool success = false;
                    string exception = "";
                    using (JsonDocument doc = JsonDocument.Parse(Accounts))
                    {
                        JsonElement root = doc.RootElement;

                        // Extract the Success and Exception values
                        success = root.GetProperty("Success").GetBoolean();
                        if (success == true)
                            exception = "Successs Create Ticket from Dynamic.";
                        else
                            exception = root.GetProperty("Exception").GetString();

                        // Output the values
                        Console.WriteLine($"Success: {success}");
                        Console.WriteLine($"Exception: {exception}");
                    }

                    if (success == true)
                        return StatusCode(201, "Success Create Ticket.");
                    else
                        return StatusCode(400, exception);



                }
                else
                {


                    var _strTime = new SqlParameter("@TicketNumber", strTime);
                    var _strGenesysNumber = new SqlParameter("@GenesysNumber", strGenesysNumber);
                    var _strThreadID = new SqlParameter("@ThreadID", strThreadID);
                    var _strAccount = new SqlParameter("@Account", strAccount);
                    var _strChannel = new SqlParameter("@Channel", strChannel);
                    var _CustomerID = new SqlParameter("@CustomerID", request.CustomerID);
                    var _UserName = new SqlParameter("@UserName", request.UserName);
                    var _Priority = new SqlParameter("@Priority", request.Priority);
                    var _Status = new SqlParameter("@Status", request.Status);
                    var _Subject = new SqlParameter("@Subject", request.Subject);
                    var _Kategori = new SqlParameter("@Kategori", request.Kategori);
                    var _SubKategori = new SqlParameter("@SubKategori", request.SubKategori);
                    var _NoAju = new SqlParameter("@NoAju", string.IsNullOrEmpty(request.NoAju) ? "" : request.NoAju);
                    var _NilaiTransaksi = new SqlParameter("@NilaiTransaksi", request.NilaiTransaksi);
                    var _Kantor = new SqlParameter("@Kantor", request.Kantor);
                    var _Pertanyaan = new SqlParameter("@Pertanyaan", request.Pertanyaan);
                    var _Jawaban = new SqlParameter("@Jawaban", request.Jawaban);
                    var _Posisi = new SqlParameter("@Posisi", request.Posisi);
                    var _NamaPerusahaan = new SqlParameter("@NamaPerusahaan", request.NamaPerusahaan);
                    var _EmailPerusahaan = new SqlParameter("@EmailPerusahaan", request.EmailPerusahaan);
                    var _TeleponPerusahaan = new SqlParameter("@TeleponPerusahaan", request.TeleponPerusahaan);
                    var _NPWPPerusahaan = new SqlParameter("@NPWPPerusahaan", request.NPWPPerusahaan);
                    var _Action = new SqlParameter("@Action", request.Action);





                    var result = await _context.Database.ExecuteSqlRawAsync(
                        "EXEC BRA_CreateTicket_DK @TicketNumber, @GenesysNumber, @ThreadID, @Account, @Channel, @CustomerID, @UserName, @Priority, @Status, @Subject, @Kategori, @SubKategori, @NoAju, @NilaiTransaksi, @Kantor, @Pertanyaan, @Jawaban, @Posisi, @NamaPerusahaan, @EmailPerusahaan, @TeleponPerusahaan, @NPWPPerusahaan, @Action",
                                                  _strTime, _strGenesysNumber, _strThreadID, _strAccount, _strChannel, _CustomerID, _UserName, _Priority, _Status, _Subject, _Kategori, _SubKategori, _NoAju, _NilaiTransaksi, _Kantor, _Pertanyaan, _Jawaban, _Posisi, _NamaPerusahaan, _EmailPerusahaan, _TeleponPerusahaan, _NPWPPerusahaan, _Action

                    );
                    //var result = await _context.Database.ExecuteSqlRawAsync(
                    //    "EXEC BRA_CreateTicket_DK @TicketNumber, @GenesysNumber, @ThreadID, @Account, @Channel, @CustomerID, @UserName, @Priority, @Status, @Subject, @Kategori, @SubKategori, @NoAju, @NilaiTransaksi, @Kantor, @Pertanyaan, @Jawaban, @Posisi,@NamaPerusahaan, @EmailPerusahaan,@TeleponPerusahaan,@NPWPPerusahaan , @Action",
                    //    new SqlParameter("@TicketNumber", strTime),
                    //    new SqlParameter("@GenesysNumber", strGenesysNumber),
                    //    new SqlParameter("@ThreadID", strThreadID),
                    //    new SqlParameter("@Account", strAccount),
                    //    new SqlParameter("@Channel", strChannel),
                    //    new SqlParameter("@CustomerID", request.CustomerID),
                    //    new SqlParameter("@UserName", request.UserName),
                    //    new SqlParameter("@Priority", request.Priority),
                    //    new SqlParameter("@Status", request.Status),
                    //    new SqlParameter("@Subject", request.Subject),
                    //    new SqlParameter("@Kategori", request.Kategori),
                    //    new SqlParameter("@SubKategori", request.SubKategori),
                    //    new SqlParameter("@NoAju", request.NoAju),
                    //    new SqlParameter("@NilaiTransaksi", request.NilaiTransaksi),
                    //    new SqlParameter("@Kantor", request.Kantor),
                    //    new SqlParameter("@Pertanyaan", Uri.EscapeDataString(request.Pertanyaan)),
                    //    new SqlParameter("@Jawaban", Uri.EscapeDataString(request.Jawaban)),
                    //    new SqlParameter("@Posisi", request.Posisi),
                    //    new SqlParameter("@NamaPerusahaan", request.NamaPerusahaan),
                    //    new SqlParameter("@EmailPerusahaan", request.EmailPerusahaan),
                    //    new SqlParameter("@TeleponPerusahaan", request.TeleponPerusahaan),
                    //    new SqlParameter("@NPWPPerusahaan", request.NPWPPerusahaan),
                    //    new SqlParameter("@Action", request.Action)
                    //);


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
                        Pertanyaan = request.Pertanyaan,
                        Jawaban = request.Jawaban,
                        NamaPerusahaan = request.NamaPerusahaan,
                        TeleponPerusahaan = request.TeleponPerusahaan,
                        NPWPPerusahaan = request.NPWPPerusahaan,
                        Posisi = request.Posisi,
                        Action = request.Action
                    };

                    listTickets.Add(ticket);
                }
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



            var nameParam = new SqlParameter("@Name", contact.CusTomerName);
            var phoneParam = new SqlParameter("@BusinessPhone", contact.CusTomerPhone);
            var CusTomerEmail = new SqlParameter("@Email", contact.CusTomerEmail);
            var fbParam = new SqlParameter("@FacebookId", contact.Facebook);
            var igParam = new SqlParameter("@InstagramId", contact.Instagram);
            var twiterParam = new SqlParameter("@TwitterId", contact.Twitter);
            var Npwp = new SqlParameter("@NPWP", contact.NPWP);
            var paramAccountid = new SqlParameter("@Accountid", contact.Accountid);
            var Login = new SqlParameter("@UserLogin", contact.Login);
            string _contactId;
            string _accountId;
            string _customerId;
            // Call the stored procedure


            var telpn = await _context.CustomerChannels.Where(x => x.ValueChannel == contact.CusTomerPhone).Select(x => x.ValueChannel).FirstOrDefaultAsync();


            if (telpn != null)
                return StatusCode(500, telpn + " already exist in Telpn");


            var fb = await _context.CustomerChannels.Where(x => x.ValueChannel == contact.Facebook).Select(x => x.ValueChannel).FirstOrDefaultAsync();


            if (fb != null)
                return StatusCode(500, fb + " already exist in facebook");

            var ig = await _context.CustomerChannels.Where(x => x.ValueChannel == contact.Instagram).Select(x => x.ValueChannel).FirstOrDefaultAsync();

            if (ig != null)
                return StatusCode(500, ig + " already exist in Instagram");

            var tw = await _context.CustomerChannels.Where(x => x.ValueChannel == contact.Twitter).Select(x => x.ValueChannel).FirstOrDefaultAsync();
            if (tw != null)
                return StatusCode(500, tw + " already exist in Twiter ");
            try
            {
                var _contact = new contact
                {

                    Name = contact.CusTomerName,
                    BusinessPhone = contact.CusTomerPhone,
                    Email = contact.CusTomerEmail,
                    FacebookId = contact.Facebook,
                    InstagramId = contact.Instagram,
                    TwitterId = contact.Twitter
                };
                // var  contact Data

                var Accounts = await _externalApiService.PostContactAsync(_contact);

                // Parse the JSON
                using (JsonDocument doc = JsonDocument.Parse(Accounts))
                {
                    // Get the ContactId
                    _contactId = doc.RootElement
                       .GetProperty("Datas")[0]
                       .GetProperty("ContactId")
                       .GetString();


                }



                var contactIdParam = new SqlParameter("@ContactId", _contactId);
                var save = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC BRA_InsertContactDK @UserLogin,@ContactId, @Name, @BusinessPhone, @Email, @FacebookId, @InstagramId, @TwitterId,@NPWP,@Accountid",
                    Login, contactIdParam, nameParam, phoneParam, CusTomerEmail, fbParam, igParam, twiterParam, Npwp, paramAccountid);



                _accountId = _context.MCustomers.Where(x => x.Name == contact.CusTomerName && x.Hp == contact.CusTomerPhone && x.NPWP == contact.NPWP && x.UserCreateCustomer == contact.Login).OrderByDescending(x => x.Id)
                   .Select(x => x.Cif).FirstOrDefault();

                _customerId = _context.MCustomers.Where(x => x.Name == contact.CusTomerName && x.Hp == contact.CusTomerPhone && x.NPWP == contact.NPWP && x.UserCreateCustomer == contact.Login).OrderByDescending(x => x.Id)
                  .Select(x => x.CustomerId).FirstOrDefault();


            }

            catch (SqlException ex)
            {
                return StatusCode(500, ex.ToString());
            }
            catch (Exception ex)
            {
                // Log the exception here (consider using a logging framework)
                return StatusCode(500, ex.ToString());
            }
            contact.ContactId = _contactId;
            contact.Accountid = _accountId;
            contact.CusTomerId = _customerId;

            return CreatedAtAction(nameof(PostCustomer), new { contactId = _contactId, Accountid = _accountId }, contact);
        }

        [HttpPut("UpdatePerusahaan")]
        public async Task<ActionResult<BraNamaPerusahaan>> UpdatePerusahaan([FromBody] BraNamaPerusahaan perusahaan)
        {
            if (perusahaan == null)
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

            var _AccountId = "";

            if (perusahaan == null)
            {
                return BadRequest("Invalid data.");

            }



            try
            {
                var _account = new account
                {



                    AccountName = perusahaan.Nama_Perusahaan,
                    MainPhone = perusahaan.NomorTelepon,
                    NPWP = perusahaan.NPWP


                };
                // var  contact Data

                var Accounts = await _externalApiService.PostAccountAsync(_account);

                using (JsonDocument doc = JsonDocument.Parse(Accounts))
                {
                    // Get the ContactId
                    _AccountId = doc.RootElement
                       .GetProperty("Datas")[0]
                       .GetProperty("AccountId")
                       .GetString();


                }


                if (perusahaan.CreatedBy == null)
                    perusahaan.CreatedBy = "System";

                perusahaan.CreatedDate = DateTime.Now;

                perusahaan.AccountID = _AccountId;



                //_context.BraNamaPerusahaans.Add(perusahaan);
                //await _context.SaveChangesAsync();
                var TrxID = new SqlParameter("@TrxID", "0");
                var CreatedBy = new SqlParameter("@TrxUserName", perusahaan.CreatedBy);
                var Nama_Perusahaan = new SqlParameter("@TrxNama", perusahaan.Nama_Perusahaan);
                var Type = new SqlParameter("@TrxEmail", perusahaan.Type);
                var TrxTelepon = new SqlParameter("@TrxTelepon", "");
                var NPWP = new SqlParameter("@TrxNPWP", perusahaan.NPWP);
                var TrxAction = new SqlParameter("@TrxAction", "INSERT");
                var AccountId = new SqlParameter("@Accountid", _AccountId);

                var save = await _context.Database.ExecuteSqlRawAsync(
                  "EXEC BRA_Perusahaan @TrxID,@TrxUserName, @TrxNama, @TrxEmail, @TrxTelepon, @TrxNPWP, @TrxAction,@Accountid",
                  TrxID, CreatedBy, Nama_Perusahaan, Type, TrxTelepon, NPWP, TrxAction, AccountId);


                var _perusuhaanId = await _context.BraNamaPerusahaans.Where(x => x.AccountID == _AccountId).Select(x => x.PerusahaanID).SingleOrDefaultAsync();

                perusahaan.PerusahaanID = _perusuhaanId;




                return CreatedAtAction(nameof(CreatePerusahaan), new { id = perusahaan.Id }, perusahaan);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString()); // 500 Internal Server Error
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

    public string CusTomerId { get; set; }
    public string CusTomerName { get; set; }
    public string CusTomerEmail { get; set; }
    public string CusTomerPhone { get; set; }
    public string Facebook { get; set; }
    public string Instagram { get; set; }
    public string Twitter { get; set; }
    public string NPWP { get; set; }
    public string ContactId { get; set; }
    public string Accountid { get; set; }
    public string Login { get; set; }
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

public partial class MCustomerDto
{


    public int Id { get; set; }
    public string Tittle { get; set; }
    public string CustomerId { get; set; }
    public string Noktp { get; set; }
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public string JenisKelamin { get; set; }
    public string NamaPerusahaan { get; set; }
    public string Telepon { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Alamat { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string NetworkSocial { get; set; }
    public string CusStatus { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string Kaskus { get; set; }
    public string Home { get; set; }
    public string Office { get; set; }
    public string Hp { get; set; }
    public string Others { get; set; }
    public string Pesan { get; set; }
    public string AlamatIp { get; set; }
    public string SiteId { get; set; }
    public string SitePassword { get; set; }
    public string SiteIp { get; set; }
    public int? Login { get; set; }
    public string Path { get; set; }
    public string UserCreateCustomer { get; set; }
    public DateTime? DateCreateCustomer { get; set; }
    public string SourceCreate { get; set; }
    public string UserUpdate { get; set; }
    public DateTime? DateUpdate { get; set; }
    public string StatusUpdated { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public string Status { get; set; }
    public string Relations { get; set; }
    public string CompId { get; set; }
    public string NomorRekening { get; set; }
    public string Cabang { get; set; }
    public string AccountId { get; set; }
    public string Nik { get; set; }
    //public string Cif { get; set; }
    public string GroupId { get; set; }
    public string Na { get; set; }
    public string Instagram { get; set; }
    public string Provinsi { get; set; }
    public string Kota { get; set; }
    public string PolisNumber { get; set; }
    public string BillingKota { get; set; }
    public string BillingProvinsi { get; set; }
    public string AutoGenerateId { get; set; }
    public string NPWP { get; set; }

    public string ContactDynamicId { get; set; }
}

