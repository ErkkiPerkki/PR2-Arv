namespace Arv_Genomgång
{
    public class Item
    {
        private string name;
        private int amount;

        public string Name {get{return name;}}
        public int Amount {get{return amount;} set{amount = value;}}

        public Item(string name) {
            this.name = name;
            amount = 1;
        }
    }
}
