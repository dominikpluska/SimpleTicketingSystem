namespace AuthAPI.Dto
{
    public class UserAccountDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; } = null;
        public string? SelectedRole { get; set; } = null;
    }
}
