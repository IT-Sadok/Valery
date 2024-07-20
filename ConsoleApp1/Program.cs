// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Project1;
using System.Reflection.Metadata;
using System.Text.Json;

Console.WriteLine("Hello, World!");

bool status;

status = true;

bool isInt32TryParseSuccess;

string type;

int id, quantity;

var list = new List<Item>();

list.Add(new Item(0, "Item1", 3));
list.Add(new Item(1, "Item2", 5));
list.Add(new Item(2, "Item3", 1));

DateTime now = DateTime.Now;

Console.WriteLine("Welcome to Warehouse Manager. Please see the list of all available commands. ");

Console.WriteLine("-l  list of items ");

Console.WriteLine("-a  add item ");

Console.WriteLine("-d  delete item ");

Console.WriteLine("-x  exit ");

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
            Console.WriteLine("Here's the List! Please press any key to proceed...");
            break;
        case ConsoleKey.A:

            Console.WriteLine("");
            Console.WriteLine("Please pass an item Id");
            isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);

            while (!isInt32TryParseSuccess || !Validator.ValidateId(id, list))
            {
                if (!isInt32TryParseSuccess)
                {
                    Console.WriteLine("WARNING: Id should be of integer type.");
                    Console.WriteLine("Please pass a new Id");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);
                }

                if (isInt32TryParseSuccess && !Validator.ValidateId(id, list))
                {
                    Console.WriteLine("WARNING: Id should be unique and greater than 0");
                    Console.WriteLine("Please pass a new Id");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);
                }
            }

            Console.WriteLine("Please pass an item Type");
            type = Console.ReadLine();

            while (!Validator.ValidateType(type))
            {
                Console.WriteLine("Type cannot be null or empty");
                Console.WriteLine("Please pass a new item Type");
                type = Console.ReadLine();
            }

            Console.WriteLine("Please pass an item Quantity");
            isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out quantity);

            while (!isInt32TryParseSuccess || !Validator.ValidateQuantity(quantity))
            {
                if (!isInt32TryParseSuccess)
                {
                    Console.WriteLine("WARNING: Quantity should be of integer type.");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out quantity);
                }
                if (isInt32TryParseSuccess && !Validator.ValidateQuantity(quantity))
                {
                    Console.WriteLine("WARNING: Quantity cannot be <=0");
                    Console.WriteLine("Please pass a new Quantity");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out quantity);
                }
            }

            list.Add(new Item(id, type, quantity));
            Console.WriteLine("Item was succesfully added! Please press any key to proceed...");
            break;
        case ConsoleKey.D:
            Console.WriteLine("");
            Console.WriteLine("Please write Id to remove");
            isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);

            while(!isInt32TryParseSuccess || !Validator.ContainsId(id, list))
            {
                if (!isInt32TryParseSuccess)
                {
                    Console.WriteLine("WARNING: Id should be of integer type.");
                    Console.WriteLine("Please pass a new Id");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);
                }
                if(isInt32TryParseSuccess && !Validator.ContainsId(id, list))
                {
                    Console.WriteLine("Item with Id {0} is not found!", id);
                    Console.WriteLine("WARNING: Id should be existed and be greater than 0.");
                    Console.WriteLine("Please pass a new Id.");
                    isInt32TryParseSuccess = Int32.TryParse(Console.ReadLine(), out id);
                }
            }
           list.Remove(new Item(id, "dummyType", 0));
           Console.WriteLine("Item with Id {0} is Removed! Please press any key to proceed...", id);
            break;
        case ConsoleKey.X:
            Console.WriteLine("");
            Console.WriteLine("Succes! The list was saved: ");
            //list.Last().Quantity = -1;

            list.ForEach(Console.WriteLine);

            string fileName = "Items " + now + "";
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