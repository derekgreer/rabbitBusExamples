using System.Web.Mvc;

namespace Bookstore.Web.Initialization
{
    public class AreaInitializer : IApplicationInitializer
    {
        public void Initialize()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}