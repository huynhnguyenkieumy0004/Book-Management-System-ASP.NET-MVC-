using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class LoginFilter : IActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var role = session.GetString("Role");

        // Chưa login → redirect Login
        if (string.IsNullOrEmpty(role))
        {
            context.Result = new RedirectToActionResult("Login", "NguoiDung", null);
            return;
        }

        // Vào Admin area mà không phải Admin → redirect Home
        var area = context.RouteData.Values["area"]?.ToString();
        if (area == "Admin" && role != "Admin")
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }

        // Vào User area mà không phải User → redirect Home (tùy muốn)
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
