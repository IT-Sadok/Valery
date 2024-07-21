namespace AsincFileParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public static class CreateRandomTextFile
    {
        public static async Task CreateRandomTextFileAsync(string filePath, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                await Task.Delay(3000);
                string content = GenerateRandomText(100);
                await File.WriteAllTextAsync(filePath, content);
                Console.WriteLine($"File created: {filePath}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}
