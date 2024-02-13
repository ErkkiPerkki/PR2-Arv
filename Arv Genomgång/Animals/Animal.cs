namespace Arv_Genomgång
{
    public class Animal
    {
        private string _call;
        private string _name;
        private int _price;

        public string Call {get{return _call;}}
        public string Name {get{return _name;}}
        public int Price {get{return _price;}}

        public Animal(string name, string call, int price)
        {
            this._name = name;
            this._call = call;
            this._price = price;
        }

        public virtual void Functionality()
        {

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