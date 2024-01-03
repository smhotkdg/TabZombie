using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinPanelController : MonoBehaviour
{
    public enum Type
    {
        hero,
        gun,
        weapon
    }
    public Type m_type = Type.hero;
    public List<GameObject> SkinList;
    public GameObject BuyButton;
    public GameObject CostText;
    public Text InfoText;
    int cost;
    int selectIndex;
    public Image ImageButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }
    
    private void OnEnable()
    {
        BuyButton.GetComponent<Button>().onClick.AddListener(Buy);
        if (m_type == Type.hero)
        {
            Select(GameManager.Instance.SelectHeroIndex,true);            
        }
        else if (m_type == Type.weapon)
        {
            Select(GameManager.Instance.WeaponIndex, true);
        }
        else if (m_type == Type.gun)
        {
            Select(GameManager.Instance.GunIndex, true);
        }
        ImageButton.color = new Color32(75, 75, 75, 255);
        selectIndex = GameManager.Instance.SelectHeroIndex;
    }
    private void OnDisable()
    {
        BuyButton.GetComponent<Button>().onClick.RemoveListener(Buy);
        ImageButton.color = new Color32(255, 255, 255, 255);
    }
    public void Select(int index, bool binit = false)
    {
        BuyButton.gameObject.SetActive(false);
        switch (m_type)
        {
            case Type.hero:
                selectIndex = index;
                if (GameManager.Instance.isOwnHero[index])
                {
                    CostText.SetActive(false);
                    InfoText.gameObject.SetActive(true);
                    BuyButton.gameObject.SetActive(false);
                    GameManager.Instance.SelectHeroIndex = index;
                }
                else
                {
                    CostText.SetActive(true);
                    cost = 200 * index;
                    CostText.transform.Find("Cost_Text").gameObject.GetComponent<Text>().text = (200 * index).ToString("N0");
                    InfoText.gameObject.SetActive(false);
                    BuyButton.gameObject.SetActive(true);
                }
                switch(selectIndex)
                {
                    case 0:
                        InfoText.text = "";
                        break;
                    case 1:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_police");
                        break;
                    case 2:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_doctor");
                        break;
                    case 3:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_soldier");
                        break;
                    case 4:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_firefighter");
                        break;
                    case 5:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_astronaut");
                        break;
                }
                

                break;
            case Type.weapon:
                selectIndex = index;
                if (GameManager.Instance.isOwnWeapon[index])
                {
                    CostText.SetActive(false);
                    InfoText.gameObject.SetActive(true);
                    BuyButton.gameObject.SetActive(false);
                    GameManager.Instance.WeaponIndex = index;
                }
                else
                {
                    CostText.SetActive(true);
                    cost = 200 * index;
                    CostText.transform.Find("Cost_Text").gameObject.GetComponent<Text>().text = (200 * index).ToString("N0");
                    InfoText.gameObject.SetActive(false);
                    BuyButton.gameObject.SetActive(true);
                }
                switch (selectIndex)
                {
                    case 0:
                        InfoText.text = "";
                        break;
                    case 1:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_spear");
                        break;
                    case 2:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_hammer");
                        break;
                    case 3:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_crowbar");
                        break;
                    case 4:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_bat");
                        break;
                    case 5:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_electricsaw");
                        break;
                }
                break;
            case Type.gun:
                selectIndex = index;
                if (GameManager.Instance.isOwnGun[index])
                {
                    CostText.SetActive(false);
                    InfoText.gameObject.SetActive(true);
                    BuyButton.gameObject.SetActive(false);
                    GameManager.Instance.GunIndex = index;
                }
                else
                {
                    CostText.SetActive(true);
                    cost = 200 * index;
                    CostText.transform.Find("Cost_Text").gameObject.GetComponent<Text>().text = (200 * index).ToString("N0");
                    InfoText.gameObject.SetActive(false);
                    BuyButton.gameObject.SetActive(true);
                }
                switch (selectIndex)
                {
                    case 0:
                        InfoText.text = "";
                        break;
                    case 1:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_glock");
                        break;
                    case 2:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_smg");
                        break;
                    case 3:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_shotgun");
                        break;
                    case 4:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_rifle");
                        break;
                    case 5:
                        InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_sniper");
                        break;
                }
                break;
        }        
        for (int i = 0; i < SkinList.Count; i++)
        {
            SkinList[i].GetComponent<SkinItem>().SetInit();
            if (binit)
            {
                switch(m_type)
                {
                    case Type.hero:
                        if (i == GameManager.Instance.SelectHeroIndex)
                        {
                            SkinList[i].GetComponent<SkinItem>().SelectObject.SetActive(true);
                        }
                        break;
                    case Type.weapon:
                        if (i == GameManager.Instance.WeaponIndex)
                        {
                            SkinList[i].GetComponent<SkinItem>().SelectObject.SetActive(true);
                        }
                        break;
                    case Type.gun:
                        if (i == GameManager.Instance.GunIndex)
                        {
                            SkinList[i].GetComponent<SkinItem>().SelectObject.SetActive(true);
                        }
                        break;
                }
                
            }
        }
    }
    public void Buy()
    {
        switch(m_type)
        {
            case Type.hero:
                if(GameManager.Instance.totalCoinCount >= cost)
                {
                    GameManager.Instance.isOwnHero[selectIndex] = true;
                    GameManager.Instance.totalCoinCount -= cost;
                    GameManager.Instance.SelectHeroIndex = selectIndex;
                    Select(selectIndex,true);
                    UIManager.Instance.SetCoinText();
                    Save();
                    switch (selectIndex)
                    {
                        case 0:
                            InfoText.text = "";
                            break;
                        case 1:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_police");
                            break;
                        case 2:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_doctor");
                            break;
                        case 3:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_soldier");
                            break;
                        case 4:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_firefighter");
                            break;
                        case 5:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_astronaut");
                            break;
                    }
                }
                else
                {                    
                    UIManager.Instance.ShowPopupText(UIManager.PopupTextType.Moeny);
                }
                break;
            case Type.weapon:
                if (GameManager.Instance.totalCoinCount >= cost)
                {
                    GameManager.Instance.isOwnWeapon[selectIndex] = true;
                    GameManager.Instance.totalCoinCount -= cost;
                    GameManager.Instance.WeaponIndex = selectIndex;
                    Select(selectIndex, true);
                    UIManager.Instance.SetCoinText();
                    Save();
                    switch (selectIndex)
                    {
                        case 0:
                            InfoText.text = "";
                            break;
                        case 1:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_spear");
                            break;
                        case 2:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_hammer");
                            break;
                        case 3:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_crowbar");
                            break;
                        case 4:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_bat");
                            break;
                        case 5:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_electricsaw");
                            break;
                    }
                }
                else
                {
                    UIManager.Instance.ShowPopupText(UIManager.PopupTextType.Moeny);
                }
                break;
            case Type.gun:
                if (GameManager.Instance.totalCoinCount >= cost)
                {
                    GameManager.Instance.isOwnGun[selectIndex] = true;
                    GameManager.Instance.totalCoinCount -= cost;
                    GameManager.Instance.GunIndex = selectIndex;
                    Select(selectIndex, true);
                    UIManager.Instance.SetCoinText();
                    Save();
                    switch (selectIndex)
                    {
                        case 0:
                            InfoText.text = "";
                            break;
                        case 1:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_glock");
                            break;
                        case 2:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_smg");
                            break;
                        case 3:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_shotgun");
                            break;
                        case 4:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_rifle");
                            break;
                        case 5:
                            InfoText.text = I2.Loc.LocalizationManager.GetTermTranslation("text_sniper");
                            break;
                    }
                }
                else
                {
                    UIManager.Instance.ShowPopupText(UIManager.PopupTextType.Moeny);
                }
                break;
        }
    }
    public void Save()
    {
        ES3.Save<List<bool>>("isOwnHero", GameManager.Instance.isOwnHero);
        ES3.Save<List<bool>>("isOwnWeapon", GameManager.Instance.isOwnWeapon);
        ES3.Save<List<bool>>("isOwnGun", GameManager.Instance.isOwnGun);       
    }
}
