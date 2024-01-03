using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingPanel : MonoBehaviour
{
    public Sprite onImage;
    public Sprite OffImage;
    public Image SoundImage;
    public Text LangeText;
    public bool isTimeScale = true;
    private void OnEnable()
    {
        //LangeText.text = I2.Loc.LocalizationManager.CurrentLanguage;
        setLanguage();
        if (GameManager.Instance.isMute)
        {
            SoundImage.sprite = OffImage;
        }
        else
        {
            SoundImage.sprite = onImage;
        }
        if (isTimeScale)
        {
            Time.timeScale = 0;            
        }
        SoundManager.Instance.SoundChangeEventHandler += Instance_SoundChangeEventHandler;
        GameManager.Instance.LanguageChangeEvnetHandler += Instance_LanguageChangeEvnetHandler;

    }
    void setLanguage()
    {
        string lang = "English";
        switch (I2.Loc.LocalizationManager.CurrentLanguage)
        {
            case "English":
                lang = "English";
                break;
            case "Korean":
                lang = "한국어";
                break;
            case "Japanese":
                lang = "日本語 ";
                break;
            case "Chinese (Simplified)":
                lang = "简体中文";
                break;
            case "Chinese (Traditional)":
                lang = "繁体中文";
                break;
            case "Spanish (Spain)":
                lang = "Español";
                break;
            case "Italian":
                lang = "Italiano ";
                break;
            case "French":
                lang = "Français";
                break;
            case "Portuguese":
                lang = "Português";
                break;
            case "Turkish":
                lang = "Türkçe";
                break;
            case "German":
                lang = "Deutsch";
                break;
            case "Thai":
                lang = "ไทย";
                break;
            default:
                lang = "English";
                break;

        }
        LangeText.text = lang;
    }
    private void Instance_LanguageChangeEvnetHandler()
    {
        setLanguage();
    }

    private void Instance_SoundChangeEventHandler()
    {
        //LangeText.text = I2.Loc.LocalizationManager.CurrentLanguage;
        setLanguage();
        if (GameManager.Instance.isMute)
        {
            SoundImage.sprite = OffImage;
        }
        else
        {
            SoundImage.sprite = onImage;
        }
    }

    private void OnDisable()
    {
        if (isTimeScale)
        {
            Time.timeScale = 1;            
        }
        SoundManager.Instance.SoundChangeEventHandler -= Instance_SoundChangeEventHandler;
        GameManager.Instance.LanguageChangeEvnetHandler-= Instance_LanguageChangeEvnetHandler;

    }
    private void FixedUpdate()
    {
        
             
    }
}
