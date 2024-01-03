
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public int zombiePercent =70;
    public int chestPercent = 20;
    [SerializeField]
    float yMargin = -0.23f;
    [SerializeField]
    float xMargin = -0.2f;
    public GameObject Chest;
    public GameObject Coin;
    public GameObject NormalMap;
    public GameObject DisableMap;
    public GameObject ZombieHandMap;
    public GameObject Trap_1;
    public GameObject Rod;
    public GameObject Wood;
    public GameObject Sign;

    public GameObject HighScoreFlag;

    public GameObject NormalZombie;
    public GameObject Hp2Zombie;
    public GameObject PowerZombie;
    public GameObject RangeZombie;
    public GameObject RopeZombie;

    public List<GameObject> NormalMapList = new List<GameObject>();
    List<GameObject> DisableMapList = new List<GameObject>();
    List<GameObject> ZombieHandMapList = new List<GameObject>();
    List<GameObject> Trap_1MapList = new List<GameObject>();
    List<GameObject> RodMapList = new List<GameObject>();
    List<GameObject> WoodMapList = new List<GameObject>();
    List<GameObject> SignMapList = new List<GameObject>();
    List<GameObject> ChestList = new List<GameObject>();

    List<GameObject> NomarZombieList = new List<GameObject>();
    List<GameObject> Hp2ZombieList = new List<GameObject>();
    List<GameObject> PowerZombieList = new List<GameObject>();
    List<GameObject> RangeZombieList = new List<GameObject>();
    List<GameObject> RopeZombieList = new List<GameObject>();

    List<GameObject> CoinList = new List<GameObject>();
    private int m_index = -8;
    private static MapMaker _instance = null;

    public struct ViewMap
    {
        public string name;
        public int index;
        public Vector2 pos;
    }
    public List<ViewMap> viewList = new List<ViewMap>();
    public static MapMaker Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton MapMaker == null");
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
  
    public Dictionary<int, int> weightMonster = new Dictionary<int, int>();
    public Dictionary<int, int> weightMap = new Dictionary<int, int>();
    private void Start()
    {
        LevelChangePoint = 50;
        LevelChangeValue = 2;
        InitMap();
        //weightMonster.Add(0, 0);
        weightMonster.Add(0, 40);
        weightMonster.Add(1, 30);
        weightMonster.Add(2, 20);
        weightMonster.Add(3, 10);
        weightMonster.Add(4, 20);

        //±âº»
        weightMap.Add(0, 80);
        //»ç¶óÁü
        weightMap.Add(1, 40);
        //Á»ºñ¼Õ
        weightMap.Add(2, 5);
        //Æ®·¦1
        weightMap.Add(3, 5);
        //Rod
        weightMap.Add(4, 20);
        //Wood
        weightMap.Add(5, 20);
        //SIgn
        weightMap.Add(6, 15);
        SetInitData();
    }
    List<int> weight_mapList = new List<int>();
    List<int> weight_MonsterList = new List<int>();
    public void SetInitData()
    {
        weight_mapList.Clear();
        weight_MonsterList.Clear();
        Debug.Log("*************************");
        for (int i = 0; i < weightMap.Count; i++)
        {
            weight_mapList.Add(weightMap[i]);
        }
        for(int i =0; i< weightMonster.Count; i++)
        {
            weight_MonsterList.Add(weightMonster[i]);
        }
        
    }
    bool addScore = false;

    public int LevelChangePoint;
    public int LevelChangeValue;
    public int levelChange = 0;
    public void SetWeight()
    {
        if(weight_mapList.Count ==0)
        {
            //±âº»
            weight_mapList.Add(80);
            //»ç¶óÁü
            weight_mapList.Add(40);
            //Á»ºñ¼Õ
            weight_mapList.Add(5);
            //Æ®·¦1
            weight_mapList.Add(5);
            //Rod
            weight_mapList.Add(20);
            //Wood
            weight_mapList.Add(20);
            //SIgn
            weight_mapList.Add(15);
        }
        if(weight_mapList[0] ==0)
        {
            weight_mapList[0] = 80;
            weight_mapList[1] = 40;
            weight_mapList[2] = 5;
            weight_mapList[3] = 5;
            weight_mapList[4] = 20;
            weight_mapList[5] = 20;
            weight_mapList[6] = 15;
        }

        if(weight_MonsterList.Count ==0)
        {
            weight_MonsterList.Add(40);
            weight_MonsterList.Add(30);
            weight_MonsterList.Add(20);
            weight_MonsterList.Add(10);
            weight_MonsterList.Add(20);
        }
        if(weight_MonsterList[0] ==0)
        {
            weight_MonsterList[0]= 40;
            weight_MonsterList[0]= 30;
            weight_MonsterList[0]= 20;
            weight_MonsterList[0]= 10;
            weight_MonsterList[0] = 20;
        }

        if (GameManager.Instance.NowScore <= 1)
        {
            weightMap[0] = weight_mapList[0];
            weightMap[4] = weight_mapList[4];
            weightMap[5] = weight_mapList[5];
            weightMap[1] = weight_mapList[1];
            weightMap[2] = 0;
            weightMap[3] = 0;
            weightMap[6] = 0;

            weightMonster[0] = weight_MonsterList[0];
            weightMonster[1] = 0;
            weightMonster[2] = 0;
            weightMonster[3] = 0;
            weightMonster[4] = 0;
        }
        else if (GameManager.Instance.NowScore > LevelChangePoint)
        {
            
            if (levelChange >= LevelChangeValue)
            {
                //Debug.Log("#######\n¸Ê¸¸µë\n########");
                levelChange = 0;
                if (weightMap[6] < weight_mapList[6])
                {
                    weightMap[6] += 1;
                }
                else if (weightMonster[1] < weight_MonsterList[1])
                {
                    weightMonster[1] += 1;
                }
                else if (weightMap[2] < weight_mapList[2])
                {
                    weightMap[2] += 1;
                }
                else if (weightMonster[4] < weight_MonsterList[4])
                {
                    weightMonster[4] += 1;
                }
                else if (weightMonster[2] < weight_MonsterList[2])
                {
                    weightMonster[2] += 1;
                }
                else if (weightMonster[3] < weight_MonsterList[3])
                {
                    weightMonster[3] += 1;
                }
                else if (weightMap[3] < weight_mapList[3])
                {
                    weightMap[3] += 1;
                }
            }
            levelChange += 1;
        }

    }
    public void SetMap()
    {
        for(int i =0;i< NormalMapList.Count; i++)
        {
            NormalMapList[i].SetActive(false);
            NormalMapList[i].GetComponent<SpriteRenderer>().enabled = true;
            DisableMapList[i].SetActive(false);
            ZombieHandMapList[i].SetActive(false);

            Trap_1MapList[i].SetActive(false);
            RodMapList[i].SetActive(false);
            WoodMapList[i].SetActive(false);
            SignMapList[i].SetActive(false);

            CoinList[i].SetActive(false);

            NomarZombieList[i].SetActive(false);
            Hp2ZombieList[i].SetActive(false);
            PowerZombieList[i].SetActive(false);
            RangeZombieList[i].SetActive(false);
            ChestList[i].SetActive(false);
            RopeZombieList[i].SetActive(false);
        }
        //
        m_index = -8;
        for (int i = 0; i < 16; i++)
        {
            NormalMapList[i].transform.position = new Vector3(m_index, 0);
            NormalMapList[i].transform.localScale = new Vector3(1, 1, 1);
            NormalMapList[i].GetComponent<SpriteRenderer>().enabled = true;
            m_index++;
            NormalMapList[i].SetActive(true);
            AppendMap("normal", m_index,NormalMapList[i].transform.position);
        }
        normalIndex = 0;        
        DisableIndex = 0;
        zombieHandIndex = 0;
        CoinIndex = 0;
        trap_1Index = 0;
        rodIndex = 0;
        woodindex = 0;
        signindex = 0;

        z1_index = 0;
        z2_index = 0;
        z3_index = 0;
        z4_index = 0;
        chestIndex = 0;
        ropeZombieIndex = 0;
        isMakeHigh = false;
        Destroy(HighObject);
        SetWeight();
        levelChange = 0;
    }
    private void InitMap()
    {
        //m_index = -8;
        for (int i =0; i< 50; i++)
        {
            GameObject temp = Instantiate(NormalMap);
            temp.transform.position = new Vector3(0, 0);            
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.name = NormalMap.name;
            //m_index++;
            NormalMapList.Add(temp);
                    
            GameObject DisableTemp = Instantiate(DisableMap);
            DisableTemp.SetActive(false);
            DisableTemp.name = DisableMap.name;
            DisableMapList.Add(DisableTemp);

            GameObject ZombieTemp = Instantiate(ZombieHandMap);
            ZombieTemp.SetActive(false);
            ZombieTemp.name = ZombieHandMap.name;
            ZombieHandMapList.Add(ZombieTemp);

            GameObject mapTemp_1 = Instantiate(Trap_1);
            mapTemp_1.SetActive(false);
            mapTemp_1.name = Trap_1.name;
            Trap_1MapList.Add(mapTemp_1);

            GameObject mapTemp_2 = Instantiate(Rod);
            mapTemp_2.SetActive(false);
            mapTemp_2.name = Rod.name;
            RodMapList.Add(mapTemp_2);

            GameObject mapTemp_3 = Instantiate(Wood);
            mapTemp_3.SetActive(false);
            mapTemp_3.name = Wood.name;
            WoodMapList.Add(mapTemp_3);

            GameObject mapTemp_4 = Instantiate(Sign);            
            mapTemp_4.SetActive(false);
            mapTemp_4.name = Sign.name;
            SignMapList.Add(mapTemp_4);

            GameObject tempCoin = Instantiate(Coin);
            tempCoin.SetActive(false);
            tempCoin.name = i.ToString()+"_coin";
            CoinList.Add(tempCoin);

            GameObject z1 = Instantiate(NormalZombie);
            z1.SetActive(false);
            z1.name = NormalZombie.name;
            NomarZombieList.Add(z1);
            
            GameObject z2 = Instantiate(Hp2Zombie);
            z2.SetActive(false);
            z2.name = Hp2Zombie.name;
            Hp2ZombieList.Add(z2);

            GameObject z3 = Instantiate(PowerZombie);
            z3.SetActive(false);
            z3.name = PowerZombie.name;
            PowerZombieList.Add(z3);

            GameObject z4 = Instantiate(RangeZombie);
            z4.SetActive(false);
            z4.name = RangeZombie.name;
            RangeZombieList.Add(z4);


            GameObject z5 = Instantiate(Chest);
            z5.SetActive(false);            
            ChestList.Add(z5);

            GameObject z6 = Instantiate(RopeZombie);
            z6.SetActive(false);
            z6.name = RopeZombie.name;
            RopeZombieList.Add(z6);
        }
 
    }
    public void GenerateMap()
    {
        //weightMonster[0] += 1;
        //weightMonster[1] -= 1;
        int mapWeight = WeightedRandomizer.From(weightMap).TakeOne();
        switch(mapWeight)
        {
            case 0:
           
                GenerateNormalMap();                
                break;
            case 1:
                bMakeZombie = false;
                GenerateDisableMap();
                break;
            case 2:
                bMakeZombie = false;
                GenerateZombieHandMap();
                break;
            case 3:
                bMakeZombie = false;
                GenerateTraP_1();
                break;
            case 4:
                bMakeZombie = false;
                GenerateRod();
                break;
            case 5:
                bMakeZombie = false;
                GenerateWood();
                break;
            case 6:
                bMakeZombie = false;
                GenerateSign();
                break;
            default:
                GenerateNormalMap();
                break;
        }
        m_index++;
    }

   
    List<Vector2> newMapList = new List<Vector2>();
    void makeNormalRestart(Vector2 pos)
    {
        if(newMapList.Count >0)
        {
            for (int i = 0; i < newMapList.Count; i++)
            {
                if (newMapList[i].x != pos.x && newMapList[i].y != pos.y)
                {
                    newMapList.Add(pos);
                }
            }
        }
        else
        {
            newMapList.Add(pos);
        }
      
    }
    public void makeNormalMap(int index, bool drop = false)
    {
        makeNormalRestartMap();
    }
    void makeNormalRestartMap()
    {
        for (int i = 0; i < NormalMapList.Count; i++)
        {
            NormalMapList[i].SetActive(false);
            NormalMapList[i].GetComponent<SpriteRenderer>().enabled = true;
            DisableMapList[i].SetActive(false);
            ZombieHandMapList[i].SetActive(false);

            Trap_1MapList[i].SetActive(false);
            RodMapList[i].SetActive(false);
            WoodMapList[i].SetActive(false);
            SignMapList[i].SetActive(false);

            CoinList[i].SetActive(false);

            NomarZombieList[i].SetActive(false);
            Hp2ZombieList[i].SetActive(false);
            PowerZombieList[i].SetActive(false);
            RangeZombieList[i].SetActive(false);
            ChestList[i].SetActive(false);

            RopeZombieList[i].SetActive(false);
        }
        //List<ViewMap> tempMap = 
        StartCoroutine(MakeMapRoutine());
    }
    IEnumerator MakeMapRoutine()
    {
        GameManager.Instance.cameraController.ShowCutting(true);
        int MakeCount = 0;
        for (int i = 0; i < viewList.Count; i++)
        {
            Vector2 pos = new Vector2(0, 0);

            pos = viewList[i].pos;
            if (normalIndex >= NormalMapList.Count)
            {
                normalIndex = 0;
            }
            NormalMapList[normalIndex].transform.position = pos;
            beforePos = NormalMapList[normalIndex].transform.position;
            CheckFlag(NormalMapList[normalIndex].transform);
            NormalMapList[normalIndex].SetActive(true);
            NormalMapList[normalIndex].GetComponent<SpriteRenderer>().enabled = false;
            //AppendMap("normal", normalIndex, NormalMapList[normalIndex].transform.position);
            normalIndex++;
            MakeCount++;
        }

        for (int i = 0; i < viewList.Count; i++)
        {
            Vector2 pos = new Vector2(0, 0);

            pos = viewList[i].pos;
            if (normalIndex >= NormalMapList.Count)
            {
                normalIndex = 0;
            }
            NormalMapList[normalIndex].transform.position = pos;
            beforePos = NormalMapList[normalIndex].transform.position;
            CheckFlag(NormalMapList[normalIndex].transform);
            NormalMapList[normalIndex].SetActive(true);
            NormalMapList[normalIndex].GetComponent<SpriteRenderer>().enabled = true;
            if(i < 11)
            {
                GameManager.Instance.SetPlayer(pos);
                yield return new WaitForSeconds(0.12f);
            }
            //EffectController.Instance.ShowEffect(EffectController.EffectType.Roket, pos);
            //AppendMap("normal", normalIndex, NormalMapList[normalIndex].transform.position);
            normalIndex++;         
        }        
        GameManager.Instance.StartcontinueGame();
        GameManager.Instance.cameraController.ShowCutting(false);
    }
    public Vector2 getInitPos()
    {
        if(viewList.Count >5)
        {
            return viewList[4].pos;
        }
        return new Vector2(0, 0);
    }
    private void GenerateSign()
    {
        if (signindex >= SignMapList.Count)
        {
            signindex = 0;
        }
        SignMapList[signindex].SetActive(false);
        SignMapList[signindex].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            SignMapList[signindex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                SignMapList[signindex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                SignMapList[signindex].transform.position = new Vector3(m_index, 0);
            }

        }

        beforePos = SignMapList[signindex].transform.position;
        //isTrap = true;
        MakeCoin(SignMapList[signindex].transform);
        CheckFlag(SignMapList[signindex].transform);
        SignMapList[signindex].SetActive(true);
        AppendMap("sign", signindex, SignMapList[signindex].transform.position);
        signindex++;
    }
    public void InsertMap(int inputIndex,int index,Vector2 pos)
    {
        viewList.RemoveAt(inputIndex);
        ViewMap viewMap = new ViewMap();
        viewMap.name = name;
        viewMap.index = index;
        viewMap.pos = pos;

        viewList.Insert(inputIndex, viewMap);
    }
    private void AppendMap(string name,int index,Vector2 pos)
    {
        ViewMap viewMap = new ViewMap();
        viewMap.name = name;
        viewMap.index = index;
        viewMap.pos = pos;
        if(viewList.Count >=18)
        {
            viewList.RemoveAt(0);
        }

        viewList.Add(viewMap);
    }

    private void GenerateWood()
    {
        if (woodindex >= WoodMapList.Count)
        {
            woodindex = 0;
        }
        WoodMapList[woodindex].SetActive(false);
        WoodMapList[woodindex].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            WoodMapList[woodindex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                WoodMapList[woodindex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                WoodMapList[woodindex].transform.position = new Vector3(m_index, 0);
            }

        }

        beforePos = WoodMapList[woodindex].transform.position;
        //isTrap = true;
        MakeCoin(WoodMapList[woodindex].transform);
        CheckFlag(WoodMapList[woodindex].transform);
        WoodMapList[woodindex].SetActive(true);
        AppendMap("wood", woodindex, WoodMapList[woodindex].transform.position);
        woodindex++;
    }

    private void GenerateRod()
    {
        if (rodIndex >= RodMapList.Count)
        {
            rodIndex = 0;
        }
        RodMapList[rodIndex].SetActive(false);
        RodMapList[rodIndex].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            RodMapList[rodIndex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                RodMapList[rodIndex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                RodMapList[rodIndex].transform.position = new Vector3(m_index, 0);
            }

        }

        beforePos = RodMapList[rodIndex].transform.position;
        //isTrap = true;
        MakeCoin(RodMapList[rodIndex].transform);
        CheckFlag(RodMapList[rodIndex].transform);
        RodMapList[rodIndex].SetActive(true);
        AppendMap("rod", rodIndex, RodMapList[rodIndex].transform.position);
        rodIndex++;
    }

    private void GenerateTraP_1()
    {
        if (isTrap == true)
        {
            GenerateNormalMap();
            return;
        }
        if (trap_1Index >= Trap_1MapList.Count)
        {
            trap_1Index = 0;
        }
        Trap_1MapList[trap_1Index].SetActive(false);
        Trap_1MapList[trap_1Index].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            Trap_1MapList[trap_1Index].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index>0)
            {
                Trap_1MapList[trap_1Index].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                Trap_1MapList[trap_1Index].transform.position = new Vector3(m_index, 0);
            }

        }

        beforePos = Trap_1MapList[trap_1Index].transform.position;
        isTrap = true;
        MakeCoin(Trap_1MapList[trap_1Index].transform);
        CheckFlag(Trap_1MapList[trap_1Index].transform);
        Trap_1MapList[trap_1Index].SetActive(true);
        AppendMap("trap", trap_1Index, Trap_1MapList[trap_1Index].transform.position);
        trap_1Index++;
    }

    public int normalIndex = 0;
    int DisableIndex = 0;
    int zombieHandIndex = 0;

    int trap_1Index=0;
    int rodIndex = 0;
    int woodindex = 0;
    int signindex = 0;
    int ropeZombieIndex = 0;

    int z1_index=0;
    int z2_index = 0;
    int z3_index = 0;
    int z4_index = 0;
    int chestIndex = 0;
    int CoinIndex = 0;
    bool isTrap = false;
    Vector2 beforePos;
    bool bMakeZombie = false;
    public void GenerateNormalMap(Vector3 pos)
    {
        if (normalIndex >= NormalMapList.Count)
        {
            normalIndex = 0;
        }
      
        NormalMapList[normalIndex].transform.position = pos;
        //if(GameManager.Instance.NowScore >100)
        {
            //isTrap = false;
        }
        if (isTrap == false && bMakeZombie == false)
        {
            int rand = Random.Range(0, 100);
            if (rand < zombiePercent)
            {
                int a = WeightedRandomizer.From(weightMonster).TakeOne();

                Vector3 Pos = NormalMapList[normalIndex].transform.position;
                Pos.y = Pos.y + yMargin;
                Pos.x = Pos.x + xMargin;
                MakeZombie(a, Pos);
                bMakeZombie = true;
            }
            else
            {
                int randChest = Random.Range(0, 100);
                if (randChest < chestPercent)
                {
                    if (chestIndex >= ChestList.Count)
                    {
                        chestIndex = 0;
                    }
                    Vector2 chestPos = NormalMapList[normalIndex].transform.position;
                    chestPos.y = 0.94f + chestPos.y;
                    ChestList[chestIndex].SetActive(false);
                    ChestList[chestIndex].transform.position = chestPos;
                    ChestList[chestIndex].SetActive(true);
                    chestIndex++;
                }
                bMakeZombie = false;
            }
        }
        else
        {
            MakeCoin(NormalMapList[normalIndex].transform);
            int randChest = Random.Range(0, 100);
            if (randChest < chestPercent)
            {
                if (chestIndex >= ChestList.Count)
                {
                    chestIndex = 0;
                }
                Vector2 chestPos = NormalMapList[normalIndex].transform.position;
                chestPos.y = 0.94f + chestPos.y;
                //Debug.Log(chestPos);
                ChestList[chestIndex].transform.position = chestPos;
                ChestList[chestIndex].SetActive(true);
                chestIndex++;
            }
        }
        isTrap = false;
        NormalMapList[normalIndex].GetComponent<SpriteRenderer>().enabled = true;
        beforePos = NormalMapList[normalIndex].transform.position;
        CheckFlag(NormalMapList[normalIndex].transform);
        NormalMapList[normalIndex].SetActive(true);
        //AppendMap("normal", normalIndex, NormalMapList[normalIndex].transform.position);
        normalIndex++;
    }
    void GenerateNormalMap()
    {
        if(normalIndex >= NormalMapList.Count)
        {
            normalIndex = 0;
        }
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            NormalMapList[normalIndex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                NormalMapList[normalIndex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                NormalMapList[normalIndex].transform.position = new Vector3(m_index, 0);
            }
            
        }
        //if(GameManager.Instance.NowScore >100)
        {
            //isTrap = false;
        }
        if (isTrap == false && bMakeZombie ==false)
        {
            int rand = Random.Range(0, 100);
            if (rand < zombiePercent)
            {
                int a = WeightedRandomizer.From(weightMonster).TakeOne();

                Vector3 Pos = NormalMapList[normalIndex].transform.position;
                Pos.y = Pos.y + yMargin;
                Pos.x = Pos.x + xMargin;
                MakeZombie(a, Pos);
                bMakeZombie = true;
            }
            else
            {
                int randChest = Random.Range(0, 100);
                if (randChest < chestPercent)
                {
                    if (chestIndex >= ChestList.Count)
                    {
                        chestIndex = 0;                        
                    }
                    Vector2 chestPos = NormalMapList[normalIndex].transform.position;
                    chestPos.y = 0.94f + chestPos.y;
                    
                    ChestList[chestIndex].transform.position = chestPos;
                    ChestList[chestIndex].SetActive(true);
                    chestIndex++;
                }
                bMakeZombie = false;
            }
        }
        else
        {
            MakeCoin(NormalMapList[normalIndex].transform);
            int randChest = Random.Range(0, 100);
            if (randChest < chestPercent)
            {
                if (chestIndex >= ChestList.Count)
                {
                    chestIndex = 0;
                }
                Vector2 chestPos = NormalMapList[normalIndex].transform.position;
                chestPos.y = 0.94f + chestPos.y;
                //Debug.Log(chestPos);
                ChestList[chestIndex].transform.position = chestPos;
                ChestList[chestIndex].SetActive(true);
                chestIndex++;
            }
        }
        isTrap = false;
        NormalMapList[normalIndex].GetComponent<SpriteRenderer>().enabled = true;
        beforePos = NormalMapList[normalIndex].transform.position;
        CheckFlag(NormalMapList[normalIndex].transform);
        NormalMapList[normalIndex].SetActive(true);
        AppendMap("normal", normalIndex, NormalMapList[normalIndex].transform.position);
        normalIndex++;
    }
    void MakeZombie(int index,Vector2 pos)
    {
        switch (index)
        {
            case 0:
                if (z1_index >= NomarZombieList.Count)
                {
                    z1_index = 0;
                }
                NomarZombieList[z1_index].SetActive(true);
                NomarZombieList[z1_index].transform.position = pos;
                z1_index++;
                //Instantiate(NormalZombie, Pos, NormalZombie.transform.localRotation);
                break;
            case 1:
                if (z2_index >= Hp2ZombieList.Count)
                {
                    z2_index = 0;
                }
                Hp2ZombieList[z2_index].SetActive(true);
                Hp2ZombieList[z2_index].transform.position = pos;
                z2_index++;
                break;
            case 2:
                if (z3_index >= PowerZombieList.Count)
                {
                    z3_index = 0;
                }
                PowerZombieList[z3_index].SetActive(true);
                PowerZombieList[z3_index].transform.position = pos;
                z3_index++;
                break;
            case 3:
                if (z4_index >= RangeZombieList.Count)
                {
                    z4_index = 0;
                }
                RangeZombieList[z4_index].SetActive(true);
                RangeZombieList[z4_index].transform.position = pos;
                z4_index++;
                break;
            case 4:
                if(ropeZombieIndex >= RopeZombieList.Count)
                {
                    ropeZombieIndex = 0;
                }
                
                pos.x = pos.x - xMargin;
                pos.y = pos.y - yMargin;
                float initPosy = pos.y;
                pos.y = pos.y + 5f;
                RopeZombieList[ropeZombieIndex].transform.position = pos;
                RopeZombieList[ropeZombieIndex].GetComponent<Monster>().ropeY = pos.y;
                RopeZombieList[ropeZombieIndex].SetActive(true);
                ropeZombieIndex++;
                break;
        }

    }
    void GenerateDisableMap()
    {
        if (DisableIndex >= DisableMapList.Count)
        {
            DisableIndex = 0;
        }
        DisableMapList[DisableIndex].SetActive(false);
        DisableMapList[DisableIndex].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            DisableMapList[DisableIndex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                DisableMapList[DisableIndex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                DisableMapList[DisableIndex].transform.position = new Vector3(m_index, 0);
            }

        }
        int randChest = Random.Range(0, 100);
 
       
        //if (randChest < 20)
        //{
        //    if (chestIndex >= ChestList.Count)
        //    {
        //        chestIndex = 0;
        //    }
        //    Vector2 chestPos = DisableMapList[DisableIndex].transform.position;
        //    chestPos.y = chestPos.y + 1;
        //    ChestList[chestIndex].transform.position = chestPos;
        //    ChestList[chestIndex].SetActive(true);
        //}

        beforePos = DisableMapList[DisableIndex].transform.position;
        isTrap = true;
        DisableMapList[DisableIndex].SetActive(true);
        MakeCoin(DisableMapList[DisableIndex].transform);
        CheckFlag(DisableMapList[DisableIndex].transform);
        AppendMap("disable", DisableIndex, DisableMapList[DisableIndex].transform.position);
        DisableIndex++;
    }

    void GenerateZombieHandMap()
    {
        if(isTrap == true)
        {
            GenerateNormalMap();
            return;
        }
        if (zombieHandIndex >= ZombieHandMapList.Count)
        {
            zombieHandIndex = 0;
        }
        ZombieHandMapList[zombieHandIndex].SetActive(false);
        ZombieHandMapList[zombieHandIndex].SetActive(true);
        float randomY = Random.Range(0, 1f);
        float isSame = Random.Range(0, 1f);
        if (isSame < 0.3f)
        {
            ZombieHandMapList[zombieHandIndex].transform.position = new Vector3(m_index, randomY);
        }
        else
        {
            if (DisableIndex > 0 || normalIndex > 0 || zombieHandIndex > 0 || signindex > 0 || woodindex > 0 || rodIndex > 0 || trap_1Index > 0)
            {
                ZombieHandMapList[zombieHandIndex].transform.position = new Vector3(m_index, beforePos.y);
            }
            else
            {
                ZombieHandMapList[zombieHandIndex].transform.position = new Vector3(m_index, 0);
            }

        }

        beforePos = ZombieHandMapList[zombieHandIndex].transform.position;
        isTrap = true;
        MakeCoin(ZombieHandMapList[zombieHandIndex].transform);
        CheckFlag(ZombieHandMapList[zombieHandIndex].transform);
        ZombieHandMapList[zombieHandIndex].SetActive(true);
        AppendMap("zombiehand", zombieHandIndex, ZombieHandMapList[zombieHandIndex].transform.position);
        zombieHandIndex++;
    }
    bool isMakeHigh = false;
    void MakeCoin(Transform p)
    {
        int rand = Random.Range(0, 10);
        if(rand <3)
        {
            if (CoinIndex >= CoinList.Count)
            {
                CoinIndex = 0;
            }
            CoinList[CoinIndex].transform.SetParent(p);
            CoinList[CoinIndex].transform.localPosition = new Vector3(0, 1, 0);
            CoinList[CoinIndex].SetActive(false);
            
            //CoinList[CoinIndex].GetComponent<DG.Tweening.DOTweenAnimation>().DORestart();
            //StartCoroutine(MakeCoinRoutine(CoinIndex));
            CoinList[CoinIndex].SetActive(true);
            CoinIndex++;            
        }
         
    }
    public void CheckFlag(Transform p)
    {
        if (isMakeHigh == false)
        {
            if(GameManager.Instance.HighScore >10)
            {
                if (p.transform.position.x >= GameManager.Instance.HighScore)
                {
                    Vector2 pos = p.transform.position;
                    pos.x = pos.x + 0.9f;
                    pos.y = pos.y + 1.41f;
                    HighObject = Instantiate(HighScoreFlag);
                    HighObject.transform.position = pos;
                    isMakeHigh = true;
                }
            }         
        }

    }
    GameObject HighObject;
}
