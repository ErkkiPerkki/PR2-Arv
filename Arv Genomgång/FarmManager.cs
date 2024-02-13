using System.Runtime.InteropServices.Marshalling;

namespace Arv_Genomgång
{
    internal class FarmManager
    {
        static public int Cash = 25;
        static public string? currentCommand;
        static public string? previousCommand;
        static public (string, List<Item>)? currentItem;
        static public Dictionary<string, List<Animal>> Animals = new();
        static public Dictionary<string, List<Item>> Inventory = new();

        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Animals.Add("cow", new List<Animal>() {new Cow(), new Cow() , new Cow() });
            Animals.Add("chicken", new List<Animal>() { new Chicken() });
            Animals.Add("tiger", new List<Animal>() { new Tiger() });

            Thread thread = new(new ThreadStart(UpdateAnimals));
            thread.Start();

            while (true) {
                if (currentCommand == null) Actions.MainMenu();
                
                string? input = Console.ReadLine();
                if (input == null) continue;
                input = input.ToLower();

                if (input == "exit") {
                    currentCommand = null;
                    continue;
                }

                if (input == "back") {
                    currentCommand = null;
                    input = previousCommand;
                }

                if (currentCommand != null){
                    if (currentCommand == "animals") {
                        Actions.DisplayAnimal(input);
                    }
                    if (currentCommand == "inventory") {
                        Actions.DisplayItem(input);
                        continue;
                    }
                    if (currentCommand == "displayItem") {
                        Actions.ItemAction(input);
                    }

                    continue;
                }

                if (input == "animals") {
                    Actions.Animals();
                    continue;
                }

                if (input == "inventory" || input == "inv") {
                    Actions.Inventory();
                    continue;
                }

                if (input == "market") {
                    continue;
                }
            }

        }

        static private void UpdateAnimals()
        {
            while (true) {
                foreach (KeyValuePair<string, List<Animal>> pair in Animals) {
                    foreach(Animal animal in pair.Value) {
                        animal.Update();
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
