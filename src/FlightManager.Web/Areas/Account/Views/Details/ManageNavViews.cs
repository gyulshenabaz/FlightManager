using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Web.Areas.Account.Views.Details
{
    public static class ManageNavViews
    {
        public static string Index => "Index";
        
        public static string ChangePassword => "ChangePassword";

        public static string IndexNavClass(ViewContext viewContext) => ViewNavClass(viewContext, Index);
        
        public static string ChangePasswordNavClass(ViewContext viewContext) => ViewNavClass(viewContext, ChangePassword);

        private static string ViewNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActiveView"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}