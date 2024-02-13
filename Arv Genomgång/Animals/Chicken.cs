using Arv_Genomgång.Items;

namespace Arv_Genomgång
{
    public class Chicken: Animal 
    {
        public Chicken(): base("buc buc", "Chicken")
        {

        }

        public override void Functionality()
        {
            int weight = Utility.Random.Next(38, 51);
            Egg egg = new(weight);

            string key = egg.Name.ToLower();
            if (!FarmManager.Inventory.ContainsKey(key)) {
                FarmManager.Inventory.Add(key, new List<Item>());
            }
            FarmManager.Inventory[key].Add(egg);
            

            //Utility.AnimatedWrite("PLOP!");
            //Utility.AnimatedWrite($"Chicken layed an egg weighing {weight}g \n");
        }
    }
}