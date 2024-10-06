namespace TransientScopedSingleton.Models
{
    public interface ITransitent { }
    public interface IScoped { }
    public interface ISingleton { }
    public class MyObject : ITransitent, IScoped, ISingleton
    {
        public readonly int Number;

        public MyObject()
        {
            Number = new Random().Next(1000);
        }
    }
}
