namespace Arv_Genomgång
{
    public class WriteStack
    {
        public List<WriteStackItem> Items;

        public WriteStack(List<WriteStackItem> items)
        {
            foreach(WriteStackItem item in items) {
                Items = items;
            }
        }

        public void Draw()
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

            Utility.ColoredWrite($"\nType 'exit' to return to the main menu\n", ConsoleColor.DarkRed);
            Utility.ColoredWrite($"Type 'back' to return to the previous menu\n", ConsoleColor.DarkYellow);
        }
    }
}
