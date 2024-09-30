public class Account {
    public string Username {get; set; }
    public string Password {get; set; } // Q how to security this TT
    public Account() {}
    public Account(string username, string password){
        Username = username;
        Password = password;
    }
}