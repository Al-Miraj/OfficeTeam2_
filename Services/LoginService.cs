public class LoginService : ILoginService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string _fileName = "Data/Accounts.json";

    public LoginService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> LogInAsync(Account account)
    {
        if (!DomainInputValidation.IsValidUsername(account.Username) || !DomainInputValidation.IsValidPassword(account.Password))
        {
            return false;
        }

        Account? userAccount = SearchAccount(account.Username, account.Password);
        if (userAccount != null)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Username", userAccount.Username);
            _httpContextAccessor.HttpContext.Session.SetString("IsAdmin", userAccount.IsAdmin.ToString());
            return true;
        }

        return false;
    }

    public Account? SearchAccount(string username, string password)
    {
        List<Account> accounts = JsonFileHandler.ReadJsonFile<Account>(_fileName);

        foreach (Account item in accounts)
        {
            if (item.Username == username && item.Password == password)
            {
                return item;
            }
        }
        return null;
    }

    public bool CheckSession()
    {
        return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("Username"));
    }

    public Account? GetLoggedInUser()
    {
        var username = _httpContextAccessor.HttpContext.Session.GetString("Username");
        var isAdminString = _httpContextAccessor.HttpContext.Session.GetString("IsAdmin");

        if (username == null || isAdminString == null)
        {
            return null;
        }

        bool isAdmin = bool.Parse(isAdminString);

        return new Account(username, "", isAdmin); // wachtwoord als empty string want hoeft nog niet gesecured te worden?
    }

    public void LogOut()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
    }
}
