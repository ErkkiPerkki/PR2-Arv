namespace Arv_Genomgång
{
    public class Tiger: Animal
    {
        public Tiger(): base("Tiger", "roar")
        {

        }

        public override void Functionality()
        {
            if (Utility.Random.Next(0, 8) != 0) return;

            int volume = Utility.Random.Next(1, 4);
            TigerYoghurt yoghurt = new(volume);
            Utility.AddItemToInventory(yoghurt);
        }
    }
}
