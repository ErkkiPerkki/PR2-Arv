namespace Arv_Genomgång
{
    public class Lathet : Item
    {
        private int glädje;
        public int Glädje {get{return glädje;}}

        public Lathet(int glädje): base("Lathet", glädje * 1000)
        {
            this.glädje = glädje;
        }
    }
}
