namespace AuthAPI.CookieGenerator
{
    public interface ICookieGenerator
    {
        public void CreateCookie(string tokenString, HttpResponse httpResponse);
    }
}
