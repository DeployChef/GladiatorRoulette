namespace Infrastructure.Random
{
    public class SystemRandomProvider : IRandomProvider
    {
        private readonly System.Random _random = new System.Random();
        public int Range(int min, int max) => _random.Next(min, max);
    }
}