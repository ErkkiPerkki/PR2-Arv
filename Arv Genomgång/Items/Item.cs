namespace Arv_Genomgång
{
    public class Item
    {
        private string name;
        private int value;

        public string Name {get{return name;}}
        public int Value { get { return value; } }

        public Item(string name, int baseValue) {
            this.name = name;
            value = baseValue;
        }
    }
}
