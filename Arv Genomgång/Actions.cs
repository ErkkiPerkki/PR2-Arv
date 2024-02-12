namespace Arv_Genomgång
{
    static public class Actions
    {
        static public void MainMenu() {
            FarmManager.currentCommand = null;
            WriteStack stack = new(new List<WriteStackItem>
            {
                new("--- Farm Actions ---\n", ConsoleColor.Yellow, false),
                new("* animals\n", ConsoleColor.DarkCyan, false),
                new("* inventory\n", ConsoleColor.DarkCyan, false)
            });
            stack.Draw();
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
                stack.Items.Add(new($"{animal.ToString()}\n", ConsoleColor.Green, false));
            }

            stack.Draw();
        }

    }
}
