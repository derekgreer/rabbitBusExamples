using System.Web.Routing;

namespace Bookstore.Web.Initialization
{
    public class RouteInitializer : IApplicationInitializer
    {
        public void Initialize()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}