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

            Utility.AnimatedWrite("PLOP!");
            Thread.Sleep(500);
            Utility.AnimatedWrite($"Chicken layed an egg weighing {weight}g \n");
            Thread.Sleep(500);
        }
    }
}