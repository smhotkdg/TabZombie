using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletPos;
    [SerializeField]
    private float Rotationspeed = 1;
    // Start is called before the first frame update
    List<GameObject> BulletList = new List<GameObject>();
    void Start()
    {
        
    }
 
    public void shoot()
    {
        
        if (GameManager.Instance.BulletCount <=0)
        {
            return;
        }
        SoundManager.Instance.PlayFx(SoundManager.FxType.Gun);
        Vector2 direction = transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * Rotationspeed);
        if (BulletList.Count < 20)
        {
            if (GameManager.Instance.GunIndex == 3)
            {
                Vector2 pos;
                GameObject temp = Instantiate(Bullet);
                temp.name = "Bullet";
                BulletList.Add(temp);
                pos = BulletPos.transform.position;
                pos.y = pos.y - 0.2f;
                temp.transform.position = pos;
                temp.transform.rotation = Quaternion.AngleAxis(-2f, new Vector3(0, 0, 1));

                temp.SetActive(true);


                temp = Instantiate(Bullet);
                BulletList.Add(temp);
                pos = BulletPos.transform.position;
                pos.y = pos.y + 0.2f;
                temp.transform.position = pos;
                temp.transform.rotation = Quaternion.AngleAxis(2f, new Vector3(0, 0, 1));
                temp.SetActive(true);
            }
            else
            {
                GameObject temp = Instantiate(Bullet);
                temp.name = "Bullet";
                BulletList.Add(temp);
                temp.transform.position = BulletPos.transform.position;
                temp.SetActive(true);
            }
            
        }
        else
        {
            ShowBullet();
        }
        GameManager.Instance.BulletCount -= 1;
        UIManager.Instance.SetBullet();

    }
    public void ShootDouble()
    {
        Vector2 pos;
        pos = BulletPos.transform.position;
        pos.y = pos.y - 0.2f;
        BulletList[BulletCount].transform.position = pos;
        BulletList[BulletCount].transform.rotation = Quaternion.AngleAxis(-2f, new Vector3(0, 0, 1));     
        BulletList[BulletCount].SetActive(true);
        BulletCount++;

        pos = BulletPos.transform.position;
        pos.y = pos.y + 0.2f;
        BulletList[BulletCount].transform.position = pos;
        BulletList[BulletCount].transform.rotation = Quaternion.AngleAxis(2f, new Vector3(0, 0, 1));
        BulletList[BulletCount].SetActive(true);
        BulletCount++;
    }
    int BulletCount = 0;
    private float BulletRange = 20;
    void ShowBullet()
    {
        if(BulletCount >= BulletList.Count)
        {
            BulletCount = 0;
        }
        if(GameManager.Instance.GunIndex==3)
        {
            ShootDouble();
        }
        else
        {
            BulletList[BulletCount].transform.position = BulletPos.transform.position;
            BulletList[BulletCount].SetActive(true);
            BulletCount++;
        }
        
    }
}
