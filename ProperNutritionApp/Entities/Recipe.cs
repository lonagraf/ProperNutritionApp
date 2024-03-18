using System.Reflection.Metadata;

namespace ProperNutritionApp;

public class Recipe
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Instructions { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Carbs { get; set; }
    public byte[]? Image { get; set; }

    public override string ToString()
    {
        return RecipeName;
    }
}