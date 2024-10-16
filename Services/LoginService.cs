using Newtonsoft.Json;


public class LoginService : ILoginService
{
    private Dictionary<string, User> _loggedInUsers = new Dictionary<string, User>();

    // private Account? _loggedInUser = null;
    // private bool _isLoggedIn = false;
    private string _fileName = "Data/Accounts.json";

    public bool Login(User user)
    {
        List<User> users = JsonFileHandler.ReadJsonFile<User>("Data/Users.json");

        if (user == null)
        {
            Console.WriteLine("Users is null.");
            return false;
        }

        User? user1 = users.Find(a => a.Email == user.Email && a.Password == user.Password && a.Role == user.Role);
        if (user1 == null)
        {
            Console.WriteLine("No matching account found.");
            return false;
        }


        _loggedInUsers[user1.First_Name] = user1;

        return true; 
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
     public string GetUserRole(string username)
    {
        if (_loggedInUsers.TryGetValue(username, out User user))
        {
            return user.Role; // Return the user's role
        }
        return null; // User is not logged in
    }

    public bool Register(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        }

        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentException("Email and Password cannot be null or empty.");
        }

        if (!DomainInputValidation.IsValidUsername(user.Email) || !DomainInputValidation.IsValidPassword(user.Password))
        {
            return false; 
        }

        user.Role = "User";
        user.Id = Guid.NewGuid(); 

        List<User> users = JsonFileHandler.ReadJsonFile<User>("Data/Users.json");

        users.Add(user);

        var updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText("Data/Users.json", updatedJson);

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
    public User FindByID(Guid id){
        List<User> users = JsonFileHandler.ReadJsonFile<User>("Data/Users.json");

        User? user = users.Find(_ => _.Id == id);
        return user!;


    }
    
}