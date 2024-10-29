namespace AuthAPI.CookieGenerator
{
    public class CookieGenerator : ICookieGenerator
    {
        public void CreateCookie(string tokenString, HttpResponse httpResponse)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddHours(8);
            cookieOptions.Path = "/SimpleTicketingSystem";
            cookieOptions.Secure = true;
            httpResponse.Cookies.Append("SimpleTicketingSystem", tokenString, cookieOptions);
        }
    }
}
