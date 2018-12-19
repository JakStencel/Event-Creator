using EventCreator.Business.Report;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EventCreator.FileManager
{
    public class JsonFileManager
    {
        public void SaveFile(string fileName, GeneralReport report)
        {
            if(File.Exists(fileName) || Path.GetExtension(fileName) != ".json")
            {
                Console.WriteLine($"Run into an error. Please ensure that there is no file with given name " +
                                  $"'{fileName}' or the extension of the file was provided");
                return;
            }

            string json = JsonConvert.SerializeObject(report, Formatting.Indented);
            File.WriteAllText(fileName, json);

            Console.WriteLine("File successfully added.");
        }
    }
}
