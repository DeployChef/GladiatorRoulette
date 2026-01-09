namespace Domain
{
    public class Gladiator
    {
        public Gladiator(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public bool IsAlive { get; private set; } = true;
        
        public void Eliminate() => IsAlive = false;
        public void Reset() => IsAlive = true;
    }
}