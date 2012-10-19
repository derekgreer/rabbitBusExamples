using System.Web.Mvc;

namespace Bookstore.Web.Initialization
{
    public class FilterInitializer : IApplicationInitializer
    {
        public void Initialize()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}