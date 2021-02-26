using Ninject;
namespace Epam.Library.Ioc
{
    public class DependenciesResolver
    {
        private static NinjectBinds _ninjectBinds;
        public static StandardKernel Kernel;

        static DependenciesResolver()
        {
            _ninjectBinds = new NinjectBinds();
            Kernel = new StandardKernel(_ninjectBinds);
        }
    }
}
