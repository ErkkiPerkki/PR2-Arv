namespace Arv_Genomgång
{
    public class WriteStack
    {
        public List<WriteStackItem> Items = new();

        public WriteStack(List<WriteStackItem> items)
        {
            foreach(WriteStackItem item in items) {
                Items = items;
            }
        }

        public void Draw(bool showNavigationCommands = true)
        {
            Console.Clear();

            foreach(WriteStackItem item in Items) {
                if (item.Animated) {
                    Utility.AnimatedWrite(item.Text, item.Color);
                }
                else {
                    Utility.ColoredWrite(item.Text, item.Color);
                }
            }

            if (!showNavigationCommands) return;
            Utility.ColoredWrite($"\nType 'exit' to return to the main menu\n", ConsoleColor.Red);
            Utility.ColoredWrite($"Type 'back' to return to the previous menu\n", ConsoleColor.DarkRed);
        }
    }
}
