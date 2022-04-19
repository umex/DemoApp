namespace API.DTOs
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public List<String> Roles { get; set; }
    }
}