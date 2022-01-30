using UnityEngine;
using System;

public class Localization : MonoBehaviour
{
    private Singleton _singleton = Singleton.GetInstance();

    private void Awake() => SimpleLocalization.Localizator.Initialize();

    public void ChangeLanguage(int languageIndex)
    {
        _singleton.Data.Settings.Language = (Language)languageIndex;
        SimpleLocalization.Localizator.ChangeLanguage((SystemLanguage)Enum.Parse(typeof(SystemLanguage), _singleton.Data.Settings.Language.ToString()));
    }
}
