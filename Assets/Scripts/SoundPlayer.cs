using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SoundPlayer : MonoBehaviour
{
    public enum type
    {
        button,
        popup
    }


    public bool isStart = false;
    public type m_type = type.button;    
    private void Awake()
    {
        if (m_type == type.button)
        {
            GetComponent<Button>().onClick.AddListener(ClickButton);
        }
    }
    private void OnEnable()
    {
        if (m_type == type.popup)
        {
            //SoundManager.Instance.PlayFx(SoundManager.FxType.Popup);
        }
    } 

    void ClickButton()
    {
        if(isStart)
        {
            SoundManager.Instance.PlayFx(SoundManager.FxType.StartGame);
        }
        else
        {
            SoundManager.Instance.PlayFx(SoundManager.FxType.Button);            
        }
        
    }
}
