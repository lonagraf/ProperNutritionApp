using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class RecipeInfo : UserControl
{
    private Database _db = new Database();
    private Recipe _recipe;
    private string sql;
    private ObservableCollection<RecipeIngredient> _ingredients = new ObservableCollection<RecipeIngredient>();
    
    public RecipeInfo(Recipe recipe)
    {
        InitializeComponent();
        _recipe = recipe;
        InstructionsTxt.Text = _recipe.Instructions;
        ProteinTxt.Text = "Белки: " + _recipe.Protein.ToString();
        FatTxt.Text = "Жиры: " + _recipe.Fat.ToString();
        CarbsTxt.Text = "Углеводы: " + _recipe.Carbs.ToString();
        sql = "SELECT recipe_ingredient_id, recipe, ingredient_name, quantity FROM recipe_ingredients " +
              "join nutrition.ingredient i on i.ingredient_id = recipe_ingredients.ingredient " +
              "WHERE recipe = " + _recipe.RecipeId;
        ShowRecipeIngredient(sql);
    }

    private void ShowRecipeIngredient(string sql)
    {
        _db.OpenConnection();
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentRIngredient = new RecipeIngredient()
            {
                RecipeIngredientId = reader.GetInt32("recipe_ingredient_id"),
                Recipe = reader.GetInt32("recipe"),
                Ingredient = reader.GetString("ingredient_name"),
                Quantity = reader.GetDouble("quantity")
            };
            _ingredients.Add(currentRIngredient);
        }
        _db.CloseConnection();
        LBoxIngredient.ItemsSource = _ingredients;
    }
}