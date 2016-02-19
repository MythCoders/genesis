using MC.eSIS.Core;

namespace MC.eSIS.Task.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = ConfigurationHelper.InstanceDbConnectionName();
        }
    }
}
