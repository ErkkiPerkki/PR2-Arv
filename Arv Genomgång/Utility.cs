using Arv_Genomgång.Items;

namespace Arv_Genomgång
{
    public class Utility
    {
        static public Random Random = new();

        static public void AnimatedWrite(string text, ConsoleColor textColor = ConsoleColor.White)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;

            for (int i=0; i < text.Length; i++) {
                Console.Write(text[i]);
                Thread.Sleep(50);
            }

            Console.ForegroundColor = previousColor;
        }

        static public void ColoredWrite(string text, ConsoleColor textColor)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;

            Console.Write(text);

            Console.ForegroundColor = previousColor;
        }

        static public string[] ToStringArray<T>(T[] array)
        {
            string[] Array = new string[array.Length];

            for (short i=0; i < array.Length; i++) {
                Array[i] = array[i].ToString();
            }

            return Array;
        }

        static public string FirstLetterUppercase(string String)
        {
            return $"{char.ToUpper(String[0])}{String.Substring(1).ToLower()}";
        }

        static public void AddItemToInventory(Item item) {
            string key = item.Name.ToLower();

            if (!FarmManager.Inventory.ContainsKey(key)) {
                FarmManager.Inventory.Add(key, new List<Item>());
            }
            FarmManager.Inventory[key].Add(item);
        }

    }
}
