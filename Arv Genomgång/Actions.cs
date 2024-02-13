using Arv_Genomgång.Items;

namespace Arv_Genomgång
{
    static public class Actions
    {
        static public void MainMenu() {
            FarmManager.currentCommand = null;
            WriteStack stack = new(new List<WriteStackItem>
            {
                new("--- Farm Actions --- ", ConsoleColor.Yellow, false),
                new($"[${FarmManager.Cash}]\n", ConsoleColor.Green, false),
                new("* Animals\n", ConsoleColor.DarkCyan, false),
                new("* Inventory\n", ConsoleColor.DarkCyan, false),
                new("* Market\n", ConsoleColor.DarkCyan, false)
            });
            stack.Draw(false);
        }
            
        static public void Animals()
        {
            FarmManager.currentCommand = "animals";
            FarmManager.previousCommand = null;
            Console.Clear();

            WriteStack stack = new(new List<WriteStackItem>
            {
                new("--- Animals ---\n", ConsoleColor.Yellow, false)
            });

            foreach (KeyValuePair<string, List<Animal>> pair in FarmManager.Animals) {
                string key = Utility.FirstLetterUppercase(pair.Key);
                stack.Items.Add(new($"* {key}\n", ConsoleColor.DarkCyan, false));
            }

            stack.Draw();
        }

        static public void Inventory()
        {
            FarmManager.currentCommand = "inventory";
            FarmManager.previousCommand = null;
            Console.Clear();

            WriteStack stack = new(new List<WriteStackItem>
            {
                new("--- Inventory ---\n", ConsoleColor.Yellow, false)
            });

            foreach (KeyValuePair<string, List<Item>> item in FarmManager.Inventory) {
                stack.Items.Add(new($"[{Utility.FirstLetterUppercase(item.Key)}]: {item.Value.Count}\n", ConsoleColor.Green, false));
            }

            stack.Draw();
        }

        static public void Market()
        {
            FarmManager.currentCommand = "market";
            FarmManager.previousCommand = null;

            Dictionary<string, int> shelf = new() {
                {"chicken", 35},
                {"cow", 175},
                {"tiger", 775}
            };

            WriteStack stack = new(new List<WriteStackItem>{
                new("--- Market ---\n", ConsoleColor.Yellow, false),
            });

            foreach(KeyValuePair<string, int> pair in shelf) {
                string key = Utility.FirstLetterUppercase(pair.Key);
                stack.Items.Add(new($"* {key}", ConsoleColor.DarkCyan, false));
                stack.Items.Add(new($" [${pair.Value}]\n", ConsoleColor.Green, false));
            }

            stack.Draw();

            string? input = Console.ReadLine();
            if (input == null) return;

            FarmManager.previousCommand = "market";

            stack.Items.Clear();
            input = input.ToLower();
            if (!shelf.ContainsKey(input)) return;
            if (FarmManager.Cash < shelf[input]) {
                stack.Items.Clear();
                stack.Items.Add(new("You broke XD\n", ConsoleColor.Red, true));
                stack.Items.Add(new("Press any key to continue..\n", ConsoleColor.Gray, false));
                stack.Draw();
                Console.ReadKey();
                MainMenu();
                return;
            }

            string itemName = Utility.FirstLetterUppercase(input);
            int itemPrice = shelf[input];

            stack.Items.Add(new("Are you sure you want to buy ", ConsoleColor.DarkYellow, true));
            stack.Items.Add(new($"[{itemName}] ", ConsoleColor.DarkCyan, true));
            stack.Items.Add(new("for ", ConsoleColor.DarkYellow, true));
            stack.Items.Add(new($"${itemPrice}", ConsoleColor.Green, true));
            stack.Items.Add(new("? ", ConsoleColor.DarkYellow, true));
            stack.Items.Add(new("(y/n)\n", ConsoleColor.DarkCyan, true));

            stack.Draw(false);

            input = Console.ReadLine();
            if (input == null || input == "n") {
                MainMenu();
                return;
            }

            stack.Items.Clear();
            stack.Items.Add(new("Successfully bought ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"[{itemName}] ", ConsoleColor.DarkCyan, true));
            stack.Items.Add(new("for ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"${itemPrice}\n", ConsoleColor.Green, true));
            stack.Items.Add(new("Press any key to continue..\n", ConsoleColor.Gray, false));
            stack.Draw(false);

            Console.ReadKey();
            MainMenu();
        }

        static public void DisplayAnimal(string key)
        {
            bool success = FarmManager.Animals.TryGetValue(key, out List<Animal>? animals);
            if (!success) return;

            FarmManager.currentCommand = "displayAnimal";
            FarmManager.previousCommand = "animals";

            key = Utility.FirstLetterUppercase(key);

            WriteStack stack = new(new List<WriteStackItem>
            {
                new($"--- {key}s ---\n", ConsoleColor.Yellow, false)
            });

            foreach(Animal animal in animals) {
                stack.Items.Add(new($"{animal}\n", ConsoleColor.Green, false));
            }

            stack.Draw();
        }

        static public void DisplayItem(string key)
        {
            bool success = FarmManager.Inventory.TryGetValue(key, out List<Item>? items);
            if (!success) return;

            FarmManager.currentCommand = "displayItem";
            FarmManager.previousCommand = "inventory";
            FarmManager.currentItem = (key, items);

            key = Utility.FirstLetterUppercase(key);

            WriteStack stack = new(new List<WriteStackItem>
            {
                new($"--- {key} ", ConsoleColor.Yellow, false),
                new($"[{items.Count}]", ConsoleColor.Green, false),
                new($" ---\n", ConsoleColor.Yellow, false),

                new($"* Sell\n", ConsoleColor.DarkCyan, false)
            });

            stack.Draw();
        }

        static public void ItemAction(string action) {
            action = action.ToLower();

            string itemName = FarmManager.currentItem.Value.Item1;
            List<Item> item = FarmManager.currentItem.Value.Item2;
            int count = item.Count;

            int sum = 0;
            foreach (Item Item in FarmManager.currentItem.Value.Item2) {
                sum += Item.Value;
            }

            WriteStack stack = new(new List<WriteStackItem>());
            
            if (action == "sell") {
                stack.Items.Add(new("Are you sure you want to sell ", ConsoleColor.DarkYellow, true));
                stack.Items.Add(new($"{count} {itemName}s ", ConsoleColor.White, true));
                stack.Items.Add(new("for ", ConsoleColor.DarkYellow, true));
                stack.Items.Add(new($"${sum}", ConsoleColor.Green, true));
                stack.Items.Add(new("?", ConsoleColor.DarkYellow, true));
                stack.Items.Add(new("(y/n)\n", ConsoleColor.DarkCyan, true));
            } else {
                return;
            }
            stack.Draw(false);
            stack.Items.Clear();

            string? input = Console.ReadLine();
            if (input == null || input == "n") {
                MainMenu();
                return;
            }

            if (input == "y") {
                FarmManager.Cash += sum;
                FarmManager.currentItem.Value.Item2.Clear();
                stack.Items.Add(new($"Sold ", ConsoleColor.DarkGreen, true));
                stack.Items.Add(new($"{count} ", ConsoleColor.Gray, true));
                stack.Items.Add(new($"{itemName}s ", ConsoleColor.White, true));
                stack.Items.Add(new($"for ", ConsoleColor.DarkGreen, true));
                stack.Items.Add(new($"${sum}\n", ConsoleColor.Green, true));
            }

            stack.Items.Add(new("Press any key to continue..\n", ConsoleColor.Gray, false));

            stack.Draw(false);
            Console.ReadKey();
            MainMenu();
        }

    }
}
