namespace Arv_Genomgång.Items
{
    public class Egg : Item
    {
        private int weight;
        public int Weight {get{return weight;}}

        public Egg(int weight): base("Egg")
        {
            this.weight = weight;
        }
    }
}
