// See https://aka.ms/new-console-template for more information
using Project1;
using System.Reflection.Metadata;
using System.Text.Json;

Console.WriteLine("Hello, World!");

bool status;

status = true;

var list = new List<Item>();

list.Add(new Item(0, "Item1", 3));
list.Add(new Item(1, "Item2", 5));
list.Add(new Item(2, "Item3", 1));

Console.WriteLine("Welcome to Warehouse Manager. Please see the list of all available commands. ");

Console.WriteLine("-l || list of items ");

Console.WriteLine("-a || add item ");

Console.WriteLine("-d || delete item ");

Console.WriteLine("-x || exit ");

Console.WriteLine("Press any key to proceed...");

if (!list.Any())
{
    Console.WriteLine("Please press -a to add your first item!");
}

while (status)
{
    var input = Console.ReadKey();
    switch (input.Key)
    {
        case ConsoleKey.L:
            Console.WriteLine("");
            list.ForEach(Console.WriteLine);
            break;
        case ConsoleKey.A:
            Console.WriteLine("");
            Console.WriteLine("Please pass an item Id");
            Int32.TryParse(Console.ReadLine(), out int id);
            while (id <= 0 || list.Contains(new Item(id, "dummyType", 1)))
            {
                if (id < 0)
                {
                    Console.WriteLine("Id cannot be <0");
                    Console.WriteLine("Please pass a new Id");
                    Int32.TryParse(Console.ReadLine(), out id);
                }
                if (list.Contains(new Item(id, "dummyType", 1)))
                {
                    Console.WriteLine("Id is not unique");
                    Console.WriteLine("Please pass a new Id");
                    Int32.TryParse(Console.ReadLine(), out id);
                }
            }
            Console.WriteLine("Please pass an item Type");
            string type = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Type cannot be null or empty");
                Console.WriteLine("Please pass a new item Type");
                type = Console.ReadLine();
            }

            Console.WriteLine("Please pass an item Quantity");
            Int32.TryParse(Console.ReadLine(), out int quantity);
            while (quantity <= 0)
            {
                    Console.WriteLine("Quantity cannot be <0");
                    Console.WriteLine("Please pass a new Quantity");
                    Int32.TryParse(Console.ReadLine(), out quantity);
            }
            list.Add(new Item(id, type, quantity));
            Console.WriteLine("Item was succesfully added! Please press any key to proceed...");
            break;
        case ConsoleKey.D:
            Console.WriteLine("");
            Console.WriteLine("Please write Id to remove");

            if (int.TryParse(Console.ReadLine(), out int Id))
            {
                if (list.Remove(new Item(Id, "dummyType", 0)))
                {
                    Console.WriteLine("Item with Id {0} is Removed! Please press any key to proceed...", Id);
                }
                else
                {
                    Console.WriteLine("Item with Id {0} is not found. Please press any key to proceed...", Id);
                }
            }
            break;
        case ConsoleKey.X:
            Console.WriteLine("");
            Console.WriteLine("Succes! The list was saved: ");

            list.ForEach(Console.WriteLine);

            string fileName = "Items.json";
            string jsonString = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName));

            status = false;

            break;
        default:
            Console.WriteLine();
            Console.WriteLine("No such command {0}", input.Key);
            break;
    }
}