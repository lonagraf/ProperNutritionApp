using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class RegistrationWindow : Window
{
    private Database _db = new Database();
    private ObservableCollection<Gender> _genders = new ObservableCollection<Gender>();
    public RegistrationWindow()
    {
        InitializeComponent();
        LoadDataGenderCmb();
    }

    private void SignUpBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        _db.OpenConnetion();
        string sql =
            "insert into user (username, password, first_name, birthday, gender, weight, height, goal, activity) values (@un, @ps, @fn, @b, @g, @w, @h, @goal, @a)";
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@un", LoginTxt.Text);
        command.Parameters.AddWithValue("@ps", PasswordTxt.Text);
        command.Parameters.AddWithValue("fn", NameTxt.Text);
        command.Parameters.AddWithValue("@b", BirthdayPicker);
        command.Parameters.AddWithValue("@g", GenderCmb.SelectedItem);
        command.Parameters.AddWithValue("@w", WeightNum.Text);
        command.Parameters.AddWithValue("@h", HeightNum.Text);
        command.Parameters.AddWithValue("@goal", GoalCmb.SelectedItem);
        command.Parameters.AddWithValue("@a", ActivityCmb.SelectedItem);
        command.ExecuteNonQuery();
        _db.CloseConnection();
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
}