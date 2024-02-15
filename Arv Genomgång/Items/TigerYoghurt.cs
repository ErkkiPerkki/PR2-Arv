namespace Arv_Genomgång
{
    public class TigerYoghurt : Item
    {
        private int volume;

        public int Volume {get{return volume;}}

        public TigerYoghurt(int volume) : base("Tiger Yoghurt", volume * 125)
        {
            this.volume = volume;
        }
    }
}
