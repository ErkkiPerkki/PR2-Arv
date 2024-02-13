using Arv_Genomgång.Items;

namespace Arv_Genomgång
{
    public class Cow: Animal
    {

        public Cow(): base("Cow", "Muuuu")
        {
            
        }
        
        public override void Functionality()
        {
            if (Utility.Random.Next(0, 12) != 0) return;

            int volume = Utility.Random.Next(1, 6);
            Milk milk = new(volume);
            Utility.AddItemToInventory(milk);
        }
    }
}
