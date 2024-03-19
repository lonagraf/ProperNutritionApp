using KurLib;

namespace KursachTests;

public class Tests
{
    private Kursach _kursach;
    [SetUp]
    public void Setup()
    {
        _kursach = new Kursach();
    }

    [Test]
    public void BMICalculation_ValidData()
    {
        //arrange
        double height = 160;
        double weight = 50;
        double expected = 19.53;
        
        //act
        double actual = _kursach.BMICalulation(weight, height);

        //assert
        Assert.AreEqual(expected, actual, 0.01);
    }
    
    [Test]
    public void MacronutrientsCalculation_ValidData()
    {
        //arrange
        double calories = 2000;
        
        
        //act
        var (protein, fat, carbs) = _kursach.MacronutrientsCalculation(calories);

        //assert
        Assert.AreEqual(150, protein, 0.01);
        Assert.AreEqual(66.6, fat, 0.01); 
        Assert.AreEqual(200, carbs, 0.01);
    }
}