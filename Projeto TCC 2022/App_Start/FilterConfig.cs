using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //comentário
        }
    }
}
