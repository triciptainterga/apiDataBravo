using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        [HttpGet("GetDataAgentPusat")]

        public IActionResult GetAgentActivity()
        {

            string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var lines = System.IO.File.ReadAllLines(filePath);
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');

                    //if (values.Length == 10) // Ensure that each line has the correct number of values
                    //{

                    if (values[10] == "Informasi")
                    {
                        var agentActivity = new AgentActivity
                        {


                            // Split = int.Parse(values[0]),
                            AgentName = values[1],
                            // LoginId = int.Parse(values[2]),
                            Extn = int.Parse(values[3]),
                            //AuxReason = values[4],
                            State = values[5],
                            //SplitSkill = int.Parse(values[6]),
                            Time = values[7].ToString(),
                            Skill = values[10],
                            Calls = int.Parse(values[11].ToString())
                        };

                        agentActivities.Add(agentActivity);
                    }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.ToString());
            }

        }
        [HttpGet("GetDataAgentSoetta")]
        public IActionResult GetDataAgentSoetta()
        {
            string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var lines = System.IO.File.ReadAllLines(filePath);
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');

                    
                    if (values[10] == "CC Soetta")
                    {
                        var agentActivity = new AgentActivity
                        {


                            // Split = int.Parse(values[0]),
                            AgentName = values[1],
                            // LoginId = int.Parse(values[2]),
                            Extn = int.Parse(values[3]),
                            //AuxReason = values[4],
                            State = values[5],
                            //SplitSkill = int.Parse(values[6]),
                            Time = values[7].ToString(),
                            Skill = values[10],
                            Calls = int.Parse(values[11].ToString())
                        };

                        agentActivities.Add(agentActivity);
                    }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }
        }


        [HttpGet("GetDataAgentPasarBaru")]
        public IActionResult GetDataAgentPasarBaru()
        {
            string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var lines = System.IO.File.ReadAllLines(filePath);
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');


                    if (values[10] == "CC Ps Baru")
                    {
                        var agentActivity = new AgentActivity
                        {


                            // Split = int.Parse(values[0]),
                            AgentName = values[1],
                            // LoginId = int.Parse(values[2]),
                            Extn = int.Parse(values[3]),
                            //AuxReason = values[4],
                            State = values[5],
                            //SplitSkill = int.Parse(values[6]),
                            Time = values[7].ToString(),
                            Skill = values[10],
                            Calls = int.Parse(values[11].ToString())
                        };

                        agentActivities.Add(agentActivity);
                    }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("GetDataStatus")]
        public IActionResult GetDataStatus()
        {
          //  string filePath = "C://GrafikalStatus.txt"; // ganti sesuai path Anda
            string filePath = @"\\10.216.206.3\Acsauto\GraficalStatus.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                    return NotFound("File not found.");

                var lines = System.IO.File.ReadAllLines(filePath);
                var statusIndex = Array.FindIndex(lines, line => line.Trim().StartsWith("Agent Status for Split/Skill"));

                if (statusIndex < 0 || statusIndex + 2 >= lines.Length)
                    return BadRequest("Agent Status section not found or incomplete.");

                var headerLine = lines[statusIndex + 1].Trim(); // Baris: AVAIL;ACD;ACW;AUX;RING;OTHER
                var valueLine = lines[statusIndex + 2].Trim();  // Baris: 4;0;2;2;0;0

                var headers = headerLine.Split(';');
                var values = valueLine.Split(';');

                if (headers.Length != values.Length)
                    return BadRequest("Mismatch between header and value columns.");

                var result = new Dictionary<string, int>();
                for (int i = 0; i < headers.Length; i++)
                {
                    if (int.TryParse(values[i], out int val))
                        result[headers[i]] = val;
                    else
                        result[headers[i]] = 0;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        [HttpGet("GetDataAgentAll")]
        public IActionResult GetDataAgentAll()
        {
             string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path
            //string filePath = "C:/WalboardBC/main-dark/PHP/AgentActivityAll.txt"; // Adjust this path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                // Read the file content
                var lines = System.IO.File.ReadAllLines(filePath);

                // Skip the first two lines (header and extra information)
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));

                // Parse the data
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');

                    //if (values.Length == 10) // Ensure that each line has the correct number of values
                    //{
                    var agentActivity = new AgentActivity
                    {
                        // Split = int.Parse(values[0]),
                        AgentName = values[1],
                        // LoginId = int.Parse(values[2]),
                        Extn = int.Parse(values[3]),
                        //AuxReason = values[4],
                        State = values[5],
                        //SplitSkill = int.Parse(values[6]),
                        Time = values[7].ToString(),
                        Skill = values[10],
                        Calls = int.Parse(values[11].ToString())
                    };

                    agentActivities.Add(agentActivity);
                    // }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {
                // Log the error and return a general error message
                // You can log it here if needed, e.g., _logger.LogError(ex, "Error reading or processing the file.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetDataAgentAllData")]
        public IActionResult GetDataAgentAllTest()
        {
            string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                // Read the file content
                var lines = System.IO.File.ReadAllLines(filePath);

                // Skip the first two lines (header and extra information)
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));

                // Parse the data
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');

                    //if (values.Length == 10) // Ensure that each line has the correct number of values
                    //{
                    var agentActivity = new AgentActivity
                    {
                        // Split = int.Parse(values[0]),
                        AgentName = values[1],
                        // LoginId = int.Parse(values[2]),
                        Extn = int.Parse(values[3]),
                        //AuxReason = values[4],
                        State = values[5],
                        //SplitSkill = int.Parse(values[6]),
                        Time = values[7].ToString(),
                        Skill = values[10],
                        Calls = int.Parse(values[11].ToString())
                    };

                    agentActivities.Add(agentActivity);
                    // }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {
                // Log the error and return a general error message
                // You can log it here if needed, e.g., _logger.LogError(ex, "Error reading or processing the file.");
                return StatusCode(500, "An error occurred while processing the request."); 
            }
        }

        [HttpGet("GetDataAgentPriuk")]
        public IActionResult GetDataAgentPriuk()
        {
            string filePath = @"\\10.216.206.3\Acsauto\AgentActivityAll.txt"; // Network path

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var lines = System.IO.File.ReadAllLines(filePath);
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));
                var agentActivities = new List<AgentActivity>();

                foreach (var line in dataLines)
                {
                    var values = line.Split(';');


                    if (values[10] == "CC Priok")
                    {
                        var agentActivity = new AgentActivity
                        {


                            // Split = int.Parse(values[0]),
                            AgentName = values[1],
                            // LoginId = int.Parse(values[2]),
                            Extn = int.Parse(values[3]),
                            //AuxReason = values[4],
                            State = values[5],
                            //SplitSkill = int.Parse(values[6]),
                            Time = values[7].ToString(),
                            Skill = values[10],
                            Calls = int.Parse(values[11].ToString())
                        };

                        agentActivities.Add(agentActivity);
                    }
                }

                return Ok(agentActivities); // Return the parsed data as a list
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }
        }
        [HttpGet("GetDataAvaya")]
        public async Task<ActionResult<IEnumerable<string>>> GetDataAvaya()
        {
            string inputFilePath = @"C:\WalboardBC\main-dark\PHP\AgentActivityAll.acsauto"; // Path to the .acsauto file
            string outputFilePath = @"C:\WalboardBC\main-dark\PHP\output.txt"; // Path to the output .txt file

            try
            {
                // Step 1: Check if the file exists
                if (!System.IO.File.Exists(inputFilePath))
                {
                    return NotFound("The .acsauto file does not exist.");
                }

                // Step 2: Read the .acsauto file
                string[] lines = System.IO.File.ReadAllLines(inputFilePath);

                // Step 3: Process the data (for simplicity, we just write it directly to a .txt file)
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (var line in lines)
                    {
                        // Writing each line to the .txt file
                        writer.WriteLine(line);
                    }
                }

                // Step 4: Optionally return the content of the .acsauto file as a response (for debugging purposes or further processing)
                return Ok(lines);  // Return the contents as a list of strings (each line of the file)
            }
            catch (Exception ex)
            {
                // Log the error (you can replace this with a logger for real logging in production)
                Console.WriteLine($"Error: {ex.Message}");

                // Return a 500 Internal Server Error response with a descriptive message
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
public class AgentActivity
{
    public int Split { get; set; }
    public string AgentName { get; set; }
    public int LoginId { get; set; }
    public int Extn { get; set; }
    public string AuxReason { get; set; }
    public string State { get; set; }
    public int SplitSkill { get; set; }
    public string Time { get; set; }
    public string VdnName { get; set; }
    public string InterruptStatus { get; set; }
    public string Skill { get; set; }
    public int Calls { get; set; }
    public string Art { get; set; }
    public string Aht { get; set; }
}

public class CallData
{
    public string SkillState { get; set; }
    public int AgentsStaffed { get; set; }
    public TimeSpan? AvgAbanTime { get; set; }  // Nullable TimeSpan
    public int AbanCalls { get; set; }
    public TimeSpan? AvgACDTime { get; set; }  // Nullable TimeSpan
    public int ACDCalls { get; set; }
    public double AvgSpeedAns { get; set; }
    public double TotalIACDACW { get; set; }
    public double AnsCallsPercentage { get; set; }
    public double AbanCallsPercentage { get; set; }
    public double ACDTimePercentage { get; set; }
    public int AUXIncalls { get; set; }
    public int AgentsAvail { get; set; }
    public double ACCEPTABLE { get; set; }
    public double RingTime { get; set; }
    public int CallsWaiting { get; set; }
    public double AcceptPercentage { get; set; }
    public double AvgACWTime { get; set; }
    public int CallsOffered { get; set; }
    public int ACWIncalls { get; set; }
    public int AgentsInAUX { get; set; }
    public int AgentsOnACDCalls { get; set; }
    public int InQueue { get; set; }
    public int OldestCallWaiting { get; set; }
    public TimeSpan? ACDTime { get; set; }  // Nullable TimeSpan
    public TimeSpan? ACWTime { get; set; }  // Nullable TimeSpan
    public TimeSpan? AvgACDTime2 { get; set; }  // Nullable TimeSpan
}


