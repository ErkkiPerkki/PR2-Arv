namespace Arv_Genomgång
{
    public class Cow: Animal
    {
        public Cow(): base("Muuuu", "Cow")
        {
            
        }
        
        public override void Functionality()
        {
            int milk = 25 + Utility.Random.Next(1, 11);
            //Utility.AnimatedWrite("SPFFFFFT..");
            //Thread.Sleep(500);
            //Utility.AnimatedWrite($"Milked cow for: {milk}ml of milk \n");
            //Thread.Sleep(500);
        }
    }
}
