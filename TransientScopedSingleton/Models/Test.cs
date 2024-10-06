namespace TransientScopedSingleton.Models
{
    public interface ITest
    {
    }
    public class Test: ITest
    {
        public readonly IScoped myObject;
        public Test(IScoped myObjetct)
        {
            this.myObject = myObjetct;
        }
    }
}
