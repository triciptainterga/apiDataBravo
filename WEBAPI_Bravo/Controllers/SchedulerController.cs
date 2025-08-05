using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;


namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly BravoContext _context;
        private readonly IConfiguration _configuration;

        public SchedulerController(BravoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/MCustomers

        [HttpGet("TotalAgentAssign")]
        public async Task<List<AgentAssign>> GetDataWbByCount(DateTime DateAssign, string Channel)
        {
            var users = new List<AgentAssign>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC UIDESK_SCH_TrmMasterTransaction @TrxID, @TrxUserName, @TrxAction, @TrxActionType"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;

                        // Add parameters
                        var typeParameter = new SqlParameter("@TrxID", DateAssign);
                        var typeParameter2 = new SqlParameter("@TrxUserName", Channel);
                        var typeParameter3 = new SqlParameter("@TrxAction", "API");
                        var typeParameter4 = new SqlParameter("@TrxActionType", "Count");

                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2); // Add this line
                        command.Parameters.Add(typeParameter3);
                        command.Parameters.Add(typeParameter4);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new AgentAssign
                                {
                                    TotalAgentAssign = result.GetInt32(0)
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
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., log the error)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        [HttpGet("ListAgent")]
        public async Task<List<ListAgent>> ListAgent(DateTime DateAssign, string Channel)
        {
            var users = new List<ListAgent>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC UIDESK_SCH_TrmMasterTransaction @TrxID, @TrxUserName, @TrxAction, @TrxActionType";
                        command.CommandType = CommandType.Text;

                        // Add parameters to the command
                        command.Parameters.Add(new SqlParameter("@TrxID", DateAssign));
                        command.Parameters.Add(new SqlParameter("@TrxUserName", Channel));
                        command.Parameters.Add(new SqlParameter("@TrxAction", "API"));
                        command.Parameters.Add(new SqlParameter("@TrxActionType", "List"));

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new ListAgent
                                {
                                    Name = result.GetString(0),
                                    UserName = result.GetString(1)
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
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., log the error)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }

        [HttpGet("TotalKebutuhan")]
        public async Task<List<TotalKebutuhan>> TotalKebutuhan()
        {
            var users = new List<TotalKebutuhan>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC UIDESK_SCH_TrmMasterTransaction @TrxID, @TrxUserName, @TrxAction, @TrxActionType";
                        command.CommandType = CommandType.Text;

                        var trxIdParameter = new SqlParameter("@TrxID", "");
                        var trxUserNameParameter = new SqlParameter("@TrxUserName", ""); // Set a valid username
                        var trxActionParameter = new SqlParameter("@TrxAction", "API");
                        var trxActionTypeParameter = new SqlParameter("@TrxActionType", "TotalKebutuhan");

                        command.Parameters.Add(trxIdParameter);
                        command.Parameters.Add(trxUserNameParameter);
                        command.Parameters.Add(trxActionParameter);
                        command.Parameters.Add(trxActionTypeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new TotalKebutuhan
                                {
                                    TotalKebutuhanAgent = result.GetInt32(0),
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        [HttpGet("SCHSesiParam")]
        public async Task<List<Params>> SCH_SesiParam() // Add parameter here
        {
            var users = new List<Params>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC UIDESK_SCH_TrmMasterTransaction @TrxID, @TrxUserName, @TrxAction, @TrxActionType";
                        command.CommandType = CommandType.Text;

                        // Declare the parameters
                        var typeParameter = new SqlParameter("@TrxID", ""); // Replace with actual value if needed
                        var typeParameter2 = new SqlParameter("@TrxUserName", ""); // Use the passed value
                        var typeParameter3 = new SqlParameter("@TrxAction", "API");
                        var typeParameter4 = new SqlParameter("@TrxActionType", "Scheme");

                        // Add the parameters to the command
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);
                        command.Parameters.Add(typeParameter3);
                        command.Parameters.Add(typeParameter4);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new Params
                                {
                                    Id = result.GetInt32(0),
                                    SesiParam = result.GetString(1),
                                    ValueParam = result.GetString(2),
                                    Year = result.GetInt32(3),
                                    Month = result.GetInt32(4),
                                    Date = result.GetInt32(5),
                                    Channel = result.GetString(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;



        }

        [HttpPost("schedule")]
        public async Task<IActionResult> Schedule([FromBody] object jsonData)
        {
            if (jsonData == null)
            {
                return BadRequest("No data provided.");
            }

            // Serialize the incoming JSON data to a string
            string jsonDataString = JsonSerializer.Serialize(jsonData);

            // Retrieve the connection string from appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection2");

            try
            {
                // Create a new SqlConnection with the connection string
                using (var connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    await connection.OpenAsync();

                    // Create a SQL command to execute the stored procedure
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[dbo].[UIDESK_SCH_AUTOSCHEDULE]";

                        // Add the JSON data as a parameter
                        var jsonParam = new SqlParameter("@jsonData", SqlDbType.NVarChar)
                        {
                            Value = jsonDataString // Pass the serialized JSON string here
                        };
                        command.Parameters.Add(jsonParam);

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();
                    }
                }

                // Return a success message if everything goes well
                return Ok("Schedule updated successfully");
            }
            catch (SqlException ex)
            {
                // Log the SQL exception details (optional)
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the general exception details (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    public class Params
    {
        public int Id { get; set; }
        public string SesiParam { get; set; }
        public string ValueParam { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public string Channel { get; set; }
        
    }
    public class AgentAssign
    {
        public int TotalAgentAssign { get; set; }

    }
    public class TotalKebutuhan
    {
        public int TotalKebutuhanAgent { get; set; }

    }
    public class ListAgent
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        
    }
    public class ShiftSchedule
    {
        public string DateFixed { get; set; }
        public string Interval { get; set; }
        public int NeededAgents { get; set; }
        public List<string> AssignedAgents { get; set; }
        public string ChannelAgents { get; set; }
        public string DayAgents { get; set; }
    }


}







