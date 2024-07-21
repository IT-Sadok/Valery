using AsincFileParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


int numberOfFiles = 10;
int maxConcurrency = 3;
string directoryPath = "RandomTextFiles";
Directory.CreateDirectory(directoryPath);

List<string> filePaths = new List<string>();

for (int i = 0; i < numberOfFiles; i++)
{
    filePaths.Add(Path.Combine(directoryPath, $"file{i + 1}.txt"));
}

SemaphoreSlim semaphore = new SemaphoreSlim(maxConcurrency);
List<Task> tasks = new List<Task>();

foreach (var filePath in filePaths)
{
    tasks.Add(CreateRandomTextFile.CreateRandomTextFileAsync(filePath, semaphore));
}

await Task.WhenAll(tasks);
Console.WriteLine("All files created.");
