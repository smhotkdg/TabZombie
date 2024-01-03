using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RetryController : MonoBehaviour
{
    public Text BestScore;
    public Text Score;

    public GameObject AdsRetry;
    public GameObject FreeRetry;
    public GameObject CoinRetry;
    //5
    // Start is called before the first frame update

    private void OnEnable()
    {
        CheckData();
    }
    void CheckData()
    {
        if(GameManager.Instance.isRetry ==false)
        {
            if(GameManager.Instance.SelectHeroIndex ==5 || GameManager.Instance.Noads)
            {
                FreeRetry.SetActive(true);
                CoinRetry.SetActive(false);
            }
            else
            {
                FreeRetry.SetActive(false);
                CoinRetry.SetActive(true);
            }
            AdsRetry.GetComponent<Button>().interactable = true;
            FreeRetry.GetComponent<Button>().interactable = true;
            CoinRetry.GetComponent<Button>().interactable = true;
        }
        else
        {
            AdsRetry.GetComponent<Button>().interactable = false;
            FreeRetry.GetComponent<Button>().interactable = false;
            CoinRetry.GetComponent<Button>().interactable = false;
        }
        Score.gameObject.SetActive(true);
        if (GameManager.Instance.isBest)
        {
            BestScore.text = "NEW BEST SCORE! " + GameManager.Instance.HighScore.ToString("N0");
            Score.gameObject.SetActive(false);
        }
        else
        {
            BestScore.text = "BEST SCORE! " + GameManager.Instance.HighScore.ToString("N0");
            Score.text = "SCORE " + GameManager.Instance.NowScore.ToString("N0");
        }
    }

    public void CoinRetryStart()
    {
        if(GameManager.Instance.totalCoinCount >=100)
        {
            GameManager.Instance.continueGame();
            GameManager.Instance.totalCoinCount -= 100;
            UIManager.Instance.SetCoinText();
        }
        else
        {
            UIManager.Instance.ShowPopupText(UIManager.PopupTextType.Moeny);
        }
    }
 
}
