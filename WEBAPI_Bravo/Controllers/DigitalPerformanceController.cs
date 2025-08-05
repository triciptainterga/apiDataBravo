using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
    public class DigitalPerformanceController : ControllerBase
    {
        private readonly BravoContext _context;

        public DigitalPerformanceController(BravoContext context)
        {
            _context = context;
        }



        [HttpGet("GetDataCallPerpormance")]
        public IActionResult ProcessCallData()
        {

            //string filePath = "C:/WalboardBC/main-dark/PHP/CallPerpormances_Count.txt";
            string filePath = @"\\10.216.206.3\Acsauto\CallPerpormances_Count.txt";
            try
            {
                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found at the specified path.");
                }

                // Read the file data
                var callDataList = ReadCallData(filePath);

                // Calculate totals
                var totalData = CalculateTotal(callDataList);

                // Return the total data as JSON response
                return Ok(totalData);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound($"File not found: {ex.Message}");
            }
            catch (FormatException ex)
            {
                return BadRequest($"Data format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Helper method to read the file and parse the data
        private List<CallData> ReadCallData(string path)
        {
            var dataList = new List<CallData>();

           
                
                var lines = System.IO.File.ReadAllLines(path);
                var dataLines = lines.Skip(2).Where(line => !string.IsNullOrEmpty(line));
            // var agentActivities = new List<AgentActivity>();
            foreach (var line in dataLines)
            {
                var columns = line.Split(';');

                if (columns.Length < 28) continue;  // Skip malformed rows

                try
                {
                    var callData = new CallData
                    {
                        SkillState = columns[1],
                        AgentsStaffed = int.TryParse(columns[2], out int agentsStaffed) ? agentsStaffed : 0, // Default to 0 if invalid
                        AvgAbanTime = ParseTime(columns[3]),  // TimeSpan parsing
                        AbanCalls = int.TryParse(columns[4], out int abanCalls) ? abanCalls : 0,  // Default to 0 if invalid
                        AvgACDTime = ParseTime(columns[5]),  // TimeSpan parsing
                        ACDCalls = int.TryParse(columns[6], out int acdCalls) ? acdCalls : 0,  // Default to 0 if invalid
                        AvgSpeedAns = double.TryParse(columns[7], out double avgSpeedAns) ? avgSpeedAns : 0,  // Default to 0 if invalid
                        TotalIACDACW = double.TryParse(columns[8], out double totalIACDACW) ? totalIACDACW : 0,  // Default to 0 if invalid
                        AnsCallsPercentage = double.TryParse(columns[9], out double ansCallsPercentage) ? ansCallsPercentage : 0,  // Default to 0 if invalid
                        AbanCallsPercentage = double.TryParse(columns[10], out double abanCallsPercentage) ? abanCallsPercentage : 0,  // Default to 0 if invalid
                        ACDTimePercentage = double.TryParse(columns[11], out double acdTimePercentage) ? acdTimePercentage : 0,  // Default to 0 if invalid
                        AUXIncalls = int.TryParse(columns[12], out int auxIncalls) ? auxIncalls : 0,  // Default to 0 if invalid
                        AgentsAvail = int.TryParse(columns[13], out int agentsAvail) ? agentsAvail : 0,  // Default to 0 if invalid
                        ACCEPTABLE = double.TryParse(columns[14], out double acceptable) ? acceptable : 0,  // Default to 0 if invalid
                        RingTime = double.TryParse(columns[15], out double ringTime) ? ringTime : 0,  // Default to 0 if invalid
                        CallsWaiting = int.TryParse(columns[16], out int callsWaiting) ? callsWaiting : 0,  // Default to 0 if invalid
                        AcceptPercentage = double.TryParse(columns[17], out double acceptPercentage) ? acceptPercentage : 0,  // Default to 0 if invalid
                        AvgACWTime = double.TryParse(columns[18], out double avgACWTime) ? avgACWTime : 0,  // Default to 0 if invalid
                        CallsOffered = int.TryParse(columns[19], out int callsOffered) ? callsOffered : 0,  // Default to 0 if invalid
                        ACWIncalls = int.TryParse(columns[20], out int acwIncalls) ? acwIncalls : 0,  // Default to 0 if invalid
                        AgentsInAUX = int.TryParse(columns[21], out int agentsInAUX) ? agentsInAUX : 0,  // Default to 0 if invalid
                        AgentsOnACDCalls = int.TryParse(columns[22], out int agentsOnACDCalls) ? agentsOnACDCalls : 0,  // Default to 0 if invalid
                        InQueue = int.TryParse(columns[23], out int inQueue) ? inQueue : 0,  // Default to 0 if invalid
                        OldestCallWaiting = int.TryParse(columns[24], out int oldestCallWaiting) ? oldestCallWaiting : 0,  // Default to 0 if invalid
                        ACDTime = ParseTime(columns[25]),  // TimeSpan parsing
                        ACWTime = ParseTime(columns[26]),  // TimeSpan parsing
                        AvgACDTime2 = ParseTime(columns[27])  // TimeSpan parsing
                    };

                    dataList.Add(callData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing line: {line}. Exception: {ex.Message}");
                }
            }
           

            return dataList;
        }


        public static TimeSpan ParseTime(string timeStr)
        {
            try
            {
                // Jika waktu berupa pecahan jam (dimulai dengan titik)
                if (timeStr.StartsWith("."))
                {
                    // Mengubah pecahan jam menjadi menit
                    double fractionalHours = double.Parse(timeStr);
                    double minutes = fractionalHours * 60;  // Konversi ke menit
                    return TimeSpan.FromMinutes(minutes);   // Mengembalikan TimeSpan dalam bentuk menit
                }
                else
                {
                    // Jika format standar (hh:mm:ss)
                    return TimeSpan.Parse(timeStr);
                }
            }
            catch (Exception ex)
            {
                // Menangani kesalahan jika format tidak valid
                Console.WriteLine($"Error parsing time: {ex.Message}");
                return TimeSpan.Zero; // Mengembalikan TimeSpan kosong jika terjadi kesalahan
            }
        }
        // Helper method to calculate the total values
        private CallData CalculateTotal(List<CallData> dataList)
        {
            var totalData = new CallData
            {
                SkillState = "TOTAL",
                AgentsStaffed = dataList.Sum(d => d.AgentsStaffed),
             //   AvgAbanTime = dataList.Sum(d => d.AvgAbanTime),
                AbanCalls = dataList.Sum(d => d.AbanCalls),
                //AvgACDTime = dataList.Sum(d => d.AvgACDTime),
                ACDCalls = dataList.Sum(d => d.ACDCalls),
                AvgSpeedAns = dataList.Sum(d => d.AvgSpeedAns),
                TotalIACDACW = dataList.Sum(d => d.TotalIACDACW),
                AnsCallsPercentage = dataList.Sum(d => d.AnsCallsPercentage),
                AbanCallsPercentage = dataList.Sum(d => d.AbanCallsPercentage),
                ACDTimePercentage = dataList.Sum(d => d.ACDTimePercentage),
                AUXIncalls = dataList.Sum(d => d.AUXIncalls),
                AgentsAvail = dataList.Sum(d => d.AgentsAvail),
                ACCEPTABLE = dataList.Sum(d => d.ACCEPTABLE),
                //RingTime = dataList.Sum(d => d.RingTime),
                CallsWaiting = dataList.Sum(d => d.CallsWaiting),
                AcceptPercentage = dataList.Sum(d => d.AcceptPercentage),
               // AvgACWTime = dataList.Sum(d => d.AvgACWTime),
                CallsOffered = dataList.Sum(d => d.CallsOffered),
                ACWIncalls = dataList.Sum(d => d.ACWIncalls),
                AgentsInAUX = dataList.Sum(d => d.AgentsInAUX),
                AgentsOnACDCalls = dataList.Sum(d => d.AgentsOnACDCalls),
                InQueue = dataList.Sum(d => d.InQueue),
                OldestCallWaiting = dataList.Sum(d => d.OldestCallWaiting),
              //  ACDTime = dataList.Sum(d => d.ACDTime),
              //  ACWTime = dataList.Sum(d => d.ACWTime),
              //  AvgACDTime2 = dataList.Sum(d => d.AvgACDTime)
            };

            return totalData;
        }

        [HttpGet("GetDataDigitalEmail")]
        public async Task<List<datas2>> GetDataDigitalEmail()
        {
            var users = new List<datas2>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "DigitalEmail");
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


        [HttpGet("GetDataDigitalLc")]
        public async Task<List<datas2>> GetDataDigitalLc()
        {
            var users = new List<datas2>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "DigitalLc");
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

        [HttpGet("GetDataDigitalSosmedTrend")]
        public async Task<List<datas2>> GetDataDigitalSosmedTrend()
        {
            var users = new List<datas2>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "SosmedTrend");
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

        [HttpGet("GetDataDigitalSosmed")]
        public async Task<List<dataswwek>> TotalVolumeSitePerweek()
        {
            var users = new List<dataswwek>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "EXEC GetDataEmailWb @Type"; // Use parameter directly in command text
                        command.CommandType = CommandType.Text;
                        var typeParameter = new SqlParameter("@Type", "Sosmed");
                        command.Parameters.Add(typeParameter);

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                users.Add(new dataswwek
                                {
                                    Name = result.GetString(0),
                                    Hari = result.GetString(1),
                                    Jumlah = result.GetInt32(2)
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









