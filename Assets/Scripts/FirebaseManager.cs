using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.RemoteConfig;
using Firebase.Extensions;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Google.MiniJSON;

public class FirebaseManager : MonoBehaviour
{

    [SerializeField]
    public string testStr = "test";
    private static FirebaseManager _instance = null;
    public static FirebaseManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton FirebaseManager == null");
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
    FirebaseFirestore db;
    FirebaseRemoteConfig remoteConfig;
    // Use this for initialization
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        remoteConfig = FirebaseRemoteConfig.DefaultInstance;        
        FetchData();
    }
    [SerializeField]
    bool isLogging = false;
    void CheckLogging()
    {
        if (isLogging)
        {
            Debug.Log("로그 시작");
        }
        else
        {
            Debug.Log("로그 안함");
        }
    }
    [System.Serializable]
    class weight
    {
        public string m_name { get; set; }
        public int m_weight { get; set; }
    }
 
    private void FetchData()
    {

        remoteConfig.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(task =>
        {
            remoteConfig.ActivateAsync();
            //var isLoggingValue = remoteConfig.GetValue(testStr).StringValue;
            isLogging = remoteConfig.GetValue("logging_value").BooleanValue;
            CheckLogging();

            string json = remoteConfig.GetValue("MapWeight").StringValue;
            var MapWeightvalues = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            foreach (string key in MapWeightvalues.Keys)
            {
                switch (key)
                {
                    case "chestPercnet":
                        MapMaker.Instance.chestPercent = MapWeightvalues[key];
                        break;
                    case "zombiePercnet":
                        MapMaker.Instance.zombiePercent = MapWeightvalues[key];
                        break;
                    case "normal":
                        //Debug.Log("normal = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[0] = MapWeightvalues[key];
                        break;
                    case "disable":
                        //Debug.Log("disable = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[1] = MapWeightvalues[key];
                        break;
                    case "zombiehand":
                        //Debug.Log("zombiehand = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[2] = MapWeightvalues[key];
                        break;
                    case "trap":
                        //Debug.Log("trap = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[3] = MapWeightvalues[key];
                        break;
                    case "rod":
                        //Debug.Log("rod = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[4] = MapWeightvalues[key];
                        break;
                    case "wood":
                        //Debug.Log("wood = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[5] = MapWeightvalues[key];
                        break;
                    case "sign":
                        //Debug.Log("sign = " + MapWeightvalues[key]);
                        MapMaker.Instance.weightMap[6] = MapWeightvalues[key];
                        break;
                    case "levelChangePoint":
                        MapMaker.Instance.LevelChangePoint = MapWeightvalues[key];
                        break;
                    case "levelChangeValue":
                        MapMaker.Instance.LevelChangeValue = MapWeightvalues[key];
                        break;
                }
            }


            string json_Monster = remoteConfig.GetValue("MonsterWeight").StringValue;
            var MonsterWeightvalues =JsonConvert.DeserializeObject<Dictionary<string, int>>(json_Monster);
            foreach (string key in MonsterWeightvalues.Keys)
            {
                switch (key)
                {
                    case "normal":
                        //Debug.Log("normal = " + MonsterWeightvalues[key]);
                        MapMaker.Instance.weightMonster[0] = MonsterWeightvalues[key];
                        break;
                    case "hp2":
                        //Debug.Log("hp2 = " + MonsterWeightvalues[key]);
                        MapMaker.Instance.weightMonster[1] = MonsterWeightvalues[key];
                        break;
                    case "power":
                        //Debug.Log("power = " + MonsterWeightvalues[key]);
                        MapMaker.Instance.weightMonster[2] = MonsterWeightvalues[key];
                        break;
                    case "range":
                        //Debug.Log("range = " + MonsterWeightvalues[key]);
                        MapMaker.Instance.weightMonster[3] = MonsterWeightvalues[key];
                        break;
                    case "rope":
                        MapMaker.Instance.weightMonster[4] = MonsterWeightvalues[key];
                        break;      
                }
            }
            //Debug.Log(testStr+" =  " + isLoggingValue);
        });

        //string jsonString = "{\"normal\":50,\"disable\":60,\"zombiehand\":20,\"trap\":5,\"rod\":20,\"wood\":20,\"sign\":15}";        
        //var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);
        //Debug.Log(values);
        //Debug.Log(jsonString);
        MapMaker.Instance.SetInitData();
    }
  


    public void SetID()
    {       
        if(isLogging == false)
        {
            return;
        }

        DocumentReference docRef = db.Collection("Users").Document(GameManager.Instance.UserId).Collection(GameManager.Instance.m_playerData.m_dateTime).
            Document(GameManager.Instance.GameCount.ToString());
        Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "isAdsComplete", GameManager.Instance.logData.isAdsComplete},
            { "Death_Trap", GameManager.Instance.logData.Trap_Death_Index},
            { "Score", GameManager.Instance.logData.Score},
            { "PlayTime", GameManager.Instance.logData.PlayTime},
            { "SelectHero", GameManager.Instance.logData.Select_Hero},
            { "Weapon", GameManager.Instance.logData.Select_Weapon},
            { "Gun", GameManager.Instance.logData.Select_Gun},
            { "HighScore", GameManager.Instance.HighScore}
        };
        docRef.SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread(task =>
        {
            Debug.Log("We’ve arrived!");
        });

    }
    public void SetTotal()
    {
        if (isLogging == false)
        {
            return;
        }
        DocumentReference docRef = db.Collection("Users").Document(GameManager.Instance.UserId);
        Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "TotalPlayTime", GameManager.Instance.totalPlayTime},
            { "TotalAdsCount", GameManager.Instance.totalAdsCount},
            {"NickName",GameManager.Instance.NickName }
           
        };
        docRef.SetAsync(update, SetOptions.MergeAll);
    }
}
