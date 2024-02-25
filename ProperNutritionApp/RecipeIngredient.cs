namespace ProperNutritionApp;

public class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }
    public int Recipe { get; set; }
    public string Ingredient { get; set; }
    public double Quantity { get; set; }
}