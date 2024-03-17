namespace ProperNutritionApp;

public class Gender
{
    public int GenderId { get; set; }
    public string GenderName { get; set; }
    
    public override string ToString()
    {
        return GenderName;
    }
}