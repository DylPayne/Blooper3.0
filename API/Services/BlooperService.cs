using API.Models;

namespace API.Services
{
    public class BlooperService
    {
        public static string Bloop(string text, List<Blooper> bloopers)
        {
            string message = text;

            // Checking for phrases
            foreach (Blooper blooper in bloopers)
            {
                if (blooper.word.Contains(" "))
                {
                    if (message.ToLower().Contains(blooper.word.ToLower()))
                    {
                        int c = 0;
                        string replacement = "";
                        while (c < blooper.word.Length)
                        {
                            replacement += "*";
                            c++;
                        }
                        message = message.Replace(blooper.word, replacement);
                    }
                }
            }

            // Converting message to array of words
            string[] words = message.Split(" ");

            // Looping through each word in the array
            int i = 0;
            foreach (string word in words)
            {
                int j = 0;
                foreach (Blooper blooper in bloopers)
                {
                    // Converting words to lower case to make sure the words are the same
                    if (blooper.word.ToLower() == word.ToLower())
                    {
                        // Replacing the word with asterisks
                        string asterisks = "";
                        while (j < word.Length)
                        {
                            asterisks += "*";
                            j++;
                        }
                        // Replacing the word in the array with the asterisks
                        words[i] = asterisks;
                    }
                }
                i++;
            }

            return string.Join(" ", words);
        }
    }
}
