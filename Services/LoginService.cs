public class LoginService : ILoginService
{
    private Account? _loggedInUser = null;
    private bool _isLoggedIn = false;
    private string _fileName = "Data/Accounts.json";

    public async Task<bool> LogInAsync(Account account)
    {
        if (!DomainInputValidation.IsValidUsername(account.Username) || !DomainInputValidation.IsValidPassword(account.Password))
        {
            return false;
        }


        if (SearchAccount(account.Username, account.Password))
        {
            _loggedInUser = account;
            _isLoggedIn = true;
            return true;
        }

        return false;
    }

    public bool SearchAccount(string username, string password)
    {
        List<Account> accounts = JsonFileHandler.ReadJsonFile<Account>(_fileName);
        
        foreach (Account item in accounts)
        {
            if (item.Username == username && item.Password == password)
            {
                return true;
            }
        }
        return false;
    }


    public bool CheckSession()
    {
        return _isLoggedIn;
    }

    public Account GetLoggedInUser()
    {
        return _loggedInUser;
    }

    public void LogOut()
    {
        _loggedInUser = null!;
        _isLoggedIn = false;
    }
}