namespace Arv_Genomgång.Items
{
    public class Milk : Item
    {
        private int volume;

        public int Volume {get{return volume;}}

        public Milk(int volume): base("Milk", volume * 16)
        {
            this.volume = volume;
        }
    }
}
