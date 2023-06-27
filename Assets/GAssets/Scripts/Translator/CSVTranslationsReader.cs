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
            CurrentLanguage = "French";
            TextAsset csvFile = Resources.Load<TextAsset>(filePath);

            if (csvFile == null)
            {
                Debug.LogError("Failed to load CSV file: " + filePath);
                return;
            }

            Translations = new Dictionary<string, Dictionary<string, string>>();

            string[] lines = csvFile.text.Split('\n');

            var languages = lines[0].Split(',').Skip(1).ToList();

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                var wordKey = values[0];
                var wordTranslations = new Dictionary<string, string>();

                for (int j = 1; j < values.Length; j++)
                {
                    wordTranslations.Add(languages[j - 1], values[j]);
                }

                Translations.Add(wordKey, wordTranslations);
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
