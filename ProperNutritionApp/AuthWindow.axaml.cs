using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp;

public partial class AuthWindow : Window
{
    private Database _db = new Database();
    
    public AuthWindow()
    {
        InitializeComponent();
    }


    private void SignInBtn_OnClick(object? sender, RoutedEventArgs e)
    { 
        string sql = "select user_id from user where username = @username and password = @password";
        _db.OpenConnection();
        MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
        command.Parameters.AddWithValue("@username", LoginTxt.Text);
        command.Parameters.AddWithValue("@password", PasswordTxt.Text);

        object result = command.ExecuteScalar();

        if (result != null)
        {
            int userId = Convert.ToInt32(result);
            
            MainWindow mainWindow = new MainWindow(userId);
            this.Hide();
            mainWindow.Show();
        }
        else
        {
            Console.WriteLine("pipja");
        }
        _db.CloseConnection();
    }

    private void SignUpTxt_OnTapped(object? sender, TappedEventArgs e)
    {
        RegistrationWindow registrationWindow = new RegistrationWindow();
        this.Hide();
        registrationWindow.Show();
    }

    private void CloseApp_OnTapped(object? sender, TappedEventArgs e)
    {
        this.Close();
    }
}