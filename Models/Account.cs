public class Account {
    public string? Username {get; set; }
    public string? Password {get; set; } // Q how to security this TT

    public string? Role  {get;set;}
    
    public Account(string username, string password, string Role){
        Username = username;
        Password = password;
        this.Role = Role;
    }
}