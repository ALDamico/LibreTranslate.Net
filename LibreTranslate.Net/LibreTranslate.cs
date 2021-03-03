﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace LibreTranslate.Net
{
    public enum Language
    {
        En, //English
        Ar, //Arabic
        Zh, //Chinese
        Fr, //French
        De, //German
        It, //Italian
        Pt, //Portuguese
        Ru, //Russian
        Es, //Spanish
        None //Placeholder
    }

    public class Translate
    {
        public Translate()
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = "https://libretranslate.com";
            var languages =
                JsonSerializer.Deserialize<List<SupportedLanguage>>(
                    wc.DownloadString("https://libretranslate.com/languages"));
            LanguageList.Add(languages.Any(a => a.Code == "en") ? Language.En : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ar") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "zh") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "fr") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "de") ? Language.De : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "it") ? Language.It : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "pt") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ru") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "es") ? Language.Es : Language.None);
            LanguageList.Remove(Language.None); //just in case
        }

        public Translate(string url)
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = url;
            var languages =
                JsonSerializer.Deserialize<List<SupportedLanguage>>(wc.DownloadString($"{url}/languages"));
            LanguageList.Add(languages.Any(a => a.Code == "en") ? Language.En : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ar") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "zh") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "fr") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "de") ? Language.De : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "it") ? Language.It : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "pt") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ru") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "es") ? Language.Es : Language.None);
            LanguageList.Remove(Language.None);
        }

        private WebClient wc { get; }
        private string Url { get; }
        private List<Language> LanguageList { get; }

        public string TranslateText(Language fromLang, Language toLang, string text)
        {
            if (fromLang == Language.None || toLang == Language.None)
                throw new Exception(
                    "These language structs are not to be used! Take out \"Language.None\" from your code! ");
            if (!LanguageList.Contains(fromLang) || !LanguageList.Contains(toLang)
            ) //if server doesn't support either language
                throw new Exception("Server doesn't support this language!");
            var data =
                $"q={Uri.EscapeDataString(text)}&source={fromLang.ToString().ToLower()}&target={toLang.ToString().ToLower()}";
            wc.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            var response =
                JsonSerializer.Deserialize<TranslationResponse>(wc.UploadString(Url + "/translate", data));
            return response.translatedText;
        }

        public List<Language> SupportedLanguages()
        {
            return LanguageList;
        }
    }

    internal class SupportedLanguage
    {
        public SupportedLanguage(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }

    internal class TranslationResponse
    {
        public string translatedText { get; set; }
    }
}