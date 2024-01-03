using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;
using DamageNumbersPro;
using DamageNumbersPro.Demo;
using DG.Tweening;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject TutorialMove;
    public GameObject TutorialAttack;
    public GameObject Block;
    public GameObject EscapePanel;
    public GameObject PopupText; 
    public Text Text_SpeacialOffer;
    public Text Text_SpeacialOffer_UI;
    public GameObject SpeacialOfferPanel;
    public GameObject SpeacialOfferButton;
    public RectTransform pnlSafe;
    public GameObject InappProcess;
    public GameObject BuyFailPanel;
    public GameObject BuySuccessPanel;
    public GameObject RestoreFailPanel;
    public GameObject RestoreSuccessPanel;
    public GameObject ShopComplete;

    public Button CoinAdsButton;
    public Text CoinAdsText;
    public GameObject RewardPanel;
    public GameObject RankObject;
    public GameObject RetryObject;
    public Text ShopCoin;
    public Text SkinCoin;
    public GameObject BGObject;
    public TextMeshProUGUI textMeshPro;
    public Text CoinText_Main;
    public Transform CameraTransform;
    public List<DamageNumber> NumberList;
    public Recorder recorder;
    public ClipPlayerUI decodedClipPlayer;
    public Text Score;
    public Text CoinText;
    public Sprite normalLife;
    public Sprite disableLife;

    public Sprite normalBullet;
    public Sprite disableBullet;

    public List<GameObject> LifeList;
    public List<GameObject> BulletList;
    private static UIManager _instance = null;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton UIManager == null");
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
    private void Start()
    {
        
    }
    public void AddLife()
    {
        LifeList[GameManager.Instance.defaultLife - GameManager.Instance.Hp].GetComponent<Image>().sprite = normalLife;
        if (GameManager.Instance.Hp <= 1)
        {
            GameManager.Instance.cameraController.ShowBloodEffect(true);
        }
        else
        {
            GameManager.Instance.cameraController.ShowBloodEffect(false);
        }
        
    }
    public void AddBullet()
    {
        BulletList[GameManager.Instance.defaultBulletCount - GameManager.Instance.BulletCount].GetComponent<Image>().sprite = normalBullet;
    }
    public void SetLife()
    {
        for(int i =0; i< LifeList.Count; i++)
        {
            if(i < GameManager.Instance.Hp)
            {
                LifeList[i].SetActive(true);
                LifeList[i].GetComponent<Image>().sprite = normalLife;
            }
            else
            {
                LifeList[i].SetActive(false);
            }            
        }

        for (int i = 0; i < BulletList.Count; i++)
        {
            if (i < GameManager.Instance.BulletCount)
            {
                BulletList[i].SetActive(true);
                BulletList[i].GetComponent<Image>().sprite = normalBullet;
            }
            else
            {
                BulletList[i].SetActive(false);
            }
        }
        if(GameManager.Instance.Hp <=1)
        {
            GameManager.Instance.cameraController.ShowBloodEffect(true);
        }
        else
        {
            GameManager.Instance.cameraController.ShowBloodEffect(false);
        }

    }
    public void SetDamage()
    {
        for (int i = 0; i < GameManager.Instance.defaultLife - GameManager.Instance.Hp; i++)
        {
            LifeList[i].GetComponent<Image>().sprite = disableLife ;           
        }
        if (GameManager.Instance.Hp <= 1)
        {
            GameManager.Instance.cameraController.ShowBloodEffect(true);
        }
        else
        {
            GameManager.Instance.cameraController.ShowBloodEffect(false);
        }
    }
    bool isStartScoreTween = false;
    public void SetScore()
    {
        Score.text = GameManager.Instance.NowScore.ToString() + " <size=30>m</size>";
        if (isStartScoreTween == false)
        {
            Score.gameObject.transform.DOScale(0.2f, 0.2f).From(true).SetEase(Ease.OutBack).OnComplete(CompleteScoreTween);
            isStartScoreTween = true;
        }

    }
    void CompleteScoreTween()
    {
        isStartScoreTween = false;        
    }
    public void SetBullet()
    {
        for (int i = 0; i < GameManager.Instance.defaultBulletCount - GameManager.Instance.BulletCount; i++)
        {
            BulletList[i].GetComponent<Image>().sprite = disableBullet;
        }
    }
    AnimatedClip GifClip;
    public void Recoding()
    {
        decodedClipPlayer.gameObject.transform.parent.gameObject.SetActive(false);
        decodedClipPlayer.gameObject.SetActive(false);
        Gif.StartRecording(recorder);
        isPlayingGif = false;
    }
    bool isPlayingGif = false;
    public void Stop()
    {
        if(isPlayingGif ==false)
        {
            GifClip = Gif.StopRecording(recorder);

            PlayClipGif();
            isPlayingGif = true;
        }
     
    }
    public void PlayClipGif()
    {
        decodedClipPlayer.gameObject.transform.parent.gameObject.SetActive(true);
        decodedClipPlayer.gameObject.SetActive(true);
        Gif.PlayClip(decodedClipPlayer, GifClip);        
    }

    public void Attack()
    {
        GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].GetComponent<Bandit>().Attack();
        if (GameManager.Instance.GameCount <= 1 && GameManager.Instance.HighScore <10)
        {
            if (attackCount >= 1)
            {                
                TutorialAttack.SetActive(false);
            }

        }
        attackCount++;
    }
    int moveCount;
    int attackCount;
    public void Move()
    {
        GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].GetComponent<Bandit>().Move();
        if (GameManager.Instance.GameCount <= 1 && GameManager.Instance.HighScore < 10)
        {
            if(moveCount >=1)
            {
                TutorialMove.SetActive(false);
                if(attackCount <1)
                {
                    TutorialAttack.SetActive(true);
                }
            }
            
        }
      
        moveCount++;
    }
    public void Gun()
    {
        GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].GetComponent<Bandit>().Shoot();
    }

    public enum TextType
    {
        critical,
        dodge,
        GetBullet,
        lifeSteal
    }
    public enum TextEventType
    {
        effect,
        combo
    }
    int comboCount = 0;
    public void StartGame()
    {
        moveCount = 0;
        attackCount = 0;
        if (GameManager.Instance.GameCount <=1 && GameManager.Instance.HighScore < 10)
        {
            TutorialMove.SetActive(true);
        }
        else
        {
            TutorialMove.SetActive(false);
            TutorialAttack.SetActive(false);
        }
    }
    public void SetEffectText(TextEventType textEventType, TextType textType = TextType.critical)
    {
        if(textEventType == TextEventType.effect)
        {
            DamageNumber prefab = NumberList[0];
            DamageNumber newDamageNumber = prefab.Spawn(GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position);
            string _input = "";
            switch (textType)
            {
                case TextType.critical:
                    _input = "Cri!";
                    break;
                case TextType.dodge:
                    _input = "Dodge!";
                    break;
                case TextType.GetBullet:
                    _input = "+Bullet!";
                    break;
                case TextType.lifeSteal:
                    _input = "+HP!";
                    break;
            }

            prefab.GetComponent<DNP_PrefabSettings>().Apply(newDamageNumber, _input);
        }
        if(textEventType == TextEventType.combo)
        {
            DamageNumber prefab = NumberList[1];            
            //Vector3 pos = GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position;
            //float xRand = Random.Range(-1, 1);
            //pos.x = pos.x + xRand;
            //float yRand = Random.Range(0, 2f);
            //pos.y = pos.y + 2f+yRand;

            DamageNumber newDamageNumber = prefab.Spawn(prefab.transform.position, 1);
            newDamageNumber.gameObject.name = "Combo_" + comboInidex;      
            comboInidex++;
            newDamageNumber.gameObject.SetActive(true);
            newDamageNumber.transform.SetParent(prefab.transform.parent);
            newDamageNumber.transform.localScale = prefab.transform.localScale;
            if (comboInidex ==1)
            {
                GameManager.Instance.Combo = 1;
                if (GameManager.Instance.Combo > GameManager.Instance.maxCombo)
                {
                    GameManager.Instance.maxCombo = GameManager.Instance.Combo;
                }
                Debug.Log("combo = " + GameManager.Instance.Combo);
            }
            DamageNumberGUI temp = newDamageNumber.GetComponent<DamageNumberGUI>();
            temp.GetNumberEventHandler += Temp_GetNumberEventHandler;
            temp.StopEventRoutine += Temp_StopEventRoutine;
        }
    }
    public void CheckShop()
    {
        ShopComplete.SetActive(GameManager.Instance.Noads);
        SpeacialOfferButton.SetActive(false);
        SpeacialOfferPanel.SetActive(false);
    }
    private void Temp_StopEventRoutine()
    {
        GameManager.Instance.Combo = 0;
        if (GameManager.Instance.Combo >= 10)
        {        
        }
        else
        {       
            GameManager.Instance.cameraController.MangaEffect(false);
        }
    }

    private void Temp_GetNumberEventHandler(int maxNumber, GameObject numberObject)
    {
        GameManager.Instance.Combo = maxNumber;
        if (maxNumber > GameManager.Instance.maxCombo)
        {
            GameManager.Instance.maxCombo = maxNumber;
        }
        Debug.Log("combo = " + GameManager.Instance.Combo);
        if (GameManager.Instance.Combo >= 10)
        {
            //numberObject.transform.Find("Fire").gameObject.SetActive(true);
            GameManager.Instance.cameraController.MangaEffect(true);
        }
        else
        {
            //numberObject.transform.Find("Fire").gameObject.SetActive(false);
            GameManager.Instance.cameraController.MangaEffect(false);
        }
    }
    int beforeCoin;
    public void SetCoinText()
    {
        if(beforeCoin != GameManager.Instance.totalCoinCount)
        {
            beforeCoin = GameManager.Instance.totalCoinCount;
            CoinText.text = GameManager.Instance.totalCoinCount.ToString("N0");
            CoinText_Main.text = GameManager.Instance.totalCoinCount.ToString("N0");
            SkinCoin.text = GameManager.Instance.totalCoinCount.ToString("N0");
            ShopCoin.text = GameManager.Instance.totalCoinCount.ToString("N0");
            if (bEffectCoin ==false)
            {
                CoinText.transform.parent.gameObject.transform.DOShakeScale(0.5f, 0.3f);
                SkinCoin.transform.parent.gameObject.transform.DOShakeScale(0.5f, 0.3f);
                ShopCoin.transform.parent.gameObject.transform.DOShakeScale(0.5f, 0.3f);
                CoinText_Main.transform.parent.gameObject.transform.DOShakeScale(0.5f, 0.3f).OnComplete(onCompleteTween);
                bEffectCoin = true;
            }            
        }
       
    }
    public void InitCoinText()
    {
        CoinText.text = GameManager.Instance.totalCoinCount.ToString("N0");
        CoinText_Main.text = GameManager.Instance.totalCoinCount.ToString("N0");
        SkinCoin.text = GameManager.Instance.totalCoinCount.ToString("N0");
        ShopCoin.text = GameManager.Instance.totalCoinCount.ToString("N0");
    }
    bool bEffectCoin = false;
    void onCompleteTween()
    {
        SkinCoin.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
        CoinText.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
        CoinText_Main.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
        ShopCoin.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
        bEffectCoin = false;
    }
    public int comboInidex = 0;
    DamageNumber initCombo;
    
    public void TestBounds(int count)
    {
        Vector2 pos = GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position;
        pos.x = pos.x + 1.5f;
        EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsCoin, pos, count);
    }
    public void SetLobbyText()
    {
        GameManager.Instance.InitHero();
        if(AdsManager.Instance.isReawrdContinue)
        {
            AdsManager.Instance.isReawrdContinue = false;
            if (GameManager.Instance.SpecialOfferTime > 0)
            {
                UIManager.Instance.SpeacialOfferPanel.SetActive(true);
            }
        }
        textMeshPro.text = "<size=40><color=#F0C216><waitfor=0.4>Best Score!</color></size>\n<bounce>" + GameManager.Instance.HighScore+"</bounce>";
        CheckRankObject();
    }
    public void SetBG(Transform panel)
    {
        //BGObject.transform.SetParent(panel);
        //BGObject.transform.SetAsFirstSibling();
    }

    private void FixedUpdate()
    {
        CoinAdsButton.interactable = !GameManager.Instance.isAds;
        if(GameManager.Instance.isAds)
        {
            CoinAdsText.text = GameManager.Instance.AdsTime.ToString("N0") +" sec";
        }
        else
        {
            CoinAdsText.text = "+100 coin";
        }
        GameManager.Instance.SpecialOfferTime -= Time.deltaTime;
        if(GameManager.Instance.SpecialOfferTime <=0)
        {
            GameManager.Instance.SpecialOfferTime =0;
            SpeacialOfferButton.SetActive(false);
            SpeacialOfferPanel.SetActive(false);
        }
        else
        {
            SpeacialOfferButton.SetActive(true);
            TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.SpecialOfferTime);

            string str = time.ToString(@"hh\:mm\:ss");

            Text_SpeacialOffer.text = str;
            Text_SpeacialOffer_UI.text = str;
            //SpeacialOfferPanel.SetActive(false);
        }

      
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) //뒤로가기 키 입력
        {
            if (isEsacapeKey)
            {
                return;
            }
            if (EscapeCount != 0)
            {
                return;
            }
            if (EscapePanel.activeSelf == false)
            {
                EscapePanel.SetActive(true);
            }
            else
            {
                EscapePanel.SetActive(false);
            }


            isEsacapeKey = true;
            StartCoroutine(DisableEscape());
        }
    }
    public void CheckRankObject()
    {
        if(GameManager.Instance.HighScore >1)
        {
            RankObject.SetActive(true);
        }
        else
        {
            RankObject.SetActive(false);
        }
    }    
    public void ChangepnlSafe(bool flag)
    {
        if(flag)
        {            
            pnlSafe.offsetMin = new Vector2(0, pnlSafe.offsetMin.y);
            pnlSafe.offsetMax = new Vector2(0, pnlSafe.offsetMax.y);

            pnlSafe.offsetMax = new Vector2(pnlSafe.offsetMax.x, 0);
            pnlSafe.offsetMin = new Vector2(pnlSafe.offsetMin.x, 0);
        }
        else
        {
            pnlSafe.offsetMin= new Vector2(0, pnlSafe.offsetMin.y);
            pnlSafe.offsetMax = new Vector2(0, pnlSafe.offsetMax.y);

            pnlSafe.offsetMax = new Vector2(pnlSafe.offsetMax.x,0);
            pnlSafe.offsetMin = new Vector2(pnlSafe.offsetMin.x, 70);            
        }        
    }
    public enum PopupTextType
    {
        Moeny,
        ADS,

    }
    bool isPopup = false;
    public void ShowPopupText(PopupTextType popupTextType)
    {
        if (isPopup)
            return;
        PopupText.SetActive(true);
        switch (popupTextType)
        {
            case PopupTextType.Moeny:
                PopupText.transform.Find("Notification").GetComponent<Text>().text = I2.Loc.LocalizationManager.GetTermTranslation("text_CoinPopup");
                break;
        }
        isPopup = true;
        StartCoroutine(PopupEndRoutine());
    }
    IEnumerator PopupEndRoutine()
    {
        yield return new WaitForSeconds(1f);
        isPopup = false;
        PopupText.SetActive(false);
    }

    public List<GameObject> PanelList = new List<GameObject>();
    public void AddPanel(GameObject add)
    {
        PanelList.Add(add);
    }
    public void RemovePanel()
    {
        if (PanelList.Count > 0)
        {
            PanelList.RemoveAt(PanelList.Count - 1);
        }
        isEsacapeKey = true;
        StartCoroutine(CheckZeroPanel());
    }
    IEnumerator CheckZeroPanel()
    {
        yield return new WaitForSeconds(.2f);
        if (EscapeCount == 0)
        {
            isEsacapeKey = false;
        }
    }
    IEnumerator DisableEscape()
    {
        yield return new WaitForSeconds(0.2f);
        isEsacapeKey = false;
    }
    public bool GetLast(GameObject add)
    {
        if (PanelList.Count > 0)
        {
            if (PanelList[PanelList.Count - 1] == add)
            {
                return true;
            }
        }
        return false;
    }
    bool isEsacapeKey = false;
    public int EscapeCount = 0;
    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

}
