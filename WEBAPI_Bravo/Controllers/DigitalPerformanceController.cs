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
    public class DigitalPerformanceController : ControllerBase
    {
        private readonly BravoContext _context;

        public DigitalPerformanceController(BravoContext context)
        {
            _context = context;
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









