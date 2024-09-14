namespace NZWalks.API.Models.DTO
{
    public class JwtDto
    {
    }

    public class CreateJwtDto
    {
        public string Username { get; set; }
    }

    public class JwtResponseDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        
    }
}
