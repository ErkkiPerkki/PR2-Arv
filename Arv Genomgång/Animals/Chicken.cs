using Arv_Genomgång.Items;

namespace Arv_Genomgång
{
    public class Chicken: Animal 
    {

        public Chicken(): base("Chicken", "buc buc")
        {
            
        }

        public override void Functionality()
        {
            if (Utility.Random.Next(0, 3) != 0) return;

            int weight = Utility.Random.Next(38, 52);
            Egg egg = new(weight);
            Utility.AddItemToInventory(egg);
        }
    }
}