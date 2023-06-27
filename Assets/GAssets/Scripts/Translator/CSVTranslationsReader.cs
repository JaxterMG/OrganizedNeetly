using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Core.Localization
{
    public static class CSVTranslationsReader
    {
        private static Dictionary<string, Dictionary<string, string>> Translations;

        private static string CurrentLanguage;
        public static void InitializeTranslations(string filePath)
        {
            //TODO: Узнать язык у системы устройства пользователя
            CurrentLanguage = "English";

            Translations = new Dictionary<string, Dictionary<string, string>>();

            using (var reader = new StreamReader(filePath))
            {
                var languages = reader.ReadLine()?.Split(',')?.Skip(1).ToList();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var wordKey = values[0];
                    var wordTranslations = new Dictionary<string, string>();

                    for (int i = 1; i < values.Length; i++)
                    {
                        wordTranslations.Add(languages?[i - 1], values[i]);
                    }

                    Translations.Add(wordKey, wordTranslations);
                }
            }
        }

        public static string RequestTranslation(string key)
        {
            return RequestTranslation(key, CurrentLanguage);
        }
        public static string RequestTranslation(string key, string language)
        {
            if (Translations == null || Translations.Count == 0)
            {
                Debug.LogError("Null or empty translations dictionary");
                return "Error";
            }

            if (Translations.TryGetValue(key, out var languages))
            {
                if (languages.TryGetValue(language, out var translatedWord))
                {
                    return translatedWord;
                }
            }

            Debug.LogError("No such word or language in translations");
            return "Error";
        }

        public static void ClearTranslations()
        {
            Translations?.Clear();
        }
    }
}
