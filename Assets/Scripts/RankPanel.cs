using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class RankPanel : MonoBehaviour
{
    public GameObject MyRank;
    public GameObject RankObject;
    public GameObject MakeNickNamePanel;
    public Text ConfirmText;
    public GameObject Confim;

    public InputField inputField;
    bool bConfrim = false;

    public GameObject DummyRank;
    List<GameObject> DummyList = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        
        NanooManager.Instance.checkNicknameEventHandler += Instance_checkNicknameEventHandler;
        NanooManager.Instance.CompleteNickNameEventHandler += Instance_CompleteNickNameEventHandler;
        NanooManager.Instance.GetPersonalRankEventHandler += Instance_GetPersonalRankEventHandler;
        NanooManager.Instance.GetRanksEventHandler += Instance_GetRanksEventHandler;
        NanooManager.Instance.AddRankCompleteEventHandler += Instance_AddRankCompleteEventHandler;

        DummyRank.SetActive(false);
        
    }

    private void Instance_AddRankCompleteEventHandler(bool flag)
    {        
        NanooManager.Instance.RankPersonal();
    }

    void makeDummy()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject temp = Instantiate(DummyRank, DummyRank.transform.parent);
            temp.transform.localPosition = new Vector3(0, 0, 0);
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.SetActive(false);
            DummyList.Add(temp);
        }
    }
    private void Instance_GetRanksEventHandler(List<string> names, List<string> scores)
    {
        for(int i =0; i< names.Count;i++)
        {
            DummyList[i].SetActive(true);
            int rank = i + 1;
            DummyList[i].transform.Find("Image/Grade").gameObject.GetComponent<Text>().text = rank.ToString("N0");
            DummyList[i].transform.Find("NickName").gameObject.GetComponent<Text>().text = names[i];
            DummyList[i].transform.Find("Score").gameObject.GetComponent<Text>().text = scores[i];
        }
    }

    private void Instance_GetPersonalRankEventHandler(string rank)
    {
        MyRank.transform.Find("Image/Grade").gameObject.GetComponent<Text>().text = rank;
        NanooManager.Instance.GetTotalRank();
    }

    private void OnEnable()
    {
        if(DummyList.Count ==0)
        {
            makeDummy();
        }
        for (int i = 0; i < DummyList.Count; i++)
        {
            DummyList[i].SetActive(false);            
        }
        if (GameManager.Instance.NickName == string.Empty)
        {
            MakeNickNamePanel.SetActive(true);
            MyRank.SetActive(false);
            RankObject.SetActive(false);
        }
        else
        {
            if(GameManager.Instance.NickName != string.Empty)
            {
                NanooManager.Instance.AddRank();
                MyRank.SetActive(true);
                RankObject.SetActive(true);
                GetRankData();
            }
          
            //NanooManager.Instance.GetTotalRank();
            //NanooManager.Instance.RankPersonal();
        }
    }
  
    void GetRankData()
    {
        MyRank.transform.Find("NickName").gameObject.GetComponent<Text>().text = GameManager.Instance.NickName;
        MyRank.transform.Find("Score").gameObject.GetComponent<Text>().text = GameManager.Instance.HighScore.ToString("N0");
        

    }
    private void Instance_CompleteNickNameEventHandler(bool flag)
    {
        bConfrim = false;
        if (flag)
        {
            if (GameManager.Instance.NickName != string.Empty && GameManager.Instance.NickName != "unknown")
            {
                NanooManager.Instance.AddRank();
            }                
            MakeNickNamePanel.SetActive(false);
            Confim.SetActive(false);
            GetRankData();            
            MyRank.SetActive(true);
            RankObject.SetActive(true);
        }
        else
        {
            //Confim.SetActive(false);
            MakeNickNamePanel.SetActive(true);
        }
    }

    private void Instance_checkNicknameEventHandler(bool flag)
    {
        string name = inputField.text;
        bConfrim = false;
        Confim.SetActive(true);
        if (flag == false)
        {
            ConfirmText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_cantSet");
        }
        else
        {
            ConfirmText.text = "["+ name+ "]\n"+I2.Loc.LocalizationManager.GetTermTranslation("text_checkNick");
            bConfrim = true;
        }        
    }
    public void MakeNickName()
    {
        if(bConfrim)
        {
            NanooManager.Instance.AddNickName(name);            
            bConfrim = false;            
        }
        else
        {
            Confim.SetActive(false);
        }
    }
    string name;
    public void SetNickName()
    {
        bConfrim = false;
        name = inputField.text;        
        string idChecker = Regex.Replace(name, @"[ ^0-9a-zA-Z가-힣ㄱ-ㅎㅏ-ㅣぁ-ゔァ-ヴー々〆〤一-龥]{1,10}", "", RegexOptions.Singleline);
        //Debug.Log(idChecker);
        if (idChecker !="")
        {
            Confim.SetActive(true);
            ConfirmText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_cantSet");
        }
        else
        {
            NanooManager.Instance.MakeNickName(name);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
