using System.Net;
using System.Runtime.InteropServices;

class Pen<T>(string name) : IPen where T : Animal, new()
{
    string Name = name;
    List<T> Animals { get; set; } = new();

    public string Describe()
    {
        return $"{Name}, containing {Animals.Count} animals of type {typeof(T).Name}";
    }

    public void Add(T animal)
    {
        Animals.Add(animal);
    }

    public void Remove(string name)
    {
        foreach (T animal in Animals)
        {
            if (animal.Name == name)
            {
                Animals.Remove(animal);
            }
        }
    }

    public void HandleMenu() {}
    public void HandleSelection() {}

    public void ListAllItems()
    {
        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Animals[i].Name}, {Animals[i].Age} years old.");
        }
    }

    public void HandleListMenu()
    {
        ListAllItems();

        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select an animal from among the above choices."
        );

        HandleListSelection();
    }

    public void HandleListSelection()
    {
        bool success = ConsoleUtils.GetIntResponse(out int response);

        // If the user has chosen to exit the application, return to the main menu.
        if (!success) return;

        if (response <= Animals.Count + 1)
        {
            // The response is valid, and we will select a pen from the repository.
            Animals[response - 1].HandleMenu();
        }
    }
}