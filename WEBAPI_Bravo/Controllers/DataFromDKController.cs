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
    public class DataFromDKController : ControllerBase
    {
        private readonly BravoContext _context;
        private readonly IConfiguration _configuration;

        public DataFromDKController(BravoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        [HttpGet("DataAgentActivitySoetta")]
        public async Task<List<DtoActivitySite>> DataAgentActivitySoetta()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SiteActivity");
                        var typeParameter2 = new SqlParameter("@Site", "Soekarno Hatta");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivitySite
                                {
                                    Agent = result.GetString(0),
                                    Status = result.GetString(1),
                                    NowHandle = result.GetInt32(2)
                                    //  Chat = result.GetInt32(3),

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

        [HttpGet("DataAgentActivityPriuk")]
        public async Task<List<DtoActivitySite>> DataAgentActivityPriuk()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SiteActivity");
                        var typeParameter2 = new SqlParameter("@Site", "Tanjung Priok");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivitySite
                                {
                                    Agent = result.GetString(result.GetOrdinal("NameAgent")),
                                    Status = result.GetString(result.GetOrdinal("Deskripsi")),
                                    NowHandle = result.IsDBNull(result.GetOrdinal("NowHandle")) ? 0 : result.GetInt32(result.GetOrdinal("NowHandle")),
                                    Chat = result.GetString(result.GetOrdinal("chh_all"))
                               
                                  //  Chat = result.IsDBNull(result.GetInt32(result.GetOrdinal("chh_all")).ToString()) ? "0" : result.GetInt32(result.GetOrdinal("chh_all")).ToString(),
                                   // Chat = result.GetInt32(result.GetOrdinal("chh_all")).ToString()
                                  
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

        [HttpGet("DataAgentActivityPasarBaru")]
        public async Task<List<DtoActivitySite>> DataAgentActivityPasarBaru()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SiteActivity");
                        var typeParameter2 = new SqlParameter("@Site", "Pasar Baru");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivitySite
                                {
                                    Agent = result.GetString(0),
                                    Status = result.GetString(1),
                                    NowHandle = result.GetInt32(2)
                                    //  Chat = result.GetInt32(3),

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


        [HttpGet("DataAgentActivityPusatMultimedia")]
        public async Task<List<DtoActivitySite>> DataAgentActivityPusat()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SiteActivityPusat");
                        var typeParameter2 = new SqlParameter("@Site", "MultiChat");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

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

        [HttpGet("DataAgentActivityPusatSosmed")]
        public async Task<List<DtoActivitySite>> DataAgentActivityPusatSosmed()
        {
            var users = new List<DtoActivitySite>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SiteActivityPusat");
                        var typeParameter2 = new SqlParameter("@Site", "Sosmed");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivitySite
                                {
                                    Agent = result.GetString(0),
                                    Status = result.GetString(1),
                                    NowHandle = result.GetInt32(2)
                                    //  Chat = result.GetInt32(3),

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

        [HttpGet("TotalDataDKTanjungPriuk")]
        public async Task<List<DtoActivityTotal>> TotalDataChat()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "TanjungPriuk");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalFb = result.GetInt64(4),
                                    TotalIg = result.GetInt64(5),
                                    TotalWa = result.GetInt64(6),
                                    TotaLc = result.GetInt64(7),
                                    TotaLInChat = result.GetInt64(8),
                                    avgChatTime = result.GetFieldValue<TimeSpan>(11).ToString(@"hh\:mm\:ss"),
                                    avgConverChatTime = result.GetFieldValue<TimeSpan>(12).ToString(@"hh\:mm\:ss")


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
        [HttpGet("TotalDataDKSoetta")]
        public async Task<List<DtoActivityTotal>> TotalDataDKSoetta()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "Soetta");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalFb = result.GetInt64(4),
                                    TotalIg = result.GetInt64(5),
                                    TotalWa = result.GetInt64(6),
                                    TotaLc = result.GetInt64(7),
                                    TotaLInChat = result.GetInt64(8)


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
        [HttpGet("TotalDataDKPasarBaru")]
        public async Task<List<DtoActivityTotal>> TotalDataDKPasarBaru()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "PasarBaru");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalFb = result.GetInt64(4),
                                    TotalIg = result.GetInt64(5),
                                    TotalWa = result.GetInt64(6),
                                    TotaLc = result.GetInt64(7),
                                    TotaLInChat = result.GetInt64(8)


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
        [HttpGet("TotalDataDKPusat")]
        public async Task<List<DtoActivityTotal>> TotalDataDKPusat()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "Pusat");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            Console.WriteLine(result);
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalOther = result.GetInt64(4),
                                    TotaLInChat = result.GetInt64(5),
                                    TotalFb = result.GetInt64(6),
                                    TotalIg = result.GetInt64(7),
                                    TotalWa = result.GetInt64(8),
                                    TotaLc = result.GetInt64(9),


                                    avgChatTime =  result.GetFieldValue<TimeSpan>(11).ToString(@"hh\:mm\:ss"),
                                    avgConverChatTime =  result.GetFieldValue<TimeSpan>(12).ToString(@"hh\:mm\:ss")

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

        [HttpGet("TotalDataDKPusatPerMonth")]
        public async Task<List<SosmedMonth>> TotalDataDKPusatPerMonth()
        {
            var users = new List<SosmedMonth>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "PusatMonth");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new SosmedMonth
                                {
                                    bulan   = result.GetString(1),
                                    TotalFb = result.GetDecimal(2),
                                    TotalIg = result.GetDecimal(3),
                                    TotalWa = result.GetDecimal(4),
                                    TotaLc = result.GetDecimal(5),
                                    TotaLx = result.GetDecimal(6)


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
        [HttpGet("TotalDataDKAll")]
        public async Task<List<DtoActivityTotal>> TotalDataDKAll()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "TotalDataSite");
                        var typeParameter2 = new SqlParameter("@Site", "All");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalFb = result.GetInt64(4),
                                    TotalIg = result.GetInt64(5),
                                    TotalWa = result.GetInt64(6),
                                    TotaLc = result.GetInt64(7),
                                    TotaLInChat = result.GetInt64(8)


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

        [HttpGet("lineChartDk")]
        public async Task<List<DtoActivityTotal>> lineChartDk()
        {
            var users = new List<DtoActivityTotal>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC DataAgentFromDK @Type,@Site"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "Bulanan");
                        var typeParameter2 = new SqlParameter("@Site", "All");
                        command.Parameters.Add(typeParameter);
                        command.Parameters.Add(typeParameter2);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new DtoActivityTotal
                                {
                                    TotalQueue = result.GetInt64(0),
                                    TotalHandling = result.GetInt64(1),
                                    TotalAbn = result.GetInt64(2),
                                    TotalAns = result.GetInt64(3),
                                    TotalFb = result.GetInt64(4),
                                    TotalIg = result.GetInt64(5),
                                    TotalWa = result.GetInt64(6),
                                    TotaLc = result.GetInt64(7),
                                    TotaLInChat = result.GetInt64(8)


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





    }
}


public class DtoActivitySite
{
    public string Agent { get; set; }
    public string Status { get; set; }
    public int NowHandle { get; set; }
    public string Chat { get; set; }
    public string  longest { get; set; }

}

public class DtoActivityTotal
{
    public Int64 TotalQueue { get; set; }
    public Int64 TotalHandling { get; set; }
    public Int64 TotalAbn { get; set; }
    public Int64 TotalAns { get; set; }
    public Int64 TotalFb { get; set; }
    public Int64 TotalIg { get; set; }
    public Int64 TotalWa { get; set; }
    public Int64 TotaLc { get; set; }
    public Int64 TotaLInChat { get; set; }
    public Int64 TotalOther { get; set; }
    public string avgChatTime { get; set; }
    public string avgConverChatTime { get; set; }


}
public class SosmedMonth
{
    public string  bulan { get; set; }
  
    public Decimal TotalFb { get; set; }
    public Decimal TotalIg { get; set; }
    public Decimal TotalWa { get; set; }
    public Decimal TotaLc { get; set; }
    public Int64 TotaLInChat { get; set; }
    public Decimal TotaLx { get; set; }


}






















