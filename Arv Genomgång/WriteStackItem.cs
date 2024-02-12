namespace Arv_Genomgång
{
    public class WriteStackItem
    {
        private bool _animated;
        private string _text;
        private ConsoleColor _color;

        public string Text {get{return _text;}}
        public ConsoleColor Color { get { return _color;}}
        public bool Animated {get{return _animated;}}

        public WriteStackItem(string text, ConsoleColor color, bool animated)
        {
            _text = text;
            _color = color;  
            _animated = animated;
        }

        public WriteStackItem(string[] array, string title, ConsoleColor color, bool animated)
        {
            string stringedArray = $"[*] -- {title} --\n";
            foreach (string element in array) {
                stringedArray += $" | {element}\n";
            }
            stringedArray += "[*] -------------\n";

            _text = stringedArray;
            _color = color;
            _animated = animated;
        }

    }
}
