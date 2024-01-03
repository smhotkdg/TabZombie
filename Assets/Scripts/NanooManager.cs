using PlayNANOO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanooManager : MonoBehaviour
{
    Plugin plugin;
    public delegate void AddRankComplete(bool flag);
    public event AddRankComplete AddRankCompleteEventHandler;


    public delegate void GetRanks(List<string> names, List<string> scores);
    public event GetRanks GetRanksEventHandler;

    public delegate void checkNickname(bool flag);
    public event checkNickname checkNicknameEventHandler;

    public delegate void GetPersonalRank(string rank);
    public event GetPersonalRank GetPersonalRankEventHandler;

    public delegate void CompleteNickName(bool flag);
    public event CompleteNickName CompleteNickNameEventHandler;
    private static NanooManager _instance = null;

    public static NanooManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton NanooManager == null");
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
    
    public void GetTotalRank()
    {
        List<string> RankList = new List<string>();
        List<string> ScoreList = new List<string>();
        plugin = Plugin.GetInstance();
        
        plugin.RankingRange("tapzombie-RANK-81B69262-5C99DB71", 1, 50, (status, errorMessage, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                foreach (Dictionary<string, object> item in (ArrayList)values["items"])
                {
                    Debug.Log(item["ranking"]);
                    Debug.Log(item["uuid"]);
                    Debug.Log(item["nickname"]);
                    Debug.Log(item["score"]);
                    Debug.Log(item["data"]);
                    RankList.Add(item["nickname"].ToString());
                    ScoreList.Add(item["score"].ToString());
                }
                GetRanksEventHandler?.Invoke(RankList, ScoreList);
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void RankPersonal()
    {
        plugin = Plugin.GetInstance();
        plugin.RankingPersonal("tapzombie-RANK-81B69262-5C99DB71", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                
                Debug.Log(dictionary["ranking"]);
                Debug.Log(dictionary["data"]);
                Debug.Log(dictionary["total_player"]);
                int rank = int.Parse(dictionary["score"].ToString());
                if( rank > GameManager.Instance.HighScore)
                {
                    GameManager.Instance.HighScore = rank;
                    UIManager.Instance.SetLobbyText();
                }
                GetPersonalRankEventHandler?.Invoke(dictionary["ranking"].ToString());
                
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    void Start()
    {
        plugin = Plugin.GetInstance();
        if(GameManager.Instance.NickName == string.Empty)
        {
            plugin.AccountGuestSignIn((status, errorCode, jsonString, values) => {
                if (status.Equals(Configure.PN_API_STATE_SUCCESS))
                {
                    Debug.Log(values["access_token"].ToString());
                    Debug.Log(values["refresh_token"].ToString());
                    Debug.Log(values["uuid"].ToString());
                    Debug.Log(values["openID"].ToString());
                    Debug.Log(values["nickname"].ToString());
                    Debug.Log(values["linkedID"].ToString());
                    Debug.Log(values["linkedType"].ToString());
                    Debug.Log(values["country"].ToString());
                    if (GameManager.Instance.NickName == string.Empty)
                    {
                        if(values["nickname"].ToString() != "unknown" && values["nickname"].ToString() != "")
                        {
                            GameManager.Instance.NickName = values["nickname"].ToString();
                            AddNickNameInit(GameManager.Instance.NickName);
                        }                        
                        GameManager.Instance.UserId = values["uuid"].ToString();
                        
                        
                    }


                }
                else
                {
                    if (values != null)
                    {
                        if (values["ErrorCode"].ToString() == "30007")
                        {
                            Debug.Log(values["WithdrawalKey"].ToString());
                        }
                        else
                        {
                            Debug.Log("Fail");
                        }
                    }
                    else
                    {
                        Debug.Log("Fail");
                    }
                }
            });
        }
      
    }
    public void AddNickNameInit(string nickName)
    {
        plugin = Plugin.GetInstance();
        plugin.AccountNickanmePut(nickName, false, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                GameManager.Instance.NickName = values["nickname"].ToString();
                CompleteNickNameEventHandler?.Invoke(true);
                RankPersonal();
            }
            else
            {
                if (values != null)
                {
                    if (values["ErrorCode"].ToString() == "30007")
                    {
                        Debug.Log(values["WithdrawalKey"].ToString());
                    }
                    else
                    {
                        Debug.Log("Fail");
                    }
                }
                else
                {
                    Debug.Log("Fail");
                }
                CompleteNickNameEventHandler?.Invoke(false);
            }
        });
    }
    public void AddNickName(string nickName)
    {
        plugin = Plugin.GetInstance();
        plugin.AccountNickanmePut(nickName, false, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                GameManager.Instance.NickName = values["nickname"].ToString();
                CompleteNickNameEventHandler?.Invoke(true);
            }
            else
            {
                if (values != null)
                {
                    if (values["ErrorCode"].ToString() == "30007")
                    {
                        Debug.Log(values["WithdrawalKey"].ToString());
                    }
                    else
                    {
                        Debug.Log("Fail");
                    }
                }
                else
                {
                    Debug.Log("Fail");
                }
                CompleteNickNameEventHandler?.Invoke(false);
            }
        });
    }
    public void MakeNickName(string nickName)
    {
        plugin = Plugin.GetInstance();

        plugin.AccountNicknameExists(nickName, (status, errorCode, jsonString, values) => {
            if (status.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(values["status"].ToString());
                if(values["status"].ToString() == "EXISTS")
                {
                    Debug.Log("ม฿บน");
                    checkNicknameEventHandler?.Invoke(false);
                }
                else
                {
                    //AddNickName(nickName);
                    checkNicknameEventHandler?.Invoke(true);
                }
            }
            else
            {
                if (values != null)
                {
                    if (values["ErrorCode"].ToString() == "30007")
                    {
                        Debug.Log(values["WithdrawalKey"].ToString());
                    }
                    else
                    {
                        Debug.Log("Fail");
                    }
                }
                else
                {
                    Debug.Log("Fail");
                }
            }
        });
    }
    public void AddRank()
    {
        plugin = Plugin.GetInstance();

        plugin.RankingRecord("tapzombie-RANK-81B69262-5C99DB71", GameManager.Instance.HighScore, "{PLAYER_DATA}", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {                
                AddRankCompleteEventHandler?.Invoke(true);
            }
            else
            {
                AddRankCompleteEventHandler?.Invoke(false);
            }
        });
    }
}
