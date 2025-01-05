using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace WordleProject
{
    public class FileDownloader
    {
        private readonly string fileName = "words.txt";
        private readonly string filePath;

        //Assigning where path is stored (Generated with the assistance of chatGpt)
        public FileDownloader()
        {
            filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        }

        // Check if the file already exists, otherwise download it
        public async Task CheckFileExists()
        {
            if (!File.Exists(filePath))
            {
                await DownloadFileAsync();
            }
        }

        // Download the file from the URL and save it to the local file system
        private async Task DownloadFileAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Get the file content from the URL
                    var fileContent = await httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");

                    // Save the file content to the local file system
                    await File.WriteAllTextAsync(filePath, fileContent);
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the download process
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }
        }

        // Read the words from the downloaded file
        public async Task<string[]> GetWordsAsync()
        {
            await CheckFileExists();
            return await File.ReadAllLinesAsync(filePath);
        }

        // Get a random word from the file
        public async Task<string> GetRandomWordAsync()
        {
            var words = await GetWordsAsync();
            var random = new Random();
            return words[random.Next(words.Length)].Trim(); // Select a random word
        }
    }
}
