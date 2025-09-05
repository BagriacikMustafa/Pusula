using System;
using System.Collections.Generic;
using System.Text.Json;

// --- Fonksiyon Sınıfı ---
public static class LongestVowelSubsequenceAsJson
{
    public static string LongestVowelSubsequenceAsJsonFunc(List<string> words)
    {
        if (words == null || words.Count == 0)
            return "[]";

        var vowels = "aeiouAEIOU";
        var result = new List<object>();

        foreach (var word in words)
        {
            string maxSeq = "";
            string currentSeq = "";

            foreach (var c in word)
            {
                if (vowels.Contains(c))
                {
                    currentSeq += c;
                    if (currentSeq.Length > maxSeq.Length)
                        maxSeq = currentSeq;
                }
                else
                {
                    currentSeq = "";
                }
            }

            result.Add(new
            {
                word = word,
                sequence = maxSeq,
                length = maxSeq.Length
            });
        }

        return JsonSerializer.Serialize(result);
    }
}

// --- Top-Level Statements: Giriş ve Çalıştırma ---
Console.WriteLine("Kelime listesini girin (virgül ile ayırın):");
string input = Console.ReadLine() ?? "";

// Virgüllerle ayrılmış kelimeleri listeye çevir
var words = new List<string>();
if (!string.IsNullOrWhiteSpace(input))
{
    words = new List<string>(input.Split(',', StringSplitOptions.RemoveEmptyEntries));
    for (int i = 0; i < words.Count; i++)
        words[i] = words[i].Trim();
}

// Fonksiyonu çağır ve JSON çıktısını göster
string jsonResult = LongestVowelSubsequenceAsJson.LongestVowelSubsequenceAsJsonFunc(words);
Console.WriteLine("Sonuç JSON:");
Console.WriteLine(jsonResult);
