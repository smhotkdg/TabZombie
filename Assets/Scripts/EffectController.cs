using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private static EffectController _instance = null;

    public static EffectController Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton EffectController == null");
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
    public enum EffectType
    {
        o_Effect,
        Dust,
        Death,
        Heal,
        BoundsCoin,
        BoundsLife,
        BoundsBullet,
        Dust_1,
        Dust_2,
        shield,
        goldLife,
        goldStar
        //Roket
    }
    //public GameObject RoketYellow;
    public GameObject GoldLIfe;
    public GameObject GoldStar;
    public GameObject BoundsCoin;
    public GameObject O_Effect;
    public GameObject Dust;
    public GameObject DeathEffect;
    public GameObject Heal;
    public GameObject Dust_1;
    public GameObject Dust_2;
    public GameObject BoundsBullet;
    public GameObject BoundsLIfe;
    public GameObject shield;

    List<GameObject> GoldLIfeList = new List<GameObject>();
    List<GameObject> GoldStarList = new List<GameObject>();

    List<GameObject> OEffectList = new List<GameObject>();
    List<GameObject> DustList = new List<GameObject>();
    List<GameObject> DeathEffectList = new List<GameObject>();
    List<GameObject> HealEffectList = new List<GameObject>();
    List<GameObject> BoundsCoinList = new List<GameObject>();

    List<GameObject> BoundsLifeList = new List<GameObject>();
    List<GameObject> BoundsBulletList = new List<GameObject>();
    int oEffectCount =0;
    int DustCount = 0;
    int DeathCount = 0;
    int healCount=0;
    int BoundsCount = 0;
    int BoundsBulletCount = 0;
    int BoundsLifeCount = 0;
    int GoldLifeCount = 0;
    int GoldStartCount = 0;
    private void Start()
    {
        for(int i =0; i< 10; i++)
        {
            GameObject temp = Instantiate(O_Effect);
            temp.SetActive(false);
            OEffectList.Add(temp);

            GameObject tempDust = Instantiate(Dust);
            Dust.SetActive(false);
            DustList.Add(tempDust);

            GameObject tempDeath = Instantiate(DeathEffect);
            tempDeath.SetActive(false);            
            DeathEffectList.Add(tempDeath);

            GameObject tempHeal = Instantiate(Heal);
            tempHeal.SetActive(false);            
            HealEffectList.Add(tempHeal);

            GameObject BoundLifeGold = Instantiate(GoldLIfe);
            BoundLifeGold.SetActive(false);
            GoldLIfeList.Add(BoundLifeGold);

            GameObject BoundStar = Instantiate(GoldStar);
            BoundStar.SetActive(false);
            GoldStarList.Add(BoundStar);

        }
        for(int i=0; i< 30;i++)
        {
            GameObject temp = Instantiate(BoundsCoin);
            temp.SetActive(false);
            BoundsCoinList.Add(temp);

            GameObject tempLife = Instantiate(BoundsLIfe);
            tempLife.SetActive(false);
            BoundsLifeList.Add(tempLife);

            GameObject tempBullet = Instantiate(BoundsBullet);
            tempBullet.SetActive(false);
            BoundsBulletList.Add(tempBullet);
        }
        DustCount = 0;
        oEffectCount = 0;
        DeathCount = 0;
        healCount = 0;
        BoundsCount = 0;

        BoundsLifeCount = 0;
        BoundsBulletCount = 0;
        GoldStartCount = 0;
        GoldLifeCount = 0;
        shield_temp = Instantiate(shield);
        shield_temp.SetActive(false);
    }
    public GameObject shield_temp;
    [SerializeField]
    float max_x = 300;
    [SerializeField]
    float max_y = 500;
    [SerializeField]
    float torque = 10;

    public void DisableEffect()
    {
        for(int i =0; i< GoldLIfeList.Count; i++)
        {
            GoldLIfeList[i].SetActive(false);
            GoldStarList[i].SetActive(false);
        }
    }
    
    IEnumerator BoundsCointRoutine(Vector3 pos,int count)
    {
        for (int i = 0; i < count; i++)
        {            
            if (BoundsCount >= BoundsCoinList.Count)
                BoundsCount = 0;
            BoundsCoinList[BoundsCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            BoundsCoinList[BoundsCount].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            BoundsCoinList[BoundsCount].transform.localPosition = new Vector3(0, 0, 0);
            BoundsCoinList[BoundsCount].transform.rotation = new Quaternion(0, 0, 0, 0);


            BoundsCoinList[BoundsCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            BoundsCoinList[BoundsCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            BoundsCoinList[BoundsCount].SetActive(true);
            BoundsCoinList[BoundsCount].transform.position = pos;
            float randx = Random.Range(-max_x / 2, max_x);
            float randy = Random.Range(200, max_y);
            float randTorque = Random.Range(0, torque);
            BoundsCoinList[BoundsCount].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            BoundsCoinList[BoundsCount].GetComponent<Rigidbody2D>().AddTorque(randTorque);
            BoundsCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator BoundsLifeRoutine(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (BoundsLifeCount >= BoundsLifeList.Count)
                BoundsLifeCount = 0;
            BoundsLifeList[BoundsLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            BoundsLifeList[BoundsLifeCount].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            BoundsLifeList[BoundsLifeCount].transform.localPosition = new Vector3(0, 0, 0);
            BoundsLifeList[BoundsLifeCount].transform.rotation = new Quaternion(0, 0, 0, 0);


            BoundsLifeList[BoundsLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            BoundsLifeList[BoundsLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            BoundsLifeList[BoundsLifeCount].SetActive(true);
            BoundsLifeList[BoundsLifeCount].transform.position = pos;
            float randx = Random.Range(-max_x / 2, max_x);
            float randy = Random.Range(200, max_y);
            float randTorque = Random.Range(0, torque);
            BoundsLifeList[BoundsLifeCount].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            BoundsLifeList[BoundsLifeCount].GetComponent<Rigidbody2D>().AddTorque(randTorque);
            BoundsLifeCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator BoundsBulletRoutine(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (BoundsBulletCount >= BoundsBulletList.Count)
                BoundsBulletCount = 0;
            BoundsBulletList[BoundsBulletCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            BoundsBulletList[BoundsBulletCount].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            BoundsBulletList[BoundsBulletCount].transform.localPosition = new Vector3(0, 0, 0);
            BoundsBulletList[BoundsBulletCount].transform.rotation = new Quaternion(0, 0, 0, 0);


            BoundsBulletList[BoundsBulletCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            BoundsBulletList[BoundsBulletCount].SetActive(true);
            BoundsBulletList[BoundsBulletCount].transform.position = pos;
            float randx = Random.Range(-max_x / 2, max_x);
            float randy = Random.Range(200, max_y);
            float randTorque = Random.Range(0, torque);
            BoundsBulletList[BoundsBulletCount].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            BoundsBulletList[BoundsBulletCount].GetComponent<Rigidbody2D>().AddTorque(randTorque);
            BoundsBulletCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator BoundsGoldLIfeRoutine(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (GoldLifeCount >= GoldLIfeList.Count)
                GoldLifeCount = 0;
            GoldLIfeList[GoldLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GoldLIfeList[GoldLifeCount].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GoldLIfeList[GoldLifeCount].transform.localPosition = new Vector3(0, 0, 0);
            GoldLIfeList[GoldLifeCount].transform.rotation = new Quaternion(0, 0, 0, 0);


            GoldLIfeList[GoldLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GoldLIfeList[GoldLifeCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GoldLIfeList[GoldLifeCount].SetActive(true);
            GoldLIfeList[GoldLifeCount].transform.position = pos;
            float randx = Random.Range(-max_x / 2, max_x);
            float randy = Random.Range(200, max_y);
            float randTorque = Random.Range(0, torque);
            GoldLIfeList[GoldLifeCount].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            GoldLIfeList[GoldLifeCount].GetComponent<Rigidbody2D>().AddTorque(randTorque);
            GoldLifeCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator BoundsGoldStarRoutine(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (GoldStartCount >= GoldStarList.Count)
                GoldStartCount = 0;
            GoldStarList[GoldStartCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GoldStarList[GoldStartCount].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GoldStarList[GoldStartCount].transform.localPosition = new Vector3(0, 0, 0);
            GoldStarList[GoldStartCount].transform.rotation = new Quaternion(0, 0, 0, 0);


            GoldStarList[GoldStartCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GoldStarList[GoldStartCount].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GoldStarList[GoldStartCount].SetActive(true);
            GoldStarList[GoldStartCount].transform.position = pos;
            float randx = Random.Range(-max_x / 2, max_x);
            float randy = Random.Range(200, max_y);
            float randTorque = Random.Range(0, torque);
            GoldStarList[GoldStartCount].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            GoldStarList[GoldStartCount].GetComponent<Rigidbody2D>().AddTorque(randTorque);
            GoldStartCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ShowEffect(EffectType effectType,Transform follow)
    {
        switch (effectType)
        {
            case EffectType.shield:
                shield_temp.SetActive(true);
                shield_temp.GetComponent<FollowObject>().target = follow;
                break;  
        }
    }
    public void ShowEffect(EffectType effectType, Vector3 Pos,int count =0)
    {
        
        switch(effectType)
        {
            case EffectType.shield:

                break;
            case EffectType.Dust_1:
                Vector2 nextPos = GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position;
                GameObject temp = Instantiate(Dust_1);
                //temp.SetActive(false);
                nextPos.x = nextPos.x - 2.5f;
                //nextPos.y = nextPos.y - 0.0f;
                
                temp.transform.position = nextPos;
                temp.SetActive(true);
                break;
            case EffectType.Dust_2:
                Vector2 nextPos2 = GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position;
                GameObject temp2 = Instantiate(Dust_2);
                temp2.SetActive(false);
                nextPos2.x = nextPos2.x + -2.5f;
                nextPos2.y = nextPos2.y+0.6f;                
                temp2.transform.position = nextPos2;
                temp2.SetActive(true);
                break;
            case EffectType.BoundsCoin:
                StartCoroutine(BoundsCointRoutine(Pos, count));
                break;
            case EffectType.goldLife:
                StartCoroutine(BoundsGoldLIfeRoutine(Pos, count));
                break;
            case EffectType.goldStar:
                StartCoroutine(BoundsGoldStarRoutine(Pos, count));
                break;

            case EffectType.BoundsLife:
                StartCoroutine(BoundsLifeRoutine(Pos, count));
                break;
            case EffectType.BoundsBullet:
                StartCoroutine(BoundsBulletRoutine(Pos, count));
                break;
            case EffectType.o_Effect:
                if (oEffectCount >= OEffectList.Count)
                    oEffectCount = 0;
                OEffectList[oEffectCount].transform.position = Pos;
                OEffectList[oEffectCount].SetActive(true);
                oEffectCount++;
                break;
            case EffectType.Dust:
                if (DustCount >= DustList.Count)
                    DustCount = 0;
                
                DustList[DustCount].transform.position = Pos;
                DustList[DustCount].SetActive(true);
                DustCount++;
                break;
            case EffectType.Death:
                if (DeathCount >= DeathEffectList.Count)
                    DeathCount = 0;

                DeathEffectList[DeathCount].transform.position = Pos;
                DeathEffectList[DeathCount].SetActive(true);
                DeathCount++;
                break;
            case EffectType.Heal:

                if (healCount >= HealEffectList.Count)
                    healCount = 0;

                HealEffectList[healCount].transform.position = Pos;
                HealEffectList[healCount].SetActive(true);
                healCount++;
                break;
        }
    }
}
