namespace ProperNutritionApp;

public class Goal
{
    public int GoalId { get; set; }
    public string GoalName { get; set; }

    public override string ToString()
    {
        return GoalName;
    }
}