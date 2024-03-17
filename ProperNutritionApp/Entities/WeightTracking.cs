using System;

namespace ProperNutritionApp;

public class WeightTracking
{
    public int WeightId { get; set; }
    public int User { get; set; }
    public DateTime Date { get; set; }
    public double Weight { get; set; }
}