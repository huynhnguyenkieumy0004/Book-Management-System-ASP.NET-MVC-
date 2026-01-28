using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminOnlyAttribute : TypeFilterAttribute
{
    public AdminOnlyAttribute() : base(typeof(LoginFilter))
    {
        Arguments = new object[] { "Admin" };
    }
}
