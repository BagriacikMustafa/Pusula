using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

// --- Fonksiyon Sınıfı ---
public static class EmployeeFilter
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList();

        var names = filtered
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToList();

        var totalSalary = filtered.Sum(e => e.Salary);
        var count = filtered.Count;
        var averageSalary = count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0;
        var minSalary = count > 0 ? filtered.Min(e => e.Salary) : 0;
        var maxSalary = count > 0 ? filtered.Max(e => e.Salary) : 0;

        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}

// --- Top-Level Statements: Statik Tuple Listesi ile Çalıştırma ---
var employees = new List<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)>
{
    ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
    ("Ayşe", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
    ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
};

string jsonResult = EmployeeFilter.FilterEmployees(employees);
Console.WriteLine("Sonuç JSON:");
Console.WriteLine(jsonResult);
