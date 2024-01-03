using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField]
    float yPos = 30;
    [SerializeField]
    float time = 0.2f;
    Button myButton;
    
    private void Awake()
    {
        //myButton = GetComponent<Button>();
        //myButton.onClick.AddListener(Click);
    }
    bool isClick = false;
    public void Click()
    {
        if(isClick ==false)
        {            
            transform.DOLocalMoveY(transform.localPosition.y - yPos, time).SetEase(Ease.Linear).OnComplete(ClickTweenComplete).From(true);
            isClick = true;
        }
    }
    void ClickTweenComplete()
    {
        isClick = false;
    }
}
