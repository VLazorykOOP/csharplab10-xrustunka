using System;

// Виняткові ситуації

// Виняток, який виникає, коли спробуємо побудувати будівлю у неправильному місці
public class InvalidBuildingLocationException : Exception
{
    public InvalidBuildingLocationException(string message) : base(message)
    {
    }
}

// Клас для представлення будівлі
public class Building
{
    public string Name { get; set; }
    public string Location { get; set; }

    public Building(string name, string location)
    {
        Name = name;
        Location = location;
    }

    public override string ToString()
    {
        return $"{Name} розташована у місці {Location}";
    }
}

// Події

// Делегат для події будівництва будівлі
public delegate void BuildingEventHandler(object sender, BuildingEventArgs e);

// Аргументи для події будівництва будівлі
public class BuildingEventArgs : EventArgs
{
    public Building Building { get; }

    // Зробити конструктор публічним
    public BuildingEventArgs(Building building)
    {
        Building = building;
    }
}

// Клас для представлення міста
class City
{
    // Подія будівництва будівлі
    public event BuildingEventHandler BuildingConstructed;

    public void ConstructBuilding(string name, string location)
    {
        Building building = new Building(name, location);

        // Сповіщення про будівництво будівлі
        OnBuildingConstructed(new BuildingEventArgs(building));
    }

    // Метод для сповіщення про будівництво будівлі
    protected virtual void OnBuildingConstructed(BuildingEventArgs e)
    {
        BuildingConstructed?.Invoke(this, e);
    }
}

// Головний клас програми
class Program
{
    static void Main1(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        City city = new City();

        // Підписка на подію будівництва будівлі
        city.BuildingConstructed += (sender, e) => {
            Console.WriteLine($"Будівництво нової будівлі: {e.Building}");
        };

        try
        {
            // Моделюємо будівництво будівлі
            city.ConstructBuilding("Офісний центр", "Центр міста");
            city.ConstructBuilding("Житловий комплекс", "На околиці міста");

            // Моделюємо помилку: спроба побудувати будівлю у неправильному місці
            city.ConstructBuilding("Супермаркет", "На воді");
        }
        catch (InvalidBuildingLocationException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}

