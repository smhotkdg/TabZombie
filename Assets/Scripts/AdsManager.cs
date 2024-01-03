using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private static AdsManager _instance = null;

    public static AdsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            else
            {
                return _instance;
            }

        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            //
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
        
    IronSourceClientImpl IronSourceClientImpl;
    private void Start()
    {
        AdsSetting();
    }
    void AdsSetting()
    {
        if (!Advertising.IsInitialized())
        {
            Advertising.Initialize();
        }
        Advertising.GrantDataPrivacyConsent(AdNetwork.UnityAds);
        Advertising.RevokeDataPrivacyConsent(AdNetwork.UnityAds);        
        //ConsentStatus moduleConsentUnity = Advertising.GetDataPrivacyConsent(AdNetwork.UnityAds);        
        //UnityAdsClient = Advertising.UnityAdsClient;
        IronSourceClientImpl = Advertising.IronSourceClient;
        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.InterstitialAdCompleted += Advertising_InterstitialAdCompleted;      
        
        IronSourceClientImpl.OnBannerAdScreenPresentedEvent += IronSourceClientImpl_OnBannerAdScreenPresentedEvent;
        ShowBanner();

    }

    private void IronSourceClientImpl_OnBannerAdScreenPresentedEvent()
    {
        isShowBanner = true;
    }

    bool isShowBanner = false;
    private void UnityAdsClient_BannerAdShownCallback(AdPlacement obj)
    {
        isShowBanner = true;
    }
    public void ShowBanner()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        if (GameManager.Instance.Noads)
        {
            isShowBanner = true;
            return;
        }
        //UnityAdsClient.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.Banner);
        IronSourceClientImpl.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.Banner);
        if (isShowBanner == false)
        {
            StartCoroutine(BannerRoutine());
        }
    }
    IEnumerator BannerRoutine()
    {
        yield return new WaitForSeconds(1f);
        ShowBanner();
    }
    public void HideBannder()
    {
        Advertising.HideBannerAd();
    }
    private void Advertising_InterstitialAdCompleted(InterstitialAdNetwork arg1, AdPlacement arg2)
    {
        //Debug.Log("±¤°í Á¾·á");
        GameManager.Instance.totalAdsCount++;
        GameManager.Instance.logData.isAdsComplete = true;
        SoundManager.Instance.MuteSound(false);
        if(GameManager.Instance.SpecialOfferTime >0)
        {
            UIManager.Instance.SpeacialOfferPanel.SetActive(true);
        }
        
    }
    public int RewardType = 0;
    public bool isReawrdContinue = false;
    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        GameManager.Instance.totalAdsCount++;
        if (RewardType ==1)
        {
            GameManager.Instance.totalCoinCount += 100;
            UIManager.Instance.SetCoinText();
            UIManager.Instance.RewardPanel.SetActive(true);
            GameManager.Instance.isAds = true;
            GameManager.Instance.AdsTime = 600;
        }
        else if(RewardType ==2)
        {
            GameManager.Instance.continueGame();
            isReawrdContinue = true;
        }
        SoundManager.Instance.MuteSound(false);
        RewardType = 0;
        //if (GameManager.Instance.SpecialOfferTime > 0)
        //{
        //    UIManager.Instance.SpeacialOfferPanel.SetActive(true);
        //}
    }
    public void PopupAds()
    {
        if(Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd();
            SoundManager.Instance.MuteSound(true);
        }
        
    }
    public void RewardAds(int index)
    {
        if (GameManager.Instance.Noads == false)
        {
            RewardType = index;
            if(Advertising.IsRewardedAdReady())
            {
                Advertising.ShowRewardedAd();
                SoundManager.Instance.MuteSound(true);
            }
            
        }
        else
        {
            if(index ==2)
            {
                GameManager.Instance.continueGame();
            }
            else if(index ==1)
            {
                GameManager.Instance.totalCoinCount += 100;
                UIManager.Instance.SetCoinText();
                UIManager.Instance.RewardPanel.SetActive(true);
                GameManager.Instance.isAds = true;
                GameManager.Instance.AdsTime = 600;
            }            
        }        
    }
}
