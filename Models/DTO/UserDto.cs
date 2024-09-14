namespace NZWalks.API.Models.DTO
{
    public class UserDto
    {
    }

    public class AddUserDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
