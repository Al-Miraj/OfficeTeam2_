////////////////////////////////////////////////
// User.cs
////////////////////////////////////////////////
public class User
{
    public Guid Id { get; set; } // should be Guid
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public int Recurring_Days { get; set; }

    public User() { }
    public User(Guid id, string first_name, string last_name, string email, string password, bool isAdmin, int recurring_days)
    { // Q how to handle password securely?
        Id = id;
        First_Name = first_name;
        Last_Name = last_name;
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
        Recurring_Days = recurring_days;
    }
}