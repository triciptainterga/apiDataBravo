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
using Microsoft.Extensions.Configuration;
using WEBAPI_Bravo.DinamicModel;
using WEBAPI_Bravo.Model;
using WebApiBravo.Models;


namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallboadController : ControllerBase
    {
        private readonly BravoContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString2;

        public WallboadController(BravoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _connectionString2 = _configuration.GetConnectionString("DefaultConnection2");
        }




        [HttpGet("GetDataAgentPerPormancePusat")]
        public IActionResult GetDataAgentPriuk()
        {
            string filePath = "C:/WalboardBC/main-dark/PHP/AgentActivityCall.txt"; // Adjust this path
           // string filePath = @"\\10.216.206.3\Acsauto\AgentActivityCall.txt";

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


                    if (values[12] == "Informasi")
                    {
                        var agentActivity = new AgentActivity
                        {


                            // Split = int.Parse(values[0]),
                            AgentName = values[1],
                            Calls = int.Parse(values[3].ToString()),
                            Art = values[9].ToString(),
                            Aht = values[11].ToString(),


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

        [HttpGet("GetWbDataWbByCount")]
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

        [HttpGet("GetWbDataWbByList")]
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



        [HttpGet("GetWbDataEmailAllSide")]
        public async Task<List<dataEmailWB>> GetWbDataEmailAllSide()
        {
            var users = new List<dataEmailWB>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataEmailWB
                                {
                                    Name = result.GetString(0),
                                    UserName = result.GetString(1),
                                    SideId = result.GetString(2),
                                    LoginTime = result.GetString(3),
                                    HandleTime = result.GetString(4),
                                    Status = result.GetString(5)
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

        [HttpGet("GetWbDataEmailPerpormance")]
        public async Task<List<Perpormance>> GetWbDataEmailPerpormance()
        {
            var users = new List<Perpormance>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "Performance");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new Perpormance
                                {
                                    Name = result.GetString(0),
                                    Total = result.GetInt32(1),
                                    Answer = result.GetInt32(2),
                                    AHT = result.GetString(3),
                                    FRT = result.GetString(4)
                                    
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

        [HttpGet("GetWbDataPerformanceMultiChat")]
        public async Task<List<Perpormance>> GetWbDataPerformanceMultiChat()
        {
            var users = new List<Perpormance>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "PerformanceMultiChat");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new Perpormance
                                {
                                    Name = result.GetString(0),
                                    Total = result.GetInt64(1),
                                    //Answer = result.GetInt32(2),
                                    AHT = result.GetString(3),
                                    FRT = result.GetString(4)

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


        [HttpGet("PerformanceSosmed")]
        public async Task<List<Perpormance>> PerformanceSosmed()
        {
            var users = new List<Perpormance>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "PerformanceSosmed");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new Perpormance
                                {
                                    Name = result.GetString(0),
                                    Total = result.GetInt64(1),
                                    Totalx = result.GetInt32(2),
                                    AHT = result.GetString(3),
                                    FRT = result.GetString(4)


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
        [HttpGet("GetWbDataEmailActivites")]
        public async Task<List<Activies>> GetWbDataEmailActivites()
        {
            var users = new List<Activies>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "Activities");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new Activies
                                {
                                    Name = result.GetString(0),
                                    LevelUser = result.GetString(1),
                                    Status = result.GetString(2),
                                    nowHandle = result.GetString(3),
                                    Longers = result.GetString(4)

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

        [HttpGet("GetWbDataActivitiesSosmed")]
        public async Task<List<DtoActivitySite>> GetWbDataActivitiesSosmed()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "ActivitiesSosmed");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivitySite
                                {
                                    Agent = result.GetString(0),
                                    Status = result.GetString(1),
                                    NowHandle = int.Parse(result[2].ToString()),
                                    Chat = result[3].ToString(),
                                    longest = result[4].ToString(),

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

        [HttpGet("ActivitiesSosmedPriok")]
        public async Task<List<ActiviesSosmed>> ActivitiesSosmedPriok()
        {
            var users = new List<ActiviesSosmed>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "ActivitiesSosmedPriok");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new ActiviesSosmed
                                {
                                    Name = result.GetString(0),
                                    Status = result.GetString(1),
                                    nowHandle = result.GetInt32(2),
                                    Facebook = result.GetInt32(3),
                                    Instagram = result.GetInt32(4),
                                    WhatApp = result.GetInt32(5),
                                    Longers = result.GetString(6)

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

        [HttpGet("ActivitiesSosmedSoeta")]
        public async Task<List<ActiviesSosmed>> ActivitiesSosmedSoeta()
        {
            var users = new List<ActiviesSosmed>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "ActivitiesSosmedSoeta");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new ActiviesSosmed
                                {
                                    Name = result.GetString(0),
                                    Status = result.GetString(1),
                                    nowHandle = result.GetInt32(2),
                                    Facebook = result.GetInt32(3),
                                    Instagram = result.GetInt32(4),
                                    WhatApp = result.GetInt32(5),
                                    Longers = result.GetString(6)

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



        [HttpGet("ActivitiesSosmedPasarBaru")]
        public async Task<List<ActiviesSosmed>> ActivitiesSosmedPasarBaru()
        {
            var users = new List<ActiviesSosmed>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "ActivitiesSosmedPasarBaru");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new ActiviesSosmed
                                {
                                    Name = result.GetString(0),
                                    Status = result.GetString(1),
                                    nowHandle = result.GetInt32(2),
                                    Facebook = result.GetInt32(3),
                                    Instagram = result.GetInt32(4),
                                    WhatApp = result.GetInt32(5),
                                    Longers = result.GetString(6)

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

        [HttpGet("GetWbDataActivitiesMultiChat")]
        public async Task<List<ActiviesSosmed>> GetWbDataActivitiesMultiChat()
        {
            var users = new List<ActiviesSosmed>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "ActivitiesMultiChat");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new ActiviesSosmed
                                {
                                    Name = result.GetString(0),
                                    Status = result.GetString(1),
                                    nowHandle = result.GetInt32(2),
                                    Facebook = result.GetInt32(3),
                                    Instagram = result.GetInt32(4),
                                    WhatApp = result.GetInt32(5),
                                    Longers = result.GetString(6)

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


        [HttpGet("GetDailyCallHistory")]
        public async Task<List<dailyHistory>> GetDailyCallHistory()
        {
            var users = new List<dailyHistory>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DailyHistory"; // Your stored procedure or query
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dailyHistory
                                {
                                    Hari = result.GetString(0),
                                    Answer = result.GetDecimal(1),
                                    Abandon = result.GetDecimal(2),
                                    Resolved = result.GetDecimal(3)

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"An error occurred: {sqlEx.Message}");
                return new List<dailyHistory>(); // Or throw
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<dailyHistory>(); // Or throw
            }

            return users;
        }
        [HttpGet("GetWeeklylHistory")]
        public async Task<List<dailyHistory>> GetWeeklylHistory()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var users = new List<dailyHistory>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC WeeklyHistory"; // Your stored procedure or query
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dailyHistory
                                {
                                    Hari = result.GetString(0),
                                    Answer = result.GetDecimal(2),
                                    Abandon = result.GetDecimal(3),
                                    Resolved = result.GetDecimal(4)

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"An error occurred: {sqlEx.Message}");
                return new List<dailyHistory>(); // Or throw
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<dailyHistory>(); // Or throw
            }

            return users;
        }


        [HttpGet("GetmontlylHistory")]
        public async Task<List<dailyHistory>> GetmontlylHistory()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var users = new List<dailyHistory>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC montly5History"; // Your stored procedure or query
                        command.CommandType = CommandType.Text;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dailyHistory
                                {
                                    Hari = result.GetString(2),
                                    Answer = result.GetDecimal(3),
                                    Abandon = result.GetDecimal(4),
                                    Resolved = result.GetDecimal(5)

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"An error occurred: {sqlEx.Message}");
                return new List<dailyHistory>(); // Or throw
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<dailyHistory>(); // Or throw
            }

            return users;
        }

        [HttpGet("TotalEmail")]
        public async Task<List<TotalEmail>> TotalEmail()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var users = new List<TotalEmail>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC WalboardBravo2 @Type"; // Your stored procedure or query
                        command.CommandType = CommandType.Text; 
                        var typeParameter = new SqlParameter("@Type", "TotalEmail");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new TotalEmail
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
                Console.WriteLine($"An error occurred: {sqlEx.Message}");
                //return new List<dailyHistory>(); // Or throw
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                //return new List<dailyHistory>(); // Or throw
            }

            return users;
        }

    }

   


public class dataEmailWB
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string SideId { get; set; }
        public string LoginTime { get; set; }
        public string HandleTime { get; set; }
        public string  Status { get; set; }
    }
    public class Perpormance
    {
        public string Name { get; set; }
        public Int64 Total { get; set; }
        public Int32 Totalx { get; set; }
        public int Answer { get; set; }
        public string AHT { get; set; }
        public string FRT { get; set; }
        //public string  Status { get; set; }
    }
    public class Activies
    {
        public string Name { get; set; }
        public string LevelUser { get; set; }
        public string Status { get; set; }
        public string nowHandle { get; set; }
        public string Longers { get; set; }
        //public string  Status { get; set; }
    }
    public class ActiviesSosmed
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int nowHandle { get; set; }
        public int Facebook { get; set; }
        public int Instagram { get; set; }
        public int WhatApp { get; set; }
        public string Longers { get; set; }
     
    }
    public class datasWb2
    {
        public string Jenis { get; set; }
        public int Jumlah { get; set; }
    }
    public class dailyHistory
    {
        public string Hari { get; set; }
        public Decimal Answer { get; set; }
        public Decimal Abandon { get; set; }
        public Decimal Resolved { get; set; }
        
    }
    public class TotalEmail
    {
        public string Jenis { get; set; }
        public int Jumlah { get; set; }
     

    }
}



public partial class PostDataCustomerWb
{

    public string ContactId { get; set; }
    public string Name { get; set; }
    public string NomorTelpon { get; set; }
    public string Email { get; set; }
    public string Extension { get; set; }
    public string Type { get; set; }
}

public partial class DataCustomerWb
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


