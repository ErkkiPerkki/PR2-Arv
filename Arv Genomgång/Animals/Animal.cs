namespace Arv_Genomgång
{
    public class Animal
    {
        private string _call;
        private string _name;

        public string Call {get{return _call;}}
        public string Name {get{ return _name;}}

        public Animal(string call, string name = "NewAnimal")
        {
            this._name = name;
            this._call = call;
        }

        public virtual void Functionality()
        {
            //Console.WriteLine("Default Functionality");
        }

        public void MakeNoise()
        {
            Console.WriteLine(_call);
        }

        public void Update()
        {
            Functionality();
        }
    }
}