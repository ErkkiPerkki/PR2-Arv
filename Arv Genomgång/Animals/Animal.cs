namespace Arv_Genomgång
{
    public class Animal
    {
        private string _call;
        private string _name;
        private int _age = 0;

        public string Call {get{return _call;}}
        public string Name {get{ return _name;}}

        public Animal(string call, string name = "NewAnimal")
        {
            this._name = name;
            this._call = call;
        }

        public virtual void Functionality()
        {
            Console.WriteLine("Default Functionality");
        }

        public void MakeNoise()
        {
            Console.WriteLine(_call);
        }

        public bool Update()
        {
            int deathProbability = 100 - _age;
            bool shouldDie = Utility.Random.Next(0, deathProbability) == 0;
            if (shouldDie) return true;
            

            _age += 1;
            return false;
        }
    }
}