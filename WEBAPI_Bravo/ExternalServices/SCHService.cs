using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI_Bravo.ExternalServices;
using WebApiBravo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class SCHService : ISCHService
{
    private readonly IConfiguration _configuration;
    private readonly BravoContext _context;

    public SCHService(IConfiguration configuration, BravoContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task PostSCHDataAsync()
    {
        string folderPath = _configuration["ConnectionStrings:PathDataTxt"];



        try
        {

            var dataRows = new List<DataModel>();



            // Skip the first row if it contains headers

            string jsonData = await System.IO.File.ReadAllTextAsync(folderPath);
            // var dataModel = JsonSerializer.Deserialize<DataModel>(jsonData);

           

            var dataModel = ParseData(jsonData);
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(dataModel, jsonOptions);


            var connectionString = _context.Database.GetConnectionString();




            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Insert into SCH_FORECAST_CMS
                using (var command = new SqlCommand("InsertSch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Set parameters for SCH_FORECAST_CMS
                    command.Parameters.AddWithValue("@Date", dataModel.Date);
                    command.Parameters.AddWithValue("@TimeZone", dataModel.TimeZone);
                    command.Parameters.AddWithValue("@SplitSkill", dataModel.SplitSkill);

                    // Execute the insert for SCH_FORECAST_CMS
                    await command.ExecuteNonQueryAsync();
                }

                // Now insert intervals into SCH_FORECAST_CMS_INTERVAL
                foreach (var interval in dataModel.Intervals)
                {
                    using (var intervalCommand = new SqlCommand("InsertIntervalData", connection)) // Assuming a separate procedure for intervals
                    {
                        intervalCommand.CommandType = CommandType.StoredProcedure;

                        // Set parameters for the interval
                        intervalCommand.Parameters.AddWithValue("@TimeStart", interval.TimeStart);
                        intervalCommand.Parameters.AddWithValue("@Separator", interval.Separator);
                        intervalCommand.Parameters.AddWithValue("@TimeEnd", interval.TimeEnd);
                        intervalCommand.Parameters.AddWithValue("@Target", interval.TargetPercentage);
                        intervalCommand.Parameters.AddWithValue("@TargetSeconds", interval.TargetSeconds);
                        intervalCommand.Parameters.AddWithValue("@WithinServiceLevel", interval.PercentWithinServiceLevel);
                        intervalCommand.Parameters.AddWithValue("@Arrivals", interval.Arrivals);
                        intervalCommand.Parameters.AddWithValue("@AbanCalls", interval.AbanCalls);
                        intervalCommand.Parameters.AddWithValue("@FlowOut", interval.FlowOut);
                        intervalCommand.Parameters.AddWithValue("@AvgAbanTime", interval.AvgAbanTime);
                        intervalCommand.Parameters.AddWithValue("@AvgSpeedAns", interval.AvgSpeedAns);
                        intervalCommand.Parameters.AddWithValue("@MaxDelay", interval.MaxDelay);
                        intervalCommand.Parameters.AddWithValue("@AvgHoldTime", interval.AvgHoldTime);
                        intervalCommand.Parameters.AddWithValue("@InterruptDeliveries", interval.InterruptDeliveries);
                        intervalCommand.Parameters.AddWithValue("@Acceptable", interval.Acceptable);

                        // Execute the stored procedure for each interval
                        await intervalCommand.ExecuteNonQueryAsync();
                    }
                }
            }

           
        }
        catch (Exception ex)
        {
            // Log the exception
            //return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public static DataModel ParseData(string inputData)
    {
        var lines = inputData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var header = lines[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        var header2 = lines[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        var header3 = lines[2].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);


        var dataModel = new DataModel
        {
            Date = header[1],
            TimeZone = header2[1],
            SplitSkill = header3[1],
            Intervals = new List<Interval>()
        };

        foreach (var line in lines.Skip(5)) // Skip header lines
        {
            var values = line.Split(new[] { ';' });
            // if (values.Length < 13) continue; // Ensure valid data

            var metric = new Interval
            {
                TimeStart = values[0],
                Separator = values[1],
                TimeEnd = values[2],
                TargetPercentage = values[3],
                TargetSeconds = values[4],
                PercentWithinServiceLevel = values[5],
                Arrivals = values[6],
                AbanCalls = values[7],
                FlowOut = values[8],
                AvgAbanTime = values[9],
                AvgSpeedAns = values[10],
                MaxDelay = values[11],
                AvgHoldTime = values[12],
                InterruptDeliveries = values[13],
                Acceptable = values[14]
            };

            dataModel.Intervals.Add(metric);
        }

        return dataModel;
    }
}