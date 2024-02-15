namespace Arv_Genomgång
{
    public class Myrkiss: Item
    {
        private int ling;
        public int Ling {get{return ling;}}

        public Myrkiss(int ling): base("Myrkiss", ling * 5)
        {
            this.ling = ling;
        }
    }
}
