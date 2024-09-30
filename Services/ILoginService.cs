public interface ILoginService
{
    Task<bool> LogInAsync(Account account);
    bool CheckSession();
    Account GetLoggedInUser();
    void LogOut();
}