using System;

namespace ProperNutritionApp;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public DateTime Birthday { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public string Goal { get; set; }
    public double Activity { get; set; }
}