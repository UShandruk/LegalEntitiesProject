using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellChecker
{
    public static class TextChecker
    {
        private static List<string> dictionary;
        private static List<string> originalText;
        private static List<string> spellСheckedText;
        private readonly static string splitter = Environment.NewLine + "===" + Environment.NewLine;

        /// <summary>
        /// Получить исправленный текст, на основе словаря
        /// </summary>
        /// <param name="originalTextAndDictionary">Словарь + исходный текст. Разделитель "=====" на отдельной строке.</param>
        /// <returns></returns>
        public static string GetCorrectedText(string originalTextAndDictionary)
        {
            GetTextAndDictionary(originalTextAndDictionary, splitter);
            spellСheckedText = DoSpellCheck(dictionary, originalText);
            string spellСheckedTextString = String.Join(" ", spellСheckedText.ToArray());
            return spellСheckedTextString;
        }

        /// <summary>
        /// Получить коллекцию слов текста и коллекцию слов словаря
        /// </summary>
        /// <returns></returns>
        public static void GetTextAndDictionary(string originalTextAndDictionary, string splitter)
        {
            List<string> data = Regex.Split(originalTextAndDictionary, splitter).ToList();
            data.Remove("");

            dictionary = data.First().Split(' ').ToList();
            originalText = data.Last().Split(' ').ToList();
        }

        /// <summary>
        /// Выполнить проверку орфографии используя словарь
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<string> DoSpellCheck(List<string> dictionary, List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                if (!dictionary.Contains(text[i]) && text[i] != "")
                {
                    text[i] = FindSimilarWord(text[i]);
                }
            }

            return text;
        }

        /// <summary>
        /// Найти в словаре подходящее слово (расстоянии не более двух правок от ввода)
        /// </summary>
        /// <param name="originalWord"></param>
        /// <returns></returns>
        private static string FindSimilarWord(string originalWord)
        {
            string result = "";
            // для ускорения производительности будем искать только среди слов с такой же длинной +- два символа
            List<string> dictionaryPart = dictionary.Where(w => w.Length >= originalWord.Length - 2 && w.Length <= originalWord.Length + 2).ToList();

            for (int i = 0; i < dictionaryPart.Count; i++)
            {
                int d = FindLevenshteinDistance(originalWord, dictionaryPart[i]);
                if (d <= 2)
                {
                    result = dictionaryPart[i];
                    break;
                }
                else result = originalWord;
            }

            return result;



        }

        /// <summary>
        /// Найти расстояние Левенштейна
        /// https://habr.com/ru/post/331174/
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static Int32 FindLevenshteinDistance(String a, String b)
        { 
            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }
            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;
            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }
            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }
            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[d.GetUpperBound(0), d.GetUpperBound(1)];
        }
    }
}
