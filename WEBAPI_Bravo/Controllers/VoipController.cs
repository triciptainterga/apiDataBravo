using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;


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
                    contactIdParam, nameParam, phoneParam, addressParam, polisParam, Type);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception here (consider using a logging framework)
                return StatusCode(500, "An error occurred while inserting the contact.");
            }

            return CreatedAtAction(nameof(PostCustomerVoip), new { id = contact.ContactId }, contact);
        }
      
        [HttpGet("GetDataWbByCount")]
        public async Task<List<datas>> GetDataWbByCount(string type)
        {
            var users = new List<datas>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC WalboardBravo2 @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", type);
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas
                                {
                                    Jenis = result.GetString(0),
                                    Jumlah = result.GetDouble(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions (e.g., log the error)
                Console.WriteLine($"SQL error: {sqlEx.Message}");
                // Optionally, you could throw the exception or return an empty list
                // throw; // Uncomment this if you want to propagate the exception
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., log the error)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you could throw the exception or return an empty list
                // throw; // Uncomment this if you want to propagate the exception
            }

            return users;
        }

        [HttpGet("GetDataWbByList")]
        public async Task<List<datas2>> GetDataWbByList(string type)
        {
            var users = new List<datas2>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC WalboardBravo2 @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", type);
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new datas2
                                {
                                    Jenis = result.GetString(0),
                                    Jumlah = result.GetInt32(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions (e.g., log the error)
                Console.WriteLine($"SQL error: {sqlEx.Message}");
                // Optionally, you could throw the exception or return an empty list
                // throw; // Uncomment this if you want to propagate the exception
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., log the error)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you could throw the exception or return an empty list
                // throw; // Uncomment this if you want to propagate the exception
            }

            return users;
        }
        //public async Task<ActionResult<List<datas>>> GetDataWb(string type)
        //{
        //    var typeParam = new SqlParameter("@Type", type);

        //    List<datas> data;

        //    // Call the stored procedure
        //    try
        //    {
        //        data = await _context.Set<datas>()
        //            .FromSqlRaw("EXEC WalboardBravo2 @Type", typeParam)
        //            .ToListAsync();
        //    }
        //    catch (Exception ex) // Catch more general exceptions
        //    {
        //        // Log the exception here (consider using a logging framework)
        //        // For example: _logger.LogError(ex, "Error retrieving data from stored procedure");
        //        return StatusCode(500, ex.ToString());
        //    }

        //    return Ok(data); // Return 200 OK with the data
        //}
    }
    public class datas
    {
        public string Jenis { get; set; }
        public double Jumlah { get; set; }
    }
    public class datas2
    {
        public string Jenis { get; set; }
        public int Jumlah { get; set; }
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


