namespace NZWalks.API.Repositories
{
    public interface IAuthRepository
    {
        string CreateJwtToken(string username);
    }
}
