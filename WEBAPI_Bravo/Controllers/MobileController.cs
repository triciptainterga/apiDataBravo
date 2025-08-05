using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Bravo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MobileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("RegistrationMobile")]
        public IActionResult InsertCustomer([FromBody] CustomerRequest request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("BRA_Customer_Mobile", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string typeValue = (request.Type ?? "").ToLower() switch
                        {
                            "perorangan" => "1",
                            "perusahaan" => "2",
                            "pemerintah" => "3",
                            _ => "1"
                        };

                        cmd.Parameters.AddWithValue("@TrxName", request.Nama ?? "");
                        cmd.Parameters.AddWithValue("@TrxCusTomerPerusahaan", request.NamaPerusahaan ?? "");
                        cmd.Parameters.AddWithValue("@TrxNPWP", request.NPWP ?? "");
                        cmd.Parameters.AddWithValue("@TrxEmail", request.Email ?? "");
                        cmd.Parameters.AddWithValue("@TrxPhone", request.NoTelpn ?? "");
                        cmd.Parameters.AddWithValue("@TrxCusTomerType", typeValue);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["ResultNya"].ToString();
                                string customerId = reader["CustomerID"].ToString();

                                return Ok(new
                                {
                                    status = "success",
                                    message = result,
                                    customerId = customerId
                                });
                            }
                            else
                            {
                                return BadRequest(new
                                {
                                    status = "error",
                                    message = "No data returned from stored procedure."
                                });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost("Voip")]
        public IActionResult InsertCustomerVoip([FromBody] CustomerPopUpRequest request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("BRA_Customer_Voip", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string typeValue = (request.Type ?? "").ToLower() switch
                        {
                            "perorangan" => "1",
                            "perusahaan" => "2",
                            "pemerintah" => "3",
                            _ => "1"
                        };

                        cmd.Parameters.AddWithValue("@TrxName", request.Nama ?? "");
                        cmd.Parameters.AddWithValue("@TrxCusTomerPerusahaan", request.NamaPerusahaan ?? "");
                        cmd.Parameters.AddWithValue("@TrxNPWP", request.NPWP ?? "");
                        cmd.Parameters.AddWithValue("@TrxEmail", request.Email ?? "");
                        cmd.Parameters.AddWithValue("@TrxPhone", request.NoTelpn ?? "");
                        cmd.Parameters.AddWithValue("@TrxVoip", request.Voip ?? "");
                        cmd.Parameters.AddWithValue("@TrxCusTomerType", typeValue);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["ResultNya"].ToString();
                                string customerId = reader["CustomerID"].ToString();

                                return Ok(new
                                {
                                    status = "success",
                                    message = result,
                                    customerId = customerId
                                });
                            }
                            else
                            {
                                return BadRequest(new
                                {
                                    status = "error",
                                    message = "No data returned from stored procedure."
                                });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
   
}

  

    public class CustomerRequest
    {
        public string Nama { get; set; }
        public string NamaPerusahaan { get; set; }
        public string NPWP { get; set; }
        public string Email { get; set; }
        public string NoTelpn { get; set; }
        public string Type { get; set; }
    }
public class CustomerPopUpRequest
{
    public string Nama { get; set; }
    public string NamaPerusahaan { get; set; }
    public string NPWP { get; set; }
    public string Email { get; set; }
    public string NoTelpn { get; set; }
    public string Voip { get; set; }
    public string Type { get; set; }
}


