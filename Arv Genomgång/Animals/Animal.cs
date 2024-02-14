namespace Arv_Genomgång
{
    public class Animal
    {
        private string _call;
        private string _name;

        public string Call {get{return _call;}}
        public string Name {get{return _name;}}

        public Animal(string name, string call)
        {
            this._name = name;
            this._call = call;
        }

        public virtual void Functionality()
        {

        }

        public void Update()
        {
            Functionality();
        }
    }
}