using System;

namespace ProperNutritionApp;

public class FoodDiary
{
    public int DiaryId { get; set; }
    public DateTime Date { get; set; }
    public string Recipe { get; set; }
    public string MealTime { get; set; }
    public int User { get; set; }
}