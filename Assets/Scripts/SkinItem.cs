using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinItem : MonoBehaviour
{
    public SkinPanelController panelController;
    
    public enum skinType
    {
        hero,
        weapon,
        gun
    }
    public skinType m_skintype = skinType.hero;
    public int Index;

    public Sprite DisableImage;
    public Sprite SelectImage;
    public Sprite UnlockImage;

    Image image;
    GameObject Unlock;
    Image Item;
    public GameObject SelectObject;
    public void Select()
    {
        panelController.Select(Index);
        SelectObject.SetActive(true);
    }
    bool binit = false;
    public void SetInit()
    {
        if(binit ==false)
        {
            image = GetComponent<Image>();
            Unlock = transform.Find("Unlock").gameObject;
            Item = transform.Find("Item").gameObject.GetComponent<Image>();
            SelectObject = transform.Find("Select").gameObject;
        }
        switch (m_skintype)
        {
            case skinType.hero:
                if (GameManager.Instance.isOwnHero[Index] == true)
                {
                    if (GameManager.Instance.SelectHeroIndex == Index)
                    {
                        image.sprite = SelectImage;
                    }
                    else
                    {
                        image.sprite = DisableImage;
                    }
                    Unlock.SetActive(false);
                    Item.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = UnlockImage;
                    Unlock.SetActive(true);
                    Item.color = new Color(0, 0, 0, 1);
                }
                break;
            case skinType.weapon:
                if (GameManager.Instance.isOwnWeapon[Index] == true)
                {
                    if (GameManager.Instance.WeaponIndex == Index)
                    {
                        image.sprite = SelectImage;
                    }
                    else
                    {
                        image.sprite = DisableImage;
                    }
                    Unlock.SetActive(false);
                    Item.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = UnlockImage;
                    Unlock.SetActive(true);
                    Item.color = new Color(0, 0, 0, 1);
                }
                break;
            case skinType.gun:
                if (GameManager.Instance.isOwnGun[Index] == true)
                {
                    if (GameManager.Instance.GunIndex == Index)
                    {
                        image.sprite = SelectImage;
                    }
                    else
                    {
                        image.sprite = DisableImage;
                    }
                    Unlock.SetActive(false);
                    Item.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = UnlockImage;
                    Unlock.SetActive(true);
                    Item.color = new Color(0, 0, 0, 1);
                }
                break;
        }
        SelectObject.SetActive(false);
    }
    private void Awake()
    {
        image = GetComponent<Image>();
        Unlock = transform.Find("Unlock").gameObject;
        Item = transform.Find("Item").gameObject.GetComponent<Image>();
        SelectObject = transform.Find("Select").gameObject;
        binit = true;
    }
    private void OnEnable()
    {
        //SetInit();
      
    }
}
