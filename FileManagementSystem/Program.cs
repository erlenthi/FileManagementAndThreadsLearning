// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading;

Mutex mutex = new Mutex();


Console.WriteLine(Directory.GetCurrentDirectory());

Thread thread1 = new Thread(() => WriteToFile("Thread 1"));
Thread thread2 = new Thread(() => WriteToFile("Thread 2"));
thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

void processFile(String filePath, String scriptFilePath, int methodNumber)
{
    switch (methodNumber)
    {
        case 1:
            processCharacterCounts(filePath, scriptFilePath);
            break;
        case 2:
            processWordCounts(filePath, scriptFilePath);
            break;
        case 3:
            processEncryption(filePath, scriptFilePath);
            break;
        default:
            Debug.Print("error");
            break;
    }
}

void processCharacterCounts(String filePath, String scriptFilePath)
{
    var fileWriter = File.AppendText(filePath);
    //Got the bee movie script from "https://gist.github.com/MattIPv4/045239bc27b16b2bcf7a3a9a4648c08a"
    var scriptReader = File.ReadLines(scriptFilePath);
    //This loop is not the optimal way to count occurences of letters, but the point is to have somewhat time consuming tasks
    int charOccurence = 0;
    for (char alphabetCharacter = 'a'; alphabetCharacter <= 'z'; alphabetCharacter++)
    {
        foreach (var line in scriptReader)
        {
            foreach (var character in line)
                if (line != null)
                {
                    if (alphabetCharacter == character) charOccurence++;
                }
        }
        fileWriter.WriteLine($"Number of occurences of {alphabetCharacter}: {charOccurence}");

        charOccurence = 0;
    }
    fileWriter.Close();
}

void processWordCounts(string filePath, string scriptFilePath)
{
    List<string> wordList = new List<string>();
    List<int> wordListOccurences = new List<int>();

    var fileWriter = File.AppendText(filePath);
    //Got the bee movie script from "https://gist.github.com/MattIPv4/045239bc27b16b2bcf7a3a9a4648c08a"
    var scriptReaderLines = File.ReadLines(scriptFilePath);
    //This loop is not the optimal way to count occurences of words, but the point is to have somewhat time consuming tasks

    var scriptReader = File.ReadAllText(scriptFilePath);

    var scriptReader2 = scriptReader.Trim().Split(' ').CountBy(word => word);

    var wordCounts =
    from word in scriptReader.Trim().Split(' ')
    group word by word into g
    select new { Word = g.Key, Count = g.Count() };

    // foreach (var word in scriptReader.SelectMany(line => line.Split(" ")))
    // {
    //      if (!wordList.Contains(word))
    //         {
    //             wordList.Append(word);
    //             wordListOccurences.Append(1);
    //         }
    //         else wordListOccurences[wordList.IndexOf(word)]++;
    // }
    Console.WriteLine(testList);
    fileWriter.Close();
}

void processEncryption(string filePath, string scriptFilePath)
{
    throw new NotImplementedException("Process Encryption is not yet implemented");
}

void WriteToFile(string threadName)
{
    mutex.WaitOne();

    string path = $"Data\\";

    IEnumerable<string> allFilesInAllFolders = Directory.EnumerateFiles(path, "*.txt", SearchOption.TopDirectoryOnly);

    foreach (var file in allFilesInAllFolders)
    {
        try
        {

            File.AppendAllText("test.txt", $"{threadName} writing...\n");
            processFile(file, path + "scripts" + "\\" + "beemovie.txt", 2);
            File.AppendAllText("test.txt", $"{threadName} finished writing.\n");
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}
