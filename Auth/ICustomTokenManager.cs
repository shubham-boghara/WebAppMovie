namespace WebAppMovie.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(PlayLoad playLoad);
        bool VerifyToken(string token);
        PlayLoad GetUserInfoByToken(string token);
    }
}