using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Database=AttendanceDB;Trusted_Connection=True;TrustServerCertificate=True;";

        [HttpPost]
        public IActionResult PostAttendance([FromBody] Attendance attendance)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Attendance (MatricNumber, Name, Course) VALUES (@MatricNumber, @Name, @Course)", conn))
                    {
                        cmd.Parameters.AddWithValue("@MatricNumber", attendance.MatricNumber);
                        cmd.Parameters.AddWithValue("@Name", attendance.Name);
                        cmd.Parameters.AddWithValue("@Course", attendance.Course);
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok("Attendance recorded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAttendance()
        {
            List<Attendance> attendanceList = new List<Attendance>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Attendance ORDER BY AttendanceTime DESC", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                attendanceList.Add(new Attendance
                                {
                                    Id = reader.GetInt32(0),
                                    MatricNumber = reader.GetString(1),
                                    Name = reader.GetString(2),
                                    Course = reader.GetString(3),
                                    AttendanceTime = reader.GetDateTime(4)
                                });
                            }
                        }
                    }
                }
                return Ok(attendanceList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}