using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ProperNutritionApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
        UserPage userPage = new UserPage();
        PagesPanel.Children.Add(userPage);
    }

    private void RecipeBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        PagesPanel.Children.Clear();
        RecipePage recipePage = new RecipePage();
        PagesPanel.Children.Add(recipePage);
    }
}