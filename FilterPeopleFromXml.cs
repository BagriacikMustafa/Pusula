using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

// --- Fonksiyon Sınıfı ---
public static class XmlPersonFilter
{
    public static string FilterPeopleFromXmlFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Dosya bulunamadı: {filePath}");
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        string xmlData = File.ReadAllText(filePath);
        var doc = XDocument.Parse(xmlData);
        var people = doc.Descendants("Person")
                        .Select(p => new
                        {
                            Name = (string)p.Element("Name"),
                            Age = (int)p.Element("Age"),
                            Department = (string)p.Element("Department"),
                            Salary = (decimal)p.Element("Salary"),
                            HireDate = DateTime.Parse((string)p.Element("HireDate"))
                        })
                        .Where(p => p.Age > 30 && p.Department == "IT" && p.Salary > 5000 && p.HireDate.Year < 2019)
                        .ToList();

        var names = people.Select(p => p.Name).OrderBy(n => n).ToList();
        var totalSalary = people.Sum(p => p.Salary);
        var count = people.Count;
        var averageSalary = count > 0 ? totalSalary / count : 0;
        var maxSalary = people.Any() ? people.Max(p => p.Salary) : 0;

        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}

// --- Top-Level Statements: Dosya Yolu Girişi ---
Console.WriteLine("XML dosya yolunu girin (örnek: C:\\Users\\Mustafa\\people.xml):");
string filePath = Console.ReadLine() ?? "";

// Fonksiyonu çağır ve JSON çıktısını göster
string jsonResult = XmlPersonFilter.FilterPeopleFromXmlFile(filePath);
Console.WriteLine("Sonuç JSON:");
Console.WriteLine(jsonResult);
