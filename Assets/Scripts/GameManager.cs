using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void LanguageChangeEvnet();
    public event LanguageChangeEvnet LanguageChangeEvnetHandler;
    public List<bool> isOwnHero = new List<bool>();
    public List<bool> isOwnWeapon = new List<bool>();
    public List<bool> isOwnGun = new List<bool>();


    public CameraController cameraController;
    private static GameManager _instance = null;
    public List<GameObject> HeroList;
    public MapMaker mapMaker;
    public GameObject StartScreen;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineComponentBase componentBase;
    public float SpecialOfferTime;
    public bool isMute = false;
    public string NickName;
    public int WeaponIndex = 0;
    public int GunIndex = 0;
    public int HighScore = 0;
    public int NowScore = -1;
    public int SelectHeroIndex = 0;
    public string UserId = string.Empty;
    public int GameCount = 0;
    public int totalCoinCount;
    public int Combo = 0;
    public int maxCombo = 0;
    public int LanguageIndex = 0;
    public float totalPlayTime;
    public int totalAdsCount;
    public bool Noads = false;
    /// <summary>
    /// 
    /// </summary>
    public int BulletCount;
    public int Hp;
    public int power;
    public bool DoubleChance = false;
    public bool avoidChance = false;
    //ÀÌ°Å ¸¸µé¾î¾ß ÇÔ
    public bool FreeRestart = false;

    public bool WaveShoot = false;
    public bool brokenObject = false;
    public bool getBulletChance = false;
    public bool LifestealChance = false;
    public bool isFullBulletPower = false;
    public bool instant_death = false;

    public int defaultLife;
    public int defaultBulletCount;
    /// <summary>
    /// 
    /// </summary>
    public string Day;
    [System.Serializable]
    public class LogData
    {
        //public string dateTime;        
        public bool isAdsComplete = false;
        public int Trap_Death_Index = -1;
        public int Score = -1;
        public float PlayTime = 0;
        public int Select_Hero = -1;
        public int Select_Weapon = -1;
        public int Select_Gun = -1;
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
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
    public void InitHero()
    {
        for (int i = 0; i < HeroList.Count; i++)
        {
            HeroList[i].SetActive(false);
        }
    }
    public void ShakeCamera(float time)
    {
        cameraController.Shake(time);
    }
    public PlayerData m_playerData;
    public float AdsTime= 600;
    public bool isAds = false;
    private void Start()
    {
        chestCount = 0;
        SpecialOfferTime = 7200;
        for (int i =0; i< 20; i++)
        {
            isOwnHero.Add(false);
            isOwnGun.Add(false);
            isOwnWeapon.Add(false);
        }
        isMute = false;
        totalCoinCount = 0;
        totalAdsCount = 0;
        totalPlayTime = 0;
        Noads = false;
        NickName = string.Empty;

        List<string> langList = I2.Loc.LocalizationManager.GetAllLanguages();
        for(int i =0; i< langList.Count; i++)
        {
            if(langList[i] == I2.Loc.LocalizationManager.CurrentLanguage)
            {
                LanguageIndex = i;
            }
        }
        

        //SetHero();
        if(ES3.KeyExists("SpecialOfferTime"))
        {
            SpecialOfferTime = ES3.Load<float>("SpecialOfferTime");
        }
        if (ES3.KeyExists("isMute"))
        {
            isMute = ES3.Load<bool>("isMute");
        }
        if(ES3.KeyExists("isAds"))
        {
            isAds = ES3.Load<bool>("isAds");
        }
        if(ES3.KeyExists("AdsTime"))
        {
            AdsTime = ES3.Load<float>("AdsTime");
        }
        if(ES3.KeyExists("Noads"))
        {
            Noads = ES3.Load<bool>("Noads");
        }
        if (ES3.KeyExists("totalCoinCount"))
        {
            totalCoinCount = ES3.Load<int>("totalCoinCount");
        }
        if(ES3.KeyExists("userID"))
        {
            UserId = ES3.Load<string>("userID");
        }        
        else
        {
            //UserId = System.Guid.NewGuid().ToString();
        }     
        if(ES3.KeyExists("LanguageIndex"))
        {
            LanguageIndex = ES3.Load<int>("LanguageIndex");
        }
        if (ES3.KeyExists("Day"))
        {
            Day = ES3.Load<string>("Day");
        }
        if(ES3.KeyExists("SelectHeroIndex"))
        {
            SelectHeroIndex = ES3.Load<int>("SelectHeroIndex");
        }
        if (ES3.KeyExists("WeaponIndex"))
        {
            WeaponIndex = ES3.Load<int>("WeaponIndex");
        }
        if (ES3.KeyExists("GunIndex"))
        {
            GunIndex = ES3.Load<int>("GunIndex");
        }
        if(ES3.KeyExists("HighScore"))
        {
            HighScore = ES3.Load<int>("HighScore");
        }
        if (ES3.KeyExists("totalAdsCount"))
        {
            totalAdsCount = ES3.Load<int>("totalAdsCount");
        }
        if (ES3.KeyExists("totalPlayTime"))
        {
            totalPlayTime= ES3.Load<float>("totalPlayTime");
        }
        if (ES3.KeyExists("maxCombo"))
        {
            maxCombo = ES3.Load<int>("maxCombo");
        }
        if(ES3.KeyExists("isOwnHero"))
        {
            isOwnHero = ES3.Load<List<bool>>("isOwnHero");
        }
        if (ES3.KeyExists("isOwnWeapon"))
        {
            isOwnWeapon = ES3.Load<List<bool>>("isOwnWeapon");
        }
        if (ES3.KeyExists("isOwnGun"))
        {
            isOwnGun = ES3.Load<List<bool>>("isOwnGun");
        }
        if(ES3.KeyExists("NickName"))
        {
            NickName = ES3.Load<string>("NickName");
        }
        isOwnHero[0] = true;
        isOwnGun[0] = true;
        isOwnWeapon[0] = true;
        Debug.Log("UserID =   " +UserId);
        componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        StartScreen.SetActive(true);
        UIManager.Instance.SetBG(StartScreen.transform);
        m_playerData = new PlayerData();
        if(UserId == string.Empty)
        {
            UserId = "nullPlayer";
        }
        m_playerData.m_id = UserId;
        m_playerData.m_dateTime = (System.DateTime.Now.Year).ToString() + (System.DateTime.Now.Month).ToString() + (System.DateTime.Now.Day).ToString();
        if(m_playerData.m_dateTime != Day)
        {
            Day = m_playerData.m_dateTime;
            GameCount = 0;
        }
        else
        {
            if(ES3.KeyExists("GameCount"))
            {
                GameCount = ES3.Load<int>("GameCount");
            }
        }
        //if(GameCount !=0)
        //{
        //    GameCount -= 1;
        //}
        UIManager.Instance.SetCoinText();
        UIManager.Instance.InitCoinText();
        UIManager.Instance.SetLobbyText();
        //LeaderboardDB = new FirebaseDB_Leaderboards();
        SoundManager.Instance.SetSound();
        //Noads = true;
        //totalCoinCount = 50000;
        UIManager.Instance.ChangepnlSafe(Noads);
    }
     
    void SetHero()
    {
        for(int i=0; i < HeroList.Count; i++)
        {
            HeroList[i].SetActive(false);
        }
        HeroList[SelectHeroIndex].SetActive(true);
        HeroList[SelectHeroIndex].transform.position = new Vector3(0.36f, 1.123f,0);
        HeroList[SelectHeroIndex].GetComponent<Bandit>().SetWeapon();
        
    }
    public bool isRetry = false;
    public bool isStartGame = false;

    public void StartGame()
    {
        chestCount = 0;
        if (AdsManager.Instance.isReawrdContinue)
        {
            AdsManager.Instance.isReawrdContinue = false;
            if (GameManager.Instance.SpecialOfferTime > 0)
            {
                UIManager.Instance.SpeacialOfferPanel.SetActive(true);
            }
        }
        EffectController.Instance.DisableEffect();
        HeroList[SelectHeroIndex].GetComponent<Bandit>().setReset();
        virtualCamera.m_Follow = HeroList[SelectHeroIndex].transform;
        if (componentBase is CinemachineFramingTransposer)
        {
            var framingTransposer = componentBase as CinemachineFramingTransposer;            
            framingTransposer.m_DeadZoneHeight = 0;            
        }
        if(GameCount >5)
        {
            if (Noads == false)
            {
                if (GameCount % 3 == 0)
                {
                    AdsManager.Instance.PopupAds();
                }
            }

        }      
        
        
        NowScore = 0;
        Combo = 0;
        LoggingStart();
        mapMaker.SetMap();
        SetHero();
        StartCoroutine(ChangeDeadZone());
        SetHeroData();
        UIManager.Instance.SetLife();
        UIManager.Instance.SetScore();
        UIManager.Instance.SetBullet();
        UIManager.Instance.Recoding();
        isStartGame = true;
        UIManager.Instance.comboInidex = 0;
        UIManager.Instance.SetCoinText();
        //UIManager.Instance.SetLobbyText();
        UIManager.Instance.RetryObject.SetActive(false);
        cameraController.SetWeather();
        GameObject p = GameObject.Find("Bullet_Monster");
        for(int i=0; i< p.transform.childCount; i++)
        {
            Destroy(p.transform.GetChild(i).gameObject);
        }
        cameraController.MangaEffect(false);
        isRetry = false;
        SoundManager.Instance.PlayGame();
        UIManager.Instance.StartGame();
    }

    public void continueGame()
    {
        EffectController.Instance.DisableEffect();
        SoundManager.Instance.PlayFx(SoundManager.FxType.NewGame);
        isRetry = true;
        HeroList[SelectHeroIndex].GetComponent<Bandit>().setReset();
        HeroList[SelectHeroIndex].GetComponent<Animator>().Play("IDLE");
        Vector2 initPos = mapMaker.getInitPos();
        initPos.x = initPos.x + 0.36f;
        initPos.y = initPos.y + 1.225f;
        virtualCamera.m_Follow = HeroList[SelectHeroIndex].transform;
        if (componentBase is CinemachineFramingTransposer)
        {
            var framingTransposer = componentBase as CinemachineFramingTransposer;
            framingTransposer.m_DeadZoneHeight = 0;
        }
        Vector3 pos = HeroList[SelectHeroIndex].transform.position;
        //HeroList[SelectHeroIndex].GetComponent<SpriteRenderer>().enabled = false;
        //HeroList[SelectHeroIndex].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        pos.y = 1.2f;
        StartCoroutine(ChangeDeadZone());
        if(deathType == DeathType.Drop)
        {
            HeroList[SelectHeroIndex].transform.position = initPos;
            mapMaker.makeNormalMap(NowScore,true);
        }
        else
        {
            HeroList[SelectHeroIndex].transform.position = initPos;
            mapMaker.makeNormalMap(NowScore, false);
        }
        UIManager.Instance.Recoding();        
        SetHeroData();
        
        HeroList[SelectHeroIndex].GetComponent<Animator>().Play("IDLE");
        UIManager.Instance.RetryObject.SetActive(false);
        UIManager.Instance.SetLife();
        GameObject p = GameObject.Find("Bullet_Monster");
        for (int i = 0; i < p.transform.childCount; i++)
        {
            Destroy(p.transform.GetChild(i).gameObject);
        }
    }
    public void SetPlayer(Vector2 pos)
    {
        Vector2 initPos = pos;
        pos.x = pos.x + 0.36f;
        pos.y = pos.y + 1.225f;
        HeroList[SelectHeroIndex].GetComponent<Animator>().GetComponent<Bandit>().MoveEvent(pos,initPos);
    }
    bool bStartEffect = false;
    public void MoveFast(int count,int type)
    {
        BuffType = type;
        if (bStartEffect ==false)
        {
            StartCoroutine(PlayerMoveFastRoutine(count));
            bStartEffect = true;
        }
        
    }
    public SpriteRenderer BGspeed;
    public SpriteRenderer BGMask;

    public SpriteRenderer BGspeed_red;
    public SpriteRenderer BGMask_red;
    public int BuffType = 0;
    IEnumerator PlayerMoveFastRoutine(int count)
    {
        SoundManager.Instance.PlayFx(SoundManager.FxType.GoldChange);
        EffectController.Instance.ShowEffect(EffectController.EffectType.shield, HeroList[SelectHeroIndex].transform);
        float initSpeed = HeroList[SelectHeroIndex].GetComponent<Bandit>().flightSpeed;
        float initJump = HeroList[SelectHeroIndex].GetComponent<Bandit>().m_jumpForce;
        HeroList[SelectHeroIndex].GetComponent<Bandit>().isGood = true;
        HeroList[SelectHeroIndex].transform.Find("SpeedTrigger").gameObject.SetActive(true);
        HeroList[SelectHeroIndex].GetComponent<Bandit>().flightSpeed = 0.01f;
        HeroList[SelectHeroIndex].GetComponent<Bandit>().m_jumpForce = 0.01f;
                
        if(BuffType ==0)
        {
            BGspeed.gameObject.SetActive(true);
            BGspeed.color = new Color(1, 1, 1, 0);
            BGMask.color = new Color(0, 0, 0, 0);
        }
        else
        {
            BGspeed_red.gameObject.SetActive(true);
            BGspeed_red.color = new Color(1, 1, 1, 0);
            BGMask_red.color = new Color(0, 0, 0, 0);
        }
        
      
        UIManager.Instance.Block.SetActive(true);
        float alpha = 0;
        float alpha_black = 0;
        for (int i = 0; i < 10; i++)
        {            
            BGspeed.color = new Color(1, 1, 1, alpha);
            BGMask.color = new Color(0, 0, 0, alpha_black);

            BGspeed_red.color = new Color(1, 1, 1, alpha);
            BGMask_red.color = new Color(0, 0, 0, alpha_black);

            alpha += 0.1f;
            alpha_black += 0.078f;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        BGspeed.color = new Color(1, 1, 1, 1);
        BGMask.color = new Color(0, 0, 0, 0.78f);

        BGspeed_red.color = new Color(1, 1, 1, 1);
        BGMask_red.color = new Color(0, 0, 0, 0.78f);

        cameraController.EarthQuake(true);
        
        for (int i = 0; i < count; i++)
        {
            HeroList[SelectHeroIndex].GetComponent<Bandit>().m_grounded = true;
            UIManager.Instance.Move();
            MapMake();
            yield return new WaitForSecondsRealtime(0.1f);
            
        }
        cameraController.EarthQuake(false);
        MapMake();
        yield return new WaitForSecondsRealtime(0.2f);
        HeroList[SelectHeroIndex].GetComponent<Bandit>().flightSpeed = initSpeed;        
        HeroList[SelectHeroIndex].GetComponent<Bandit>().m_jumpForce = initJump;
        HeroList[SelectHeroIndex].GetComponent<Bandit>().isGood = false;
        HeroList[SelectHeroIndex].transform.Find("SpeedTrigger").gameObject.SetActive(false);
        EffectController.Instance.shield_temp.SetActive(false);
        UIManager.Instance.Block.SetActive(false);
        bStartEffect = false;
        BGspeed.gameObject.SetActive(false);
        BGspeed_red.gameObject.SetActive(false);
    }
    public int chestCount;
    void MapMake()
    {
        MapMaker.Instance.GenerateMap();
        int randDust = Random.Range(2, 6);
        for (int i = 0; i < randDust; i++)
        {
            float initX = HeroList[SelectHeroIndex].transform.position.x;
            float initY = HeroList[SelectHeroIndex].transform.position.y - 0.7f;
            float randX = Random.Range(-0.4f, 0.4f);
            float randY = Random.Range(-0.1f, 0.1f);
            EffectController.Instance.ShowEffect(EffectController.EffectType.Dust, new Vector3(initX + randX, initY + randY));
        }    
        NowScore++;
        UIManager.Instance.SetScore();
        MapMaker.Instance.SetWeight();
    }
    public void StartcontinueGame()
    {
        isStartGame = true;
        HeroList[SelectHeroIndex].GetComponent<Animator>().GetComponent<Bandit>().bDeath = false;
        //HeroList[SelectHeroIndex].GetComponent<SpriteRenderer>().enabled = true;
        //HeroList[SelectHeroIndex].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        //HeroList[SelectHeroIndex].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SetHeroData()
    {
        Hp = 3;
        BulletCount = 5;
        power = 1;
        DoubleChance = false;
        avoidChance = false;
        FreeRestart = false;
        brokenObject = false;
        getBulletChance = false;
        LifestealChance = false;
        isFullBulletPower = false;
        instant_death = false;
        WaveShoot = false;
        switch (SelectHeroIndex)
        {
            case 1:
                BulletCount += 1;
                break;
            case 2:
                Hp += 1;
                break;
            case 3:
                DoubleChance = true;
                break;
            case 4:
                avoidChance = true;
                break;
            case 5:
                FreeRestart = true;
                break;
        }
        switch(WeaponIndex)
        {
            case 2:
                brokenObject = true;
                break;
            case 3:
                power += 1;
                break;
            case 4:
                getBulletChance = true;
                break;
            case 5:
                LifestealChance = true;
                break;
        }
        switch(GunIndex)
        {
            case 1:
                BulletCount += 1;
                break;
            case 2:
                isFullBulletPower = true;
                break;
            case 4:
                WaveShoot = true;
                break;
            case 5:
                instant_death = true;
                break;

        }

        defaultLife = Hp;
        defaultBulletCount = BulletCount;
    }
    public bool isBest = false;
    public void DeathPlayer()
    {
        isBest = false;
        SoundManager.Instance.PlayFx(SoundManager.FxType.Death);
        isStartGame = false;
        //StartScreen.SetActive(true);
        
        UIManager.Instance.SetBG(StartScreen.transform);
        virtualCamera.m_Follow = null;
        LoggingJson();
        
        if(NowScore > HighScore)
        {
            HighScore = NowScore;
            isBest = true;
        }
        UIManager.Instance.RetryObject.SetActive(true);
        switch (deathType)
        {
            case DeathType.NoramlZombie:
                Debug.Log("Death = NoramlZombie");
                break;
            case DeathType.Hp2Zombie:
                Debug.Log("Death = Hp2Zombie");
                break;
            case DeathType.PowerZombie:
                Debug.Log("Death = PowerZombie");
                break;
            case DeathType.RangeZombie:
                Debug.Log("Death = RangeZombie");
                break;
            case DeathType.Trap:
                Debug.Log("Death = Trap");
                break;
            case DeathType.RopeZombie:
                Debug.Log("Death = RopeZombie");
                break;
            case DeathType.Rod:
                Debug.Log("Death = Rod");
                break;
            case DeathType.Wood:
                Debug.Log("Death = Wood");
                break;
            case DeathType.Sign:
                Debug.Log("Death = Sign");
                break;
            case DeathType.ZombieHand:
                Debug.Log("Death = ZombieHand");
                break;
            case DeathType.Drop:
                Debug.Log("Death = Drop");
                break;


        }
      
    }
    
    IEnumerator ChangeDeadZone()
    {
        yield return new WaitForSeconds(0.5f);
        if (componentBase is CinemachineFramingTransposer)
        {
            var framingTransposer = componentBase as CinemachineFramingTransposer;
            framingTransposer.m_DeadZoneHeight = 0.3f;

        }
    }
    bool StartLogging = false;
    [System.Serializable]
    public class PlayerData
    {
        public string m_id;
        public string m_dateTime;
        public List<LogData> LogList = new List<LogData>();
        
    }
    
    public void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            ES3.Save("SpecialOfferTime", SpecialOfferTime);
            ES3.Save("isMute", isMute);
            ES3.Save("AdsTime", AdsTime);
            ES3.Save("isAds", isAds);

            ES3.Save("userID", UserId);
            ES3.Save("Day", Day);
            ES3.Save("GameCount", GameCount);
            ES3.Save("SelectHeroIndex", SelectHeroIndex);
            ES3.Save("WeaponIndex", WeaponIndex);
            ES3.Save("GunIndex", GunIndex);
            ES3.Save("HighScore", HighScore);

            ES3.Save("totalAdsCount", totalAdsCount);
            ES3.Save("totalPlayTime", totalPlayTime);
            ES3.Save("maxCombo", maxCombo);
            ES3.Save("totalCoinCount", totalCoinCount);
            ES3.Save("LanguageIndex", LanguageIndex);
            ES3.Save("NickName", NickName);
            ES3.Save("Noads", Noads);
            FirebaseManager.Instance.SetTotal();

        }
    }
    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        ES3.Save("SpecialOfferTime", SpecialOfferTime);
        ES3.Save("isMute", isMute);
        ES3.Save("AdsTime", AdsTime);
        ES3.Save("isAds", isAds);
        ES3.Save("userID", UserId);
        ES3.Save("Day", Day);
        ES3.Save("GameCount", GameCount);
        ES3.Save("SelectHeroIndex", SelectHeroIndex);
        ES3.Save("WeaponIndex", WeaponIndex);
        ES3.Save("GunIndex", GunIndex);
        ES3.Save("HighScore", HighScore);

        ES3.Save("totalAdsCount", totalAdsCount);
        ES3.Save("totalPlayTime", totalPlayTime);
        ES3.Save("maxCombo", maxCombo);
        ES3.Save("totalCoinCount", totalCoinCount);
        ES3.Save("LanguageIndex", LanguageIndex);
        ES3.Save("NickName", NickName);
        ES3.Save("Noads", Noads);
        FirebaseManager.Instance.SetTotal();
#endif
    }

    void LoggingJson()
    {
        StartLogging = false;
        //LogList.Add(logData);
        //m_playerData.LogList.Add(logData);
        logData.Trap_Death_Index = (int)deathType;
        logData.Score = NowScore;
        //logData.PlayTime = float.Parse(logData.PlayTime.ToString("F2"));
        //string toJson = JsonUtility.ToJson(m_playerData);

        //Debug.Log(toJson);
        FirebaseManager.Instance.SetID();
        GameCount++;
    }
    public LogData logData;
    public void LoggingStart()
    {
        StartLogging = true;
        logData = new LogData();
        //logData.dateTime = System.DateTime.Now.ToString();
        
        logData.Score = 0;
        logData.Select_Hero = 0;
        logData.Select_Weapon = WeaponIndex;
        logData.Select_Gun = GunIndex;
        logData.Select_Hero = SelectHeroIndex;
        logData.isAdsComplete = false;
        
    }
    public enum DeathType
    {
        NoramlZombie,
        Hp2Zombie,
        PowerZombie,
        RangeZombie,
        RopeZombie,
        Trap,
        Rod,
        Wood,
        Sign,
        ZombieHand,
        Drop

    }
    public DeathType deathType = DeathType.Drop;
    private void FixedUpdate()
    {
        if(StartLogging)
        {
            logData.PlayTime += Time.deltaTime;
        }
        if(isStartGame)
        {
            totalPlayTime += Time.deltaTime;
        }
        if(isAds)
        {
            AdsTime -= Time.deltaTime;
            if(AdsTime <=0)
            {
                AdsTime = 0;
                isAds = false;
            }
        }
    }    
    public void CheckBulletGet(Vector3 pos)
    {
        if (getBulletChance)
        {
            int rand = Random.Range(0, 100);
            if (rand < 10)
            {
                //if (BulletCount < defaultBulletCount)
                //{
                //    BulletCount += 1;
                //}
                Debug.Log("ÃÑ¾Ë È¹µæ!");
                pos.y = pos.y + 1f;
                EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsBullet,pos,1);
                UIManager.Instance.SetEffectText(UIManager.TextEventType.effect,UIManager.TextType.GetBullet);
            }
        }
    }
    public void CheckHpsteel(Vector3 pos)
    {
        if (LifestealChance)
        {
            int rand = Random.Range(0, 100);
            if (rand < 5)
            {
                if (Hp < defaultLife)
                {
                    Hp += 1;
                    UIManager.Instance.AddLife();
                }
                Debug.Log("ÇÇÈí ¼º°ø!");
                UIManager.Instance.SetEffectText(UIManager.TextEventType.effect, UIManager.TextType.lifeSteal);
                EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, HeroList[SelectHeroIndex].transform.position);
                //EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsLife, pos);
            }
        }
    }
    
    public void ChangeLanguage()
    {
        List<string> langList = I2.Loc.LocalizationManager.GetAllLanguages();
        LanguageIndex++;
        if (LanguageIndex >= langList.Count)
            LanguageIndex = 0;        
        I2.Loc.LocalizationManager.CurrentLanguage = langList[LanguageIndex];
        LanguageChangeEvnetHandler?.Invoke();
    }
}
