using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using Microsoft.VisualBasic;

class Zoo : IMenuable
{
    readonly ZooRepository Repository = new();

    public void Run()
    {
        Console.WriteLine("\n\n\nWelcome to the Zoo app!");
        HandleMenu();
    }

    public void HandleMenu(int itemID = -1)
    {
        bool finished = false;

        while (!finished)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. List all pens");
            Console.WriteLine("2. Select a pen");
            Console.WriteLine("3. Add a new pen");
            Console.WriteLine("4. Save this zoo");
            Console.WriteLine("5. Load a zoo");
            Console.WriteLine("X. Exit the application");

            finished = HandleSelection();
        }
    }

    public bool HandleSelection(int itemID = -1)
    {
        bool success = ConsoleUtils.GetIntResponse(out int response, true);

        if (!success) return true;

        switch (response)
        {
            case 1:
                Repository.ListAllItems();
                break;

            case 2:
                Repository.HandleListMenu();
                break;

            case 3:
                Repository.AddByUserInput();
                break;

            case 4:
                if (ConsoleUtils.CheckValidity("to save your data."))
                    SaveDataToDisk();
                break;

            case 5:
                if (ConsoleUtils.CheckValidity("to load your data."))
                    LoadDataFromDisk();
                break;

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }

        return false;
    }

    void SaveDataToDisk()
    {
        // Get location to save to
        Console.WriteLine("\nWhat would you like to name your data? Input X to abort.");
        bool success = ConsoleUtils.GetResponse(out string fn, false);

        if (!success) return;

        string location = Path.Combine(Environment.CurrentDirectory, "Data");
        string path = location + $"/{fn}.json";

        if (File.Exists(path) && !ConsoleUtils.CheckValidity("to save your file, but a file already exists here. Overwrite? Y/N"))
            return;

        // Collect data from all pens
        List<object> pens = [.. Repository.GetPenList().Cast<object>()];

        // Save data to .TXT file
        string json = JsonSerializer.Serialize(pens);

        using StreamWriter sw = new(path);
        sw.Write(json);

        return;
    }

    void LoadDataFromDisk()
    {
        string directory = Path.Combine(Environment.CurrentDirectory, "Data");
        string[] files = Directory.GetFiles(directory, "*.json");

        Console.WriteLine("");
        int i = 1; foreach (string file in files)
        {
            Console.WriteLine($"{i++}. {Path.GetFileNameWithoutExtension(file)}");
        }
        Console.WriteLine("Select one of the data files from among the above. Input X to abort.");
        bool success = ConsoleUtils.GetIntResponse(out int res, false);

        if (!success || files[res - 1] == null) return;

        string path = directory + $"/{Path.GetFileNameWithoutExtension(files[res - 1])}.json";
        using StreamReader sr = new(path);

        string json = sr.ReadToEnd();
        using var doc = JsonDocument.Parse(json);

        List<IPen> pens = [];

        i = 1; foreach (var element in doc.RootElement.EnumerateArray())
        {
            try
            {
                string typeName = element.GetProperty("GenericType").GetString() ?? throw new NullReferenceException();
                string penName = element.GetProperty("Name").GetString() ?? throw new NullReferenceException();
                string animalsJson = element.GetProperty("Animals").GetRawText();

                bool factorySuccess = PenFactory.GenerateObject(typeName, out Item? pen, [penName, animalsJson]);
                if (!factorySuccess || pen == null) continue;

                pens.Add((IPen)pen);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Error collecting data on element {i}.");
                continue;
            }
            i++;
        }

        Repository.SetPenList(pens);
    }

}