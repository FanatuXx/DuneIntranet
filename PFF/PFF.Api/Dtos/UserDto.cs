namespace PFF.Api.Dtos
{
    public class UserDto(int id, string username, string role, string token, string refreshToken)
    {
        public int Id { get; } = id;
        public string Username { get; } = username;
        public string Role { get; } = role;
        public string Token { get; internal set; } = token;
        public string RefreshToken { get; internal set; } = refreshToken;
    }
}
