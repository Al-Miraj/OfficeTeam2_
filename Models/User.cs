////////////////////////////////////////////////
// User.cs
////////////////////////////////////////////////
public class User {
    Guid Id {get; set; } // should be Guid
    string First_Name {get; set; }
    string Last_Name {get; set; }
    string Email {get; set; }
    string Password {get; set; }
    int Recurring_Days {get; set; }

    public User () {}
    public User (Guid id, string first_name, string last_name, string email, string password, int recurring_days) { // Q how to handle password securely?
        Id = id;
        First_Name = first_name;
        Last_Name = last_name;
        Email = email;
        Password = password;
        Recurring_Days = recurring_days;
    }
}