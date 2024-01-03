using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EscapeController : MonoBehaviour
{
    Button MyButton;
    private void Start()
    {
        MyButton = GetComponent<Button>();        
    }
    private void OnEnable()
    {
        UIManager.Instance.EscapeCount++;
        UIManager.Instance.AddPanel(this.gameObject);
    } 
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) //뒤로가기 키 입력
        {
            if (UIManager.Instance.GetLast(this.gameObject))
            {
                MyButton.onClick.Invoke();
            }
        }
    }
    private void OnDisable()
    {
        if (UIManager.Instance.gameObject.activeSelf == true)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.EscapeCount--;
                UIManager.Instance.RemovePanel();
            }
        }             
    }
}
