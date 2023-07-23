using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Post(string username)
        {
            string sp = "sp_create_user";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter userameParam = new SqlParameter("@username", username);
            command.Parameters.Add(userameParam);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return CreatedAtAction(nameof(Post), $"Successfully created user {username}!");
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Get(string username)
        {
            string sp = "sp_get_user_by_username";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter userameParam = new SqlParameter("@username", username);
            command.Parameters.Add(userameParam);

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                User user = new User(reader.GetInt32(0), reader.GetString(1));
                connection.Close();
                return Ok(user);
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> GetAll()
        {
            string sp = "sp_get_all_users";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(new User(reader.GetInt32(0), reader.GetString(1)));
                }
                connection.Close();
                return Ok(users);
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Delete(int id)
        {
            string sp = "sp_delete_user";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@id", id);
            command.Parameters.Add(idParam);

            try
            {
                command.ExecuteNonQuery();
                connection.Close();
                return Ok($"Successfully deleted user with id {id}!");
            } catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }
        }
    }
}
