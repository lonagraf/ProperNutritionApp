using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class UserPage : UserControl
{
    private int _uId;
    private Database _db = new Database();
    private ObservableCollection<User> _users = new ObservableCollection<User>();
    private string sql = "select user_id, username, password, first_name, birthday, gender_name, weight, height, goal_name, activity_ratio, age from user " +
                         "join nutrition.gender g on g.gender_id = user.gender " +
                         "join nutrition.goal g2 on g2.goal_id = user.goal " + 
                         "join nutrition.activity a on a.activity_id = user.activity " +
                         "where user_id = @id";
    
    public UserPage(int userId)
    {
        InitializeComponent();
        _uId = userId;
        ShowTable(sql, _uId);
        
    }

    private void ShowTable(string sql, int id)
    {
        _db.OpenConnection();
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentUser = new User()
            {
                UserId = reader.GetInt32("user_id"),
                Username = reader.GetString("username"),
                Password = reader.GetString("password"),
                FirstName = reader.GetString("first_name"),
                Birthday = reader.GetDateTime("birthday"),
                Gender = reader.GetString("gender_name"),
                Weight = reader.GetDouble("weight"),
                Height = reader.GetDouble("height"),
                Goal = reader.GetString("goal_name"),
                Activity = reader.GetInt32("activity_ratio"),
                Age = reader.GetInt32("age")
            };
            _users.Add(currentUser);
        }
        _db.CloseConnection();
        LBoxUsers.ItemsSource = _users;
    }

    private void BMICalulation(double weight, double height)
    {
        double heightMeters = height / 100;
        
        double bmi = weight / (heightMeters * heightMeters);

        if (bmi < 16)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Выраженный дефицит массы тела)";
        }
        else if (bmi > 16 && bmi < 18.5)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Недостаточная масса тела)";
        }
        else if (bmi > 18.5 && bmi < 24.99)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Норма)";
        }
        else if (bmi > 25 && bmi < 30)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Избыточная масса тела)";
        }
        else if (bmi > 30 && bmi < 35)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Ожирение первой степени)";
        }
        else if (bmi > 35 && bmi < 40)
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Ожирение второй степени)";
        }
        else
        {
            CalculationTxt.Text = $"ИМТ: {bmi:F2} (Ожирение третьей степени)";
        }

    }

    private void MacronutrientsCalculation(double calories)
    {
        double protein = (calories * 0.3) / 4;
        double fat = (calories * 0.3) / 9;
        double carbs = (calories * 0.4) / 4;

        CalculationTxt.Text = $"Белки: {protein:F1}г Жиры: {fat:F1}г Углеводы: {carbs:F1}г";
        
    }

    private double CaloriesCalculation(double activity, double weight, double height, int age, string gender)
    {
        double bmr;
        if (gender == "Женщина")
        {
            bmr = 10 * weight + 6.25 * height - 5 * age - 161;
        }
        else
        {
            bmr = 10 * weight + 6.25 * height - 5 * age + 5;
        }

        double calories = bmr * activity;
        CalculationTxt.Text = $"Норма калорий: {calories} ккал";
        return calories;
    }


    private void BmiBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_users.Count > 0)
        {
            BMICalulation(_users[0].Weight, _users[0].Height);
        }

        CalculationTxt.IsVisible = true;
    }

    private void MacronutrientsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MacronutrientsCalculation(CaloriesCalculation(_users[0].Activity, _users[0].Weight, _users[0].Height, _users[0].Age, _users[0].Gender));
        CalculationTxt.IsVisible = true;
    }

    private void CaloriesBtn_OnClick(object? sender, RoutedEventArgs e)
    { 
        CaloriesCalculation(_users[0].Activity, _users[0].Weight, _users[0].Height, _users[0].Age, _users[0].Gender);
        CalculationTxt.IsVisible = true;
    }
}