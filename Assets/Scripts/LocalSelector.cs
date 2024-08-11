using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalSelector : MonoBehaviour
{
    [SerializeField] TTTScriptable scriptable;
    [SerializeField] Image backgroundImage;
    [SerializeField] Text language;
    [SerializeField] Text theme;
    [SerializeField] Button languageBtn;
    [SerializeField] Button themeBtn;
    bool active = false;
    bool toggelTheme = false;
    int idNumber =0;


    private void Start()
    {
        languageBtn.onClick.AddListener(ChangeLocale);
        themeBtn.onClick.AddListener(ChangeTheme);
    }
    private void OnDestroy()
    {
        languageBtn.onClick.RemoveAllListeners();
        themeBtn.onClick.RemoveAllListeners();
    }

    public void ChangeLocale()
    {
        if (active)
            return;
        if(idNumber==1)
        {
            idNumber = 0;
            language.text = scriptable.languageString[0];
        }
        else if(idNumber ==0)
        {
            idNumber = 1;
            language.text = scriptable.languageString[1];
        }
        StartCoroutine(SetLocale(idNumber));
    }

    IEnumerator SetLocale(int _localID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localID];
        active = false;
    }


    public void ChangeTheme()
    {
        if(toggelTheme)
        {
            backgroundImage.color = scriptable.white;
            toggelTheme = false;
            theme.text = scriptable.modeString[0];
        }
        else
        {
            toggelTheme = true;
            backgroundImage.color = scriptable.black;
            theme.text = scriptable.modeString[1];
        }

    }
}
