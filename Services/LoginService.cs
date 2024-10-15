using Newtonsoft.Json;


public class LoginService : ILoginService
{
    private Dictionary<string, Account> _loggedInUsers = new Dictionary<string, Account>();

    // private Account? _loggedInUser = null;
    // private bool _isLoggedIn = false;
    private string _fileName = "Data/Accounts.json";

    public bool Login(Account account)
    {
        if (!DomainInputValidation.IsValidUsername(account.Username) || !DomainInputValidation.IsValidPassword(account.Password))
        {
            return false;
        }

        List<Account> accounts = JsonFileHandler.ReadJsonFile<Account>(_fileName);
        Account? account1 = accounts.Find(a => a.Username == account.Username && a.Password == account.Password && a.Role == account.Role);
        if (account1 != null)
        {
            _loggedInUsers[account.Username] = account1;
            return true;
        }
        return false;
    }

    // public bool SearchAccount(string username, string password)
    // {
    //     List<Account> accounts = JsonFileHandler.ReadJsonFile<Account>(_fileName);
        
    //     foreach (Account item in accounts)
    //     {
    //         if (item.Username == username && item.Password == password)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    public bool Register(Account account){
        if (!DomainInputValidation.IsValidUsername(account.Username) || !DomainInputValidation.IsValidPassword(account.Password))
        {
            return false;
        }
        else if (account == null) return false;

        account.Role = "User";

        List<Account> accounts = JsonFileHandler.ReadJsonFile<Account>(_fileName);

        accounts.Add(account);

        var updatedJson = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        File.WriteAllText(_fileName, updatedJson);

        return true;

    }


    public bool CheckSession(string username)
    {
        return _loggedInUsers.ContainsKey(username);
    }

    

    public void LogOut(string username)
    {
        _loggedInUsers.Remove(username);

    }

    
}