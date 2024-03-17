namespace ProperNutritionApp;

public class Activity
{
    public int ActivityId { get; set; }
    public string ActivityName { get; set; }
    public double ActivityRatio { get; set; }

    public override string ToString()
    {
        return ActivityName;
    }
}