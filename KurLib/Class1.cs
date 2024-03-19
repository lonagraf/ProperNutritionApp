namespace KurLib;

public class Kursach
{
    public double BMICalulation(double weight, double height)
    {
        double heightMeters = height / 100;
        
        double bmi = weight / (heightMeters * heightMeters);

        return bmi;
    }
    
    public (double protein, double fat, double carbs) MacronutrientsCalculation(double calories)
    {
        double protein = (calories * 0.3) / 4;
        double fat = (calories * 0.3) / 9;
        double carbs = (calories * 0.4) / 4;

        return (protein, fat, carbs);

    }
    
    public double CaloriesCalculation(double activity, double weight, double height, int age, string gender)
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
        string text = $"Норма калорий: {calories} ккал";
        return calories;
    }
}