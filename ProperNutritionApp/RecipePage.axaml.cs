using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class RecipePage : UserControl
{
    private Database _db = new Database();
    private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
    private string sql = "select * from recipe";
    public RecipePage()
    {
        InitializeComponent();
        ShowRecipeTable(sql);
        
    }

    private void ShowRecipeTable(string sql)
    {
        _db.OpenConnection();
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentRecipe = new Recipe()
            {
                RecipeId = reader.GetInt32("recipe_id"),
                RecipeName = reader.GetString("recipe_name"),
                Instructions = reader.GetString("instructions"),
                Calories = reader.GetDouble("calories"),
                Protein = reader.GetDouble("protein"),
                Fat = reader.GetDouble("fat"),
                Carbs = reader.GetDouble("carbs"),
                Image = reader["image"] as byte[]
            };
            _recipes.Add(currentRecipe);
        }
        _db.CloseConnection();
        LBoxRecipes.ItemsSource = _recipes;
    }

    private void LBoxRecipes_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LBoxRecipes.SelectedItem != null)
        {
            Recipe selectedRecipe = LBoxRecipes.SelectedItem as Recipe;
            RecipeInfoPanel.Children.Clear();
            RecipeInfo recipeInfo = new RecipeInfo(selectedRecipe);
            RecipeInfoPanel.Children.Add(recipeInfo);
        }
    }
}