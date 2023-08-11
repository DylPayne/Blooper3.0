using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    //[Route("post")]
    public class MessageController : ControllerBase
    {
        [HttpPost("message")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Get(string text)
        {
            List<Blooper> bloopers = new List<Blooper>();
            string message = text;

            string sp = "sp_get_all_bloopers";
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=blooper;Integrated Security=True;TrustServerCertificate=true");
            connection.Open();

            SqlCommand command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Blooper blooper = new Blooper(reader.GetInt32(0), reader.GetString(1));
                    bloopers.Add(blooper);
                }
            }
            catch (SqlException e)
            {
                connection.Close();
                return BadRequest(e.Message);
            }

            //// Checking for phrases
            //foreach (Blooper blooper in bloopers)
            //{
            //    if (blooper.word.Contains(" "))
            //    {
            //        if (text.ToLower().Contains(blooper.word.ToLower()))
            //        {
            //            int c = 0;
            //            string replacement = "";
            //            while (c < blooper.word.Length)
            //            {
            //                replacement += "*";
            //                c++;
            //            }
            //            message = message.Replace(blooper.word, replacement);
            //        }
            //    }
            //}

            //// Converting message to array of words
            //string[] words = message.Split(" ");

            //// Looping through each word in the array
            //int i = 0;
            //foreach (string word in words)
            //{
            //    int j = 0;
            //    foreach (Blooper blooper in bloopers)
            //    {
            //        // Converting words to lower case to make sure the words are the same
            //        if (blooper.word.ToLower() == word.ToLower())
            //        {
            //            // Replacing the word with asterisks
            //            string asterisks = "";
            //            while (j < word.Length)
            //            {
            //                asterisks += "*";
            //                j++;
            //            }
            //            // Replacing the word in the array with the asterisks
            //            words[i] = asterisks;
            //        }
            //    }
            //    i++;
            //}

            string bloopedMessage = BlooperService.Bloop(message, bloopers);

            return Ok(JsonConvert.SerializeObject(bloopedMessage));
        }
    }
}
