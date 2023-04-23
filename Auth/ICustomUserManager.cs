namespace WebAppMovie.Auth
{
    public interface ICustomUserManager
    {
        string Authenticate(string userName,string email ,string password,int id);
    }
}