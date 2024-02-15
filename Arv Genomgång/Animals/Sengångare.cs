namespace Arv_Genomgång
{
    public class Sengångare: Animal
    {
        public Sengångare() : base("Sengångare", "hmmmmm")
        {

        }

        public override void Functionality()
        {
            if (Utility.Random.Next(0, 201) != 0) return;

            int glädje = Utility.Random.Next(3, 11);
            Lathet lathet = new(glädje);
            Utility.AddItemToInventory(lathet);
        }
    }
}
