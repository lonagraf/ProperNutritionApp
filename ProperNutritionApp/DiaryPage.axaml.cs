using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class DiaryPage : UserControl
{
    private Database _db = new Database();
    private ObservableCollection<FoodDiary> _diaries = new ObservableCollection<FoodDiary>();
    private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
    private ObservableCollection<Time> _times = new ObservableCollection<Time>();
    private string sql =
        "select diary_id,food_diary.date, recipe_name, time_name, user from food_diary " +
        "join nutrition.meal_time mt on mt.time_id = food_diary.meal_time " +
        "join nutrition.recipe r on r.recipe_id = food_diary.recipe " +
        "where user = @id";

    private int _uId;
    public DiaryPage(int userId)
    {
        InitializeComponent();
        _uId = userId;
        ShowDiary(sql, _uId);
        FillRecipeCBox();
        FillTimeCBox();
        
    }

    private void ShowDiary(string sql, int id)
    {
        _db.OpenConnection();
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currDiary = new FoodDiary()
            {
                DiaryId = reader.GetInt32("diary_id"),
                Date = reader.GetDateTime("date"),
                Recipe = reader.GetString("recipe_name"),
                MealTime = reader.GetString("time_name"),
                User = reader.GetInt32("user")
            };
            _diaries.Add(currDiary);
        }
        _db.CloseConnection();
        LBoxDiary.ItemsSource = _diaries;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Recipe selectedRecipe = (Recipe)RecipeCBox.SelectedItem;
        Time selectedTime = (Time)TimeCBox.SelectedItem;

        int recipeId = GetRecipeIdByName(selectedRecipe.RecipeName);
        int timeId = GetTimeIdByName(selectedTime.TimeName);
        
        _db.OpenConnection();
        string sql =
            "insert into food_diary (date, recipe, meal_time, user) values (CURRENT_DATE, @recipe, @time, @user);";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@recipe",recipeId);
        command.Parameters.AddWithValue("@time", timeId);
        command.Parameters.AddWithValue("@user", _uId);
        command.ExecuteNonQuery();
        _db.CloseConnection();
        UpdateDiaryList();
    }

    private void FillRecipeCBox()
    {
        _db.OpenConnection();
        string sql = "select recipe_name from recipe";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currRecipe = new Recipe()
            {
                RecipeName = reader.GetString("recipe_name")
            };
            _recipes.Add(currRecipe);
        }
        _db.CloseConnection();
        RecipeCBox.ItemsSource = _recipes;
    }
    
    private int GetRecipeIdByName(string recipeName)
    {
        _db.OpenConnection();
        string sql = "SELECT recipe_id FROM recipe WHERE recipe_name = @recipeName;";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@recipeName", recipeName);
        int recipeId = Convert.ToInt32(command.ExecuteScalar());
        _db.CloseConnection();
        return recipeId;
    }

    private int GetTimeIdByName(string timeName)
    {
        _db.OpenConnection();
        string sql = "SELECT time_id FROM meal_time WHERE time_name = @timeName;";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@timeName", timeName);
        int timeId = Convert.ToInt32(command.ExecuteScalar());
        _db.CloseConnection();
        return timeId;
    }
    
    private void FillTimeCBox()
    {
        _db.OpenConnection();
        string sql = "select time_name from meal_time";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currTime = new Time()
            {
                TimeName = reader.GetString("time_name")
            };
            _times.Add(currTime);
        }
        _db.CloseConnection();
        TimeCBox.ItemsSource = _times;
    }
    
    private void UpdateDiaryList()
    {
        _diaries.Clear();
        ShowDiary(sql, _uId);
    }
}