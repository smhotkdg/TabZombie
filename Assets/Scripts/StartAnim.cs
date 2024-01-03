using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    public GameObject StartScreen;
    void GameStart()
    {
        StartScreen.SetActive(false);
        GameManager.Instance.StartGame();
    }
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }
}
