using System.Web.Optimization;

namespace Bookstore.Web.Initialization
{
    public class BundleInitializer : IApplicationInitializer
    {
        public void Initialize()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}