using System.ComponentModel.Design;

class ZooRepository : IMenuable, IListlike
{
    List<IPen> Pens = [];

    public void ListAllItems()
    {
        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Pens.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Pens[i].Describe()}");
        }
    }

    public void AddNewPen<T>(string name) where T : Animal, new()
    {
        Pen<T> pen = new(name);
        Pens.Add(pen);
    }

    public void HandleMenu() {}
    public void HandleSelection() {}

    public void HandleListMenu()
    {
        ListAllItems();
        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select a pen from among the above choices."
        );

        HandleListSelection();
    }

    public void HandleListSelection()
    {
        bool success = ConsoleUtils.GetIntResponse(out int response);

        // If the user has chosen to exit the application, return to the main menu.
        if (!success) return;

        if (response <= Pens.Count + 1)
        {
            // The response is valid, and we will select a pen from the repository.
            Pens[response - 1].HandleMenu();
        }
    }
}