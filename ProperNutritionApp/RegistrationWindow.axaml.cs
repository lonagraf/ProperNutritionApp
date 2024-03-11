using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class RegistrationWindow : Window
{
    private Database _db = new Database();
    private ObservableCollection<Gender> _genders = new ObservableCollection<Gender>();
    private ObservableCollection<Goal> _goals = new ObservableCollection<Goal>();
    private ObservableCollection<Activity> _activities = new ObservableCollection<Activity>();
    public RegistrationWindow()
    {
        InitializeComponent();
        LoadDataGenderCmb();
        LoadDataGoalCmb();
        LoadDataActivityCmb();
    }

    private void SignUpBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LoginTxt.Text) || string.IsNullOrWhiteSpace(PasswordTxt.Text) ||
            string.IsNullOrWhiteSpace(NameTxt.Text) || BirthdayPicker.SelectedDate == null || 
            GenderCmb.SelectedItem == null || string.IsNullOrWhiteSpace(WeightNum.Text) || 
            string.IsNullOrWhiteSpace(HeightNum.Text) || GoalCmb.SelectedItem == null || 
            ActivityCmb.SelectedItem == null)
        {
            ShowMessageBox("Ошибка", "Заполните все данные", MsBox.Avalonia.Enums.Icon.Error);
        }
        else
        {
            try
            {
                _db.OpenConnetion();
                string sql =
                    "insert into user (username, password, first_name, birthday, gender, weight, height, goal, activity) values (@un, @ps, @fn, @b, @g, @w, @h, @goal, @a)";
                MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
                command.Parameters.AddWithValue("@un", LoginTxt.Text);
                command.Parameters.AddWithValue("@ps", PasswordTxt.Text);
                command.Parameters.AddWithValue("fn", NameTxt.Text);
                command.Parameters.AddWithValue("@b", BirthdayPicker.SelectedDate.GetValueOrDefault());
                command.Parameters.AddWithValue("@g", GetSelectedId("gender", GenderCmb.SelectedItem.ToString()));
                command.Parameters.AddWithValue("@w", WeightNum.Text);
                command.Parameters.AddWithValue("@h", HeightNum.Text);
                command.Parameters.AddWithValue("@goal", GetSelectedId("goal", GoalCmb.SelectedItem.ToString()));
                command.Parameters.AddWithValue("@a", GetSelectedId("activity", ActivityCmb.SelectedItem.ToString()));
                command.ExecuteNonQuery();
                _db.CloseConnection();
                ShowMessageBox("Успешно", "Вы успешно зарегистрированы!", MsBox.Avalonia.Enums.Icon.Success);
            }
            catch (Exception exception)
            {
                ShowMessageBox("Ошибка","Данные введены не корректно", MsBox.Avalonia.Enums.Icon.Error);
            }
            AuthWindow authWindow = new AuthWindow();
            this.Close();
            authWindow.Show();
        }
    }
    
    
    private void LoadDataGenderCmb()
    {
        _db.OpenConnetion();
        string sql = "select gender_name from gender";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentGender = new Gender()
            {
                GenderName = reader.GetString("gender_name")
            };
            _genders.Add(currentGender);
        }
        _db.CloseConnection();
        GenderCmb.ItemsSource = _genders;
    }

    private void LoadDataGoalCmb()
    {
        _db.OpenConnetion();
        string sql = "select goal_name from goal";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentGoal = new Goal()
            {
                GoalName = reader.GetString("goal_name")
            };
            _goals.Add(currentGoal);
        }
        _db.CloseConnection();
        GoalCmb.ItemsSource = _goals;
    }

    private void LoadDataActivityCmb()
    {
        _db.OpenConnetion();
        string sql = "select activity_name, activity_ratio from activity";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var currentActivity = new Activity()
            {
                ActivityName = reader.GetString("activity_name"),
                ActivityRatio = reader.GetDouble("activity_ratio")
            };
            _activities.Add(currentActivity);
        }
        _db.CloseConnection();
        ActivityCmb.ItemsSource = _activities;
    }
   
    private int GetSelectedId(string tableName, string selectedItem)
    {
        _db.OpenConnetion();
        string sql = $"select {tableName}_id from {tableName} where {tableName}_name = @name";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@name", selectedItem);
        int selectedId = Convert.ToInt32(command.ExecuteScalar());
        return selectedId;
    }

    private void BackMain_OnTapped(object? sender, TappedEventArgs e)
    {
        AuthWindow authWindow = new AuthWindow();
        this.Hide();
        authWindow.Show();
    }
    
    private void ShowMessageBox(string title, string message, MsBox.Avalonia.Enums.Icon icon)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message, ButtonEnum.Ok, icon);
        var result = box.ShowAsync();
    }
}