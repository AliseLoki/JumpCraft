using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class LanguageSwitcher : MonoBehaviour
{
    private const string english = "en";

    [SerializeField] private List<TMP_Text> _textsToTranslate;

    [SerializeField] private List<string> _englishTranslation;
    [SerializeField] private List<string> _russianTranslation;

    private void Awake()
    {
        ChangeLanguage(YG2.lang);
    }

    private void ChangeLanguage(string language)
    {
        if (language == english)
        {
            Translate(_englishTranslation);
        }
        else
        {
            Translate(_russianTranslation);
        }
    }

    private void Translate(List<string> language)
    {   
        int index = 0;

        foreach (var text in _textsToTranslate)
        {
            text.text = language[index].ToString();
            index++;
        }
    }
}
