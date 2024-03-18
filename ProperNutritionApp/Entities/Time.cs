namespace ProperNutritionApp;

public class Time
{
    public int TimeId { get; set; }
    public string TimeName { get; set; }

    public override string ToString()
    {
        return TimeName;
    }
}