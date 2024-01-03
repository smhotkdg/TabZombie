using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InappManager : MonoBehaviour
{
    private static InappManager _instance = null;
    public static InappManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton InappManager == null");
            return _instance;
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
        }
    }
    void Start()
    {
        InAppPurchasing.InitializePurchasing();
        InitData();
        isInit = false;
        StartCoroutine(autoInit());
    }
    void InitData()
    {
        if (InAppPurchasing.IsInitialized() == true)
        {
            IAPProduct[] products = InAppPurchasing.GetAllIAPProducts();

            // Print all product names
            foreach (IAPProduct prod in products)
            {

                Debug.Log("Product name: " + prod.Name);
            }
        }
        else
        {
            InAppPurchasing.InitializePurchasing();
        }
    }
    void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed += InAppPurchasing_PurchaseFailed;

        InAppPurchasing.RestoreCompleted += InAppPurchasing_RestoreCompleted;
        InAppPurchasing.RestoreFailed += InAppPurchasing_RestoreFailed;
    }
    private void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed -= InAppPurchasing_PurchaseFailed;

        InAppPurchasing.RestoreCompleted -= InAppPurchasing_RestoreCompleted;
        InAppPurchasing.RestoreFailed -= InAppPurchasing_RestoreFailed;
    }

    private void InAppPurchasing_PurchaseCompleted(IAPProduct obj)
    {
        if (isInit == false)
            return;
        UIManager.Instance.InappProcess.SetActive(false);
        UIManager.Instance.BuySuccessPanel.SetActive(true);
        BuyComplete();
    }

    private void InAppPurchasing_PurchaseFailed(IAPProduct arg1, string arg2)
    {
        if (isInit == false)
            return;
        UIManager.Instance.InappProcess.SetActive(false);
        UIManager.Instance.BuyFailPanel.SetActive(true);
        //Debug.Log("The purchase of product " + product.Name + " has failed.");
    }

    private void InAppPurchasing_RestoreFailed()
    {
        if (isInit == false)
            return;
        UIManager.Instance.InappProcess.SetActive(false);
        UIManager.Instance.RestoreFailPanel.SetActive(true);
        //string tryagainstring = I2.Loc.LocalizationManager.GetTermData("text_ui_RestorationFail").Languages[GameManager.Instance.Language_Type];
        //UIManager.Instance.ShowNotification(tryagainstring);
    }
    private void InAppPurchasing_RestoreCompleted()
    {
        if (isInit == false)
            return;
        UIManager.Instance.InappProcess.SetActive(false);
        UIManager.Instance.RestoreSuccessPanel.SetActive(true);
        BuyComplete();
    }

    public void BuyNoAds()
    {
        if (isInit == false)
            return;
        if (GameManager.Instance.Noads)
        {
            return;
        }
        UIManager.Instance.InappProcess.SetActive(true);
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Noads);
    }
    public void BuyNoadsPopup()
    {
        if (isInit == false)
            return;
        if (GameManager.Instance.Noads)
        {
            return;
        }
        UIManager.Instance.InappProcess.SetActive(true);
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Noads_popup);
    }
    bool isInit = false;
    IEnumerator autoInit()
    {
        yield return new WaitForSeconds(5);
        isInit = true;
    }
    public void BuyComplete()
    {
        if(isInit ==false)
        {
            return;
        }
        GameManager.Instance.Noads = true;
        UIManager.Instance.CheckShop();
        UIManager.Instance.ChangepnlSafe(false);
        AdsManager.Instance.HideBannder();
        GameManager.Instance.totalCoinCount += 2000;
        UIManager.Instance.SetCoinText();
        GameManager.Instance.SpecialOfferTime = 0;
        UIManager.Instance.ChangepnlSafe(true);        
    }

    public void restore()
    {
        if (isInit == false)
            return;
        if (GameManager.Instance.Noads)
        {
            return;
        }
        UIManager.Instance.InappProcess.SetActive(true);
#if UNITY_IOS
        if (InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_Noads) ||InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_Noads_popup))
        {
            UIManager.Instance.InappProcess.SetActive(false);
            InAppPurchasing.RestorePurchases();
        }
        else
        {
            UIManager.Instance.InappProcess.SetActive(false);
            UIManager.Instance.RestoreFailPanel.SetActive(true);
        }
#endif
#if UNITY_ANDROID
        if (InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_Noads) || InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_Noads_popup))
        {
            UIManager.Instance.InappProcess.SetActive(false);
            //여기서 적용시키기
            BuyComplete();
        }
        else
        {
            UIManager.Instance.InappProcess.SetActive(false);
            UIManager.Instance.RestoreFailPanel.SetActive(true);
        }
#endif
    }
}
