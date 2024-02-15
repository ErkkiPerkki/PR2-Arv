namespace Arv_Genomgång
{
    public class Pissmyra: Animal
    {
        public Pissmyra(): base("Pissmyra", "psst")
        {

        }

        public override void Functionality()
        {
            if (Utility.Random.Next(0, 101) != 0) return;

            int ling = Utility.Random.Next(25, 51);
            Myrkiss kiss = new(ling);
            Utility.AddItemToInventory(kiss);
        }
    }
}
