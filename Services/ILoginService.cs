public interface ILoginService
{
    bool Login(Account account);
    bool CheckSession(string username);
    // Account GetLoggedInUser(string username);
    void LogOut(string username);
    bool Register(Account account);
}
