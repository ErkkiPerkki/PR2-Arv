using System.Runtime.InteropServices.Marshalling;

namespace Arv_Genomgång
{
    internal class FarmManager
    {
        static public int Cash = 60;
        static public string? currentCommand;
        static public string? previousCommand;
        static public bool forceBack = false;
        static public List<Animal>? selectedAnimal;
        static public (string, List<Item>)? currentItem;
        static public Dictionary<string, List<Animal>> Animals = new();
        static public Dictionary<string, List<Item>> Inventory = new();

        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Thread thread = new(new ThreadStart(UpdateAnimals));
            thread.Start();

            while (true) {
                if (currentCommand == null || currentCommand == string.Empty) Actions.MainMenu();

                string? input = forceBack ? "back" : Console.ReadLine();
                if (input == null || input == string.Empty) continue;
                input = input.ToLower();

                if (input == "exit") {
                    currentCommand = null;
                    continue;
                }

                if (input == "back") {
                    currentCommand = null;
                    input = previousCommand;
                    if (forceBack) forceBack = false;
                }

                if (currentCommand != null) {
                    if (currentCommand == "animals") {
                        Actions.DisplayAnimal(input);
                    }
                    else if(currentCommand == "inventory") {
                        Actions.DisplayItem(input);
                    }
                    else if (currentCommand == "displayItem") {
                        Actions.ItemAction(input);
                    }
                    else if(currentCommand == "displayAnimal") {
                        Actions.AnimalInteraction(input);
                    }

                    continue;
                }

                if (input == "animals" || input == "anim") {
                    Actions.Animals();
                }
                else if (input == "inventory" || input == "inv") {
                    Actions.Inventory();
                }
                else if (input == "market" || input == "mark") {
                    Actions.Market();
                }

                continue;
            }

        }

        static private void UpdateAnimals()
        {
            while (true) {
                foreach (KeyValuePair<string, List<Animal>> pair in Animals) {
                    foreach (Animal animal in pair.Value) {
                        animal.Update();
                    }
                }

                Thread.Sleep(5000);
            }

        }
    }
}
