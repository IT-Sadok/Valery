namespace AsincFileParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public static class CreateRandomTextFile
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static async Task CreateRandomTextFileAsync(string filePath, SemaphoreSlim semaphore, CancellationToken token)
        {

            if (token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();

            await semaphore.WaitAsync();
            try
            {
                await Task.Delay(3000);
                string content = GenerateRandomText(100);
                await File.WriteAllTextAsync(filePath, content);
                Console.WriteLine($"File created: {filePath}");
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("The task is aborted");
                    else
                        Console.WriteLine(e.Message);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static string GenerateRandomText(int length)
        {
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
