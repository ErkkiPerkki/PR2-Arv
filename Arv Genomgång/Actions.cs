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
                stack.Items.Add(new($"* {key} ", ConsoleColor.DarkCyan, false));
                stack.Items.Add(new($"[{pair.Value.Count}]\n", ConsoleColor.Blue, false));
            }

            stack.Draw();
        }

        static public void Inventory()
        {
            FarmManager.currentCommand = "inventory";
            FarmManager.previousCommand = null;

            WriteStack stack = new(new List<WriteStackItem>
            {
                new("--- Inventory ---\n", ConsoleColor.Yellow, false)
            });

            foreach (KeyValuePair<string, List<Item>> item in FarmManager.Inventory) {
                stack.Items.Add(new($" {Utility.FirstLetterUppercase(item.Key)} ", ConsoleColor.Green, false));
                stack.Items.Add(new($"[{item.Value.Count}]\n", ConsoleColor.Blue, false));
            }

            if (FarmManager.Inventory.Count == 0) {
                stack.Items.Add(new("[Your inventory is empty]\n", ConsoleColor.DarkGray, false));
            }
            else {
                stack.Items.Add(new(" * Sell all\n", ConsoleColor.DarkCyan, false));
            }

            stack.Draw();
        }

        static public void Market()
        {
            FarmManager.currentCommand = "market";
            FarmManager.previousCommand = null;

            Dictionary<string, int> shelf = new() {
                {"pissmyra", 15},
                {"chicken", 35},
                {"cow", 175},
                {"sengångare", 450},
                {"tiger", 775}
                
            };

            WriteStack stack = new(new List<WriteStackItem>{
                new("--- Market --- ", ConsoleColor.Yellow, false),
                 new($"[${FarmManager.Cash}]\n", ConsoleColor.Green, false),
            });

            foreach(KeyValuePair<string, int> pair in shelf) {
                string key = Utility.FirstLetterUppercase(pair.Key);
                stack.Items.Add(new($"* {key}", ConsoleColor.DarkCyan, false));
                stack.Items.Add(new($" [${pair.Value}]\n", ConsoleColor.Green, false));
            }

            stack.Draw();

            string? input = Console.ReadLine();
            if (input == null || input == string.Empty) {
                Market();
                return;
            }
            if (input == "back" || input == "exit") {
                MainMenu();
                return;
            }

            input = input.ToLower();
            string[] args = input.Split(' ');
            if (args.Length > 2) {
                Market();
                return;
            }

            string item = args[0];
            string count = args.Length > 1 ? args[1] : "1";

            if (!shelf.ContainsKey(item)) {
                Market();
                return;
            }

            int price = shelf[item];
            string name = Utility.FirstLetterUppercase(item);

            if (count.ToLower() == "max") {
                count = ((int)MathF.Floor(FarmManager.Cash / price)).ToString();
            }

            bool success = int.TryParse(count, out int realCount);
            if (!success) {
                Market();
                return;
            }
            int totalPrice = realCount * price;

            if (!shelf.ContainsKey(item)) return;

            // Handles the case where you don't have enough money to buy given product
            if (FarmManager.Cash < totalPrice) {
                stack.Items.Clear();
                stack.Items.Add(new("You broke XD\n", ConsoleColor.DarkRed, true));
                stack.Draw();

                Utility.WaitForInput();
                Market();
                return;
            }

            // Makes the user confirm their purchase by typing 'y'
            // Any other characters than 'y' will cancel the transaction
            stack.Items.Clear();
            stack.Items.Add(new("Are you sure you want to buy ", ConsoleColor.DarkYellow, false));
            stack.Items.Add(new($"{realCount} ", ConsoleColor.Blue, false));
            stack.Items.Add(new($"[{name}] ", ConsoleColor.DarkCyan, false));
            stack.Items.Add(new("for ", ConsoleColor.DarkYellow, false));
            stack.Items.Add(new($"${totalPrice}", ConsoleColor.Green, false));
            stack.Items.Add(new("? ", ConsoleColor.DarkYellow, false));
            stack.Items.Add(new("(y/n)\n", ConsoleColor.DarkCyan, false));
            stack.Draw(false);

            input = Console.ReadLine();
            if (input != "y") {
                Market();
                return;
            }

            // Instances a new object of the given item and adds it to the users "inventory"
            for (int i=0; i < realCount; i++) {
                Animal newAnimal = new Chicken();
                if (item == "chicken") {
                    newAnimal = new Chicken();
                }
                else if (item == "cow") {
                    newAnimal = new Cow();
                }
                else if (item == "tiger") {
                    newAnimal = new Tiger();
                }
                else if(item == "pissmyra") {
                    newAnimal = new Pissmyra();
                }
                else if(item == "sengångare")
                {
                    newAnimal = new Sengångare();
                }
                Utility.AddAnimal(newAnimal);
            }
            
            FarmManager.Cash -= totalPrice;

            // Displays to the user that their purchase was successful
            stack.Items.Clear();
            stack.Items.Add(new("Successfully bought ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"{realCount} ", ConsoleColor.Blue, false));
            stack.Items.Add(new($"[{name}] ", ConsoleColor.DarkCyan, true));
            stack.Items.Add(new("for ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"${totalPrice}\n", ConsoleColor.Green, true));
            stack.Draw(false);

            Utility.WaitForInput();
            Market();
            return;
        }

        static public void DisplayAnimal(string key)
        {
            bool success = FarmManager.Animals.TryGetValue(key, out List<Animal>? animals);
            if (!success) return;

            FarmManager.currentCommand = "displayAnimal";
            FarmManager.previousCommand = "animals";
            FarmManager.selectedAnimal = animals;

            key = Utility.FirstLetterUppercase(key);

            WriteStack stack = new(new List<WriteStackItem> {
                new($"--- {key}s ---\n", ConsoleColor.Yellow, false)
            });

            for (int i=0; i < animals.Count; i++) {
                stack.Items.Add(new($"[{i}] ", ConsoleColor.Blue, false));
                stack.Items.Add(new($"{animals[i].Name}\n", ConsoleColor.Green, false));
            }
            stack.Draw();
        }

        static public void AnimalInteraction(string key) {
            bool success = int.TryParse(key, out int index);
            if (!success) return;
            if (index + 1 > FarmManager.selectedAnimal.Count || index < 0) return;

            Animal animal = FarmManager.selectedAnimal[index];

            WriteStack stack = new(new List<WriteStackItem> {
                new($"[{animal.Name}] ", ConsoleColor.DarkCyan, true),
                new($"says: ", ConsoleColor.White, true),
                new($"'{animal.Call}'\n\n", ConsoleColor.DarkYellow, true)
            });
            stack.Draw(false);
            Utility.WaitForInput();
            FarmManager.currentCommand = "displayAnimal";
            FarmManager.previousCommand = "animals";
            DisplayAnimal(animal.Name.ToLower());
        }

        static public void DisplayItem(string key)
        {
            key = key.ToLower();

            FarmManager.currentCommand = "displayItem";
            FarmManager.previousCommand = "inventory";

            if (key == "sell all" || key == "sell") {
                int totalValue = 0;
                foreach (KeyValuePair<string, List<Item>> pair in FarmManager.Inventory) {
                    foreach (Item item in pair.Value) {
                        totalValue += item.Value;
                    }
                }

                WriteStack Stack = new(new() {
                    new("Are you sure you want to sell ", ConsoleColor.DarkYellow, false),
                    new("all ", ConsoleColor.Red, false),
                    new("your items for ", ConsoleColor.DarkYellow, false),
                    new($"${totalValue}", ConsoleColor.Green, false),
                    new($"? ", ConsoleColor.DarkYellow, false),
                    new("(y/n)\n", ConsoleColor.DarkCyan, false)
                });
                Stack.Draw(false);

                string? input = Console.ReadLine();
                if (input != "y") {
                    Inventory();
                    return;
                }

                FarmManager.Inventory.Clear();
                FarmManager.Cash += totalValue;

                Stack.Items = new() {
                    new("Sold all your items for ", ConsoleColor.DarkGreen, true),
                    new($"${totalValue}\n", ConsoleColor.Green, true)
                };
                Stack.Draw(false);

                FarmManager.currentItem = null;
                Utility.WaitForInput();
                Inventory();
                return;
            }

            bool success = FarmManager.Inventory.TryGetValue(key, out List<Item>? items);
            if (!success) return;

            FarmManager.currentItem = (key, items);
            key = Utility.FirstLetterUppercase(key);

            WriteStack stack = new(new List<WriteStackItem>
            {
                new($"--- {key} ", ConsoleColor.Yellow, false),
                new($"[{items.Count}]", ConsoleColor.Blue, false),
                new($" ---\n", ConsoleColor.Yellow, false),

                new($"* Sell\n", ConsoleColor.DarkCyan, false)
            });
            stack.Draw();
        }

        static public void ItemAction(string action) {
            action = action.ToLower();

            FarmManager.currentCommand = "itemAction";
            FarmManager.previousCommand = "displayItem";

            if (FarmManager.currentItem == null) return;

            string itemName = FarmManager.currentItem.Value.Item1;
            List<Item> item = FarmManager.currentItem.Value.Item2;
            int count = item.Count;

            int sum = 0;
            foreach (Item Item in FarmManager.currentItem.Value.Item2) {
                sum += Item.Value;
            }

            WriteStack stack = new(new List<WriteStackItem>());
            
            if (action == "sell") {
                stack.Items.Add(new("Are you sure you want to sell ", ConsoleColor.DarkYellow, false));
                stack.Items.Add(new($"{count} ", ConsoleColor.Blue, false));
                stack.Items.Add(new($"{Utility.FirstLetterUppercase(itemName)} ", ConsoleColor.Green, false));
                stack.Items.Add(new("for ", ConsoleColor.DarkYellow, false));
                stack.Items.Add(new($"${sum}", ConsoleColor.Green, false));
                stack.Items.Add(new("? ", ConsoleColor.DarkYellow, false));
                stack.Items.Add(new("(y/n)\n", ConsoleColor.DarkCyan, false));
            } else {
                return;
            }
            stack.Draw(false);
            stack.Items.Clear();

            string? input = Console.ReadLine();
            if (input != "y") {
                return;
            }

            FarmManager.Cash += sum;
            FarmManager.Inventory.Remove(itemName);
            stack.Items.Add(new($"Sold ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"{count} ", ConsoleColor.Blue, true));
            stack.Items.Add(new($"{Utility.FirstLetterUppercase(itemName)} ", ConsoleColor.White, true));
            stack.Items.Add(new($"for ", ConsoleColor.DarkGreen, true));
            stack.Items.Add(new($"${sum}\n", ConsoleColor.Green, true));
            stack.Draw(false);

            Utility.WaitForInput();
            Inventory();
        }

    }
}
