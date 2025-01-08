// src/models/User.ts
export interface User {
    Id: string; // C# Guid wordt een string in TypeScript
    First_Name: string;
    Last_Name: string;
    Email: string;
    Password: string;
    Recurring_Days: number;
    Role: string;
}
