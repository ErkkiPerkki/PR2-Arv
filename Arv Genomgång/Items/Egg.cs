namespace Arv_Genomgång.Items
{
    public class Egg : Item
    {
        private int weight;
        public int Weight {get{return weight;}}

        public Egg(int weight): base("Egg", (int)(weight / 10))
        {
            this.weight = weight;
        }
    }
}
