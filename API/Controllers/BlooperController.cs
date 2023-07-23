using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("blooper")]
    public class BlooperController : ControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Blooper> Post(string word)
        {
            string sp = "sp_create_blooper";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter nameParam = new SqlParameter("@word", word);
            command.Parameters.Add(nameParam);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return CreatedAtAction(nameof(Post), $"Successfully created blooper {word}!");
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Blooper> Get()
        {
            Console.WriteLine("Getting all bloopers");
            string sp = "sp_get_all_bloopers";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<Blooper> bloopers = new List<Blooper>();
                while (reader.Read())
                {
                    Blooper blooper = new Blooper(reader.GetInt32(0), reader.GetString(1));
                    bloopers.Add(blooper);
                }
                connection.Close();
                return Ok(bloopers);
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }
    }
}
