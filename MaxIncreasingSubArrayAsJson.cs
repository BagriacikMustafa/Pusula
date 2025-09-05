using System;
using System.Collections.Generic;
using System.Text.Json;

// --- Fonksiyon Sınıfı ---
public static class MaxIncreasingSubArrayAsJson
{
    public static string MaxIncreasingSubarrayAsJsonFunc(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return "[]";

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int> { numbers[0] };
        int maxSum = numbers[0];
        int currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }
                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        if (currentSum > maxSum)
            maxSubarray = currentSubarray;

        return JsonSerializer.Serialize(maxSubarray);
    }
}

// --- Top-Level Statements: Kullanıcı Girişi ve Çalıştırma ---
Console.WriteLine("Tam sayı listesini girin (virgül ile ayırın, örn: 1,2,3,1,2):");
string input = Console.ReadLine() ?? "";

// Virgüllerle ayrılmış tam sayıları listeye çevir
var numbers = new List<int>();
if (!string.IsNullOrWhiteSpace(input))
{
    foreach (var s in input.Split(',', StringSplitOptions.RemoveEmptyEntries))
    {
        if (int.TryParse(s.Trim(), out int n))
            numbers.Add(n);
        else
            Console.WriteLine($"'{s}' geçerli bir sayı değil, atlandı.");
    }
}

// Fonksiyonu çağır ve JSON çıktısını göster
string jsonResult = MaxIncreasingSubArrayAsJson.MaxIncreasingSubarrayAsJsonFunc(numbers);
Console.WriteLine("Sonuç JSON:");
Console.WriteLine(jsonResult);
