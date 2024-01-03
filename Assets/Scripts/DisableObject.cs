using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DisableObject : MonoBehaviour
{
    [SerializeField]
    float disableTime;
    SpriteRenderer m_spriteRender;
    private void Awake()
    {
        m_spriteRender = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(timerRoutine());
    }
    IEnumerator timerRoutine()
    {
        yield return new WaitForSeconds(disableTime);
        m_spriteRender.DOColor(new Color(1, 1, 1, 0.2f), 0.2f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo).OnComplete(CompleteTween);
    }
    void CompleteTween()
    {        
        m_spriteRender.DOColor(new Color(1, 1, 1, 0f), 0.5f).OnComplete(EndRoutine);
    }
    void EndRoutine()
    {
        this.gameObject.SetActive(false);
    }
}
