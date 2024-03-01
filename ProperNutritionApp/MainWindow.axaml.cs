using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ProperNutritionApp;

public partial class MainWindow : Window
{
    private int _uId;
    public MainWindow(int userId)
    {
        InitializeComponent();
        _uId = userId;
    }

    private void HomeBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        PagesPanel.Children.Clear();
        DashboardPage dashboardPage = new DashboardPage();
        PagesPanel.Children.Add(dashboardPage);
    }

    private void ProfileBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        PagesPanel.Children.Clear();
        UserPage userPage = new UserPage(_uId);
        PagesPanel.Children.Add(userPage);
    }

    private void RecipeBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        PagesPanel.Children.Clear();
        RecipePage recipePage = new RecipePage();
        PagesPanel.Children.Add(recipePage);
    }
}