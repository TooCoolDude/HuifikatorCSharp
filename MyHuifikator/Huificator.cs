using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarThunderHuificator;
using static System.Net.Mime.MediaTypeNames;

namespace MyHuifikator;

public static class Huificator
{
    static string allRu = "А-ЯЁа-яё";
    static string vowelsRu = "аеёиоуыэюяАЯЁИОУЫЭЮЯ";
    static Regex subRegex = new Regex("[^" + vowelsRu + "]*([" + vowelsRu + "])");
    public static string Huificate(string inp)
    {
        string input = ReplaceInput(inp);

        return Regex.Replace(input, "[" + allRu + "]+", word =>
        {
            return GetWord(word);
        });
    }
    public static string GetWord(Match word)
    {
        if (ConstStrings.ConstReplacements.ContainsKey(word.Value))
            return ConstStrings.ConstReplacements[word.Value];
        if (word.Value.Length < 4)
            return word.Value;
        Match m = Regex.Match(word.Value, "[^" + vowelsRu + "]*([" + vowelsRu + "])");
        if (!m.Success)
            return word.Value;
        var newWord = word.Value.Substring(m.Value.Length);

        bool isWordUpperCase = word.Value.All(char.IsUpper);

        bool hasO = char.ToLower(m.Value[m.Length - 1]) == 'о';
        bool hasVowels = word.Value.Length > m.Value.Length && vowelsRu.Contains(word.Value[m.Length]);
        if (hasO && hasVowels)
        {
            var sex = GetPartWord(m, true) + newWord;
            return isWordUpperCase ? sex.ToUpper() : sex;
        }

        newWord = GetPartWord(m) + newWord;
        return isWordUpperCase ? newWord.ToUpper() : newWord;
    }
    public static string GetPartWord(Match y, bool doubleVowel = false)
    {
        string rep;
        switch (y.Groups[1].Value.ToLower())
        {
            case "а":
                // шапка → хуяпка
                rep = "я";
                break;
            case "о":
                // опера → хуёпера
                rep = doubleVowel ? "" : "ё";
                break;
            case "у":
                // ушко → хуюшко
                rep = "ю";
                break;
            case "ы":
                // мышка → хуишка
                rep = "и";
                break;
            case "э":
                // эльф → хуельф
                rep = "е";
                break;

            default:
                rep = y.Groups[1].Value.ToLower();
                break;
        }
        bool isUpperCase = char.IsUpper(y.Value[0]);
        return (isUpperCase ? "Ху" : "ху") + rep;
    }
    public static string ReplaceInput(string s)
    {
        var dicks = ConstStrings.ReplacementsPreRegex;
        foreach (string dick in dicks.Keys)
        {
            s = s.Replace(dick, dicks[dick]);
        }
        return s;
    }
}

