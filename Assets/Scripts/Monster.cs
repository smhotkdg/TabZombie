using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Monster : MonoBehaviour
{
    public enum zombieType
    {
        Normal,
        range,
        walk,
        Rope
    }
    [SerializeField]
    float max_x = 100;
    [SerializeField]
    float max_y = 200;
    [SerializeField]
    float torque = 100;
    public GameObject Bullet;
    public GameObject BulltetPos;
    public zombieType m_zombieType = zombieType.Normal;
    [SerializeField]
    private float Hp = 1;
    float defaultHp;
    Animator m_animator;
    BoxCollider2D m_boxcolider2d;
    Rigidbody2D m_rigidbody;
    public List<SpriteRenderer> LifeList;
    public Sprite Life;
    public Sprite LifeEmpty;
    Vector2 initPos;
    GameObject RopeObject;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_boxcolider2d = GetComponent<BoxCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        defaultHp = Hp;
        if(m_zombieType == zombieType.Rope)
        {
            RopeObject = transform.Find("Rope").gameObject;
        }        
    }
    
    private void OnEnable()
    {
        for(int i=0; i< LifeList.Count; i++)
        {
            LifeList[i].gameObject.SetActive(true);
            LifeList[i].sprite = Life;
        }
             
        Hp = defaultHp;
        m_animator.Play("IDLE");
        m_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        m_boxcolider2d.enabled = true;
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        initPos = transform.position;
        if (m_zombieType == zombieType.Rope)
        {
            RopeObject.SetActive(true);
            RopeObject.transform.SetParent(transform);
            RopeObject.transform.localPosition = new Vector3(0, 1.75f, 0);
            RopeObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            StartRope(0);
        }
    }
    public float ropeY=8;
    public void StartRope(float yPos)
    {
        if (m_zombieType == zombieType.Rope)
        {
            ropeY = yPos;
            float MoveRand = Random.Range(0.5f, 1.5f);
            float delayTime = Random.Range(0.5f, 1f);
            float YRand = Random.Range(1f, 3f);
            transform.DOMoveY( YRand, MoveRand).SetEase(Ease.Linear).OnComplete(MoveCompleteRope).SetDelay(delayTime).SetLoops(-1,LoopType.Yoyo);
        }
    }
    
    void MoveCompleteRope()
    {
        if (m_zombieType == zombieType.Rope)
        {
            float MoveRand = Random.Range(0.3f, 1f);
            float MoveRand2 = Random.Range(0.5f, 1.5f);
            //transform.DOMoveY(initPos.y, MoveRand2).SetEase(Ease.Linear).OnComplete(CompleteInit).SetDelay(MoveRand);
        }
    }
    void CompleteInit()
    {
        StartRope(ropeY);
    }



    public void Hit(bool instant_death = false)
    {

        if(instant_death)
        {
            m_animator.Play("None");
            StartDeath();
            m_boxcolider2d.enabled = false;
            SoundManager.Instance.PlayFx(SoundManager.FxType.Impact_Monster);
            Debug.Log("Áï»ç!");            

            return;
        }
        int power = 1;
        if(GameManager.Instance.DoubleChance)
        {
            int rand = Random.Range(0, 100);
            if(rand < 10)
            {
                power = GameManager.Instance.power * 2;
                UIManager.Instance.SetEffectText(UIManager.TextEventType.effect,UIManager.TextType.critical);
                Debug.Log("´õºí ¾îÅÃ");
            }
            else
            {
                power = GameManager.Instance.power;
            }            
        }
        else
        {
            power = GameManager.Instance.power;
        }
        if(GameManager.Instance.isFullBulletPower)
        {
            if(GameManager.Instance.BulletCount == GameManager.Instance.defaultBulletCount)
            {
                power += 1;
                Debug.Log("ÃÑ¾Ë °¡µæÂ÷¼­ ½ØÁü!");
                UIManager.Instance.SetEffectText(UIManager.TextEventType.effect,UIManager.TextType.critical);
            }
        }
        Hp -= power;
        GameManager.Instance.CheckBulletGet(transform.position);
        GameManager.Instance.CheckHpsteel(transform.position);
        if(Hp <=0)
        {
            m_animator.Play("None");
            StartDeath();
            m_boxcolider2d.enabled = false;
        }
        else
        {
            for(int i =0;i<LifeList.Count-Hp;i++)
            {
                LifeList[i].sprite = LifeEmpty;
            }            
            m_animator.Play("DAMAGED");
        }
        SoundManager.Instance.PlayFx(SoundManager.FxType.Impact_Monster);
    }
    void StartDeath()
    {
        if(m_zombieType == zombieType.Rope)
        {
            RopeObject.transform.SetParent(transform.parent);
        }
        if (GameManager.Instance.Combo >= 10)
        {
            //int count = Mathf.RoundToInt((float)GameManager.Instance.Combo / 10f);
            Vector2 pos = transform.position;
            pos.y = pos.y + 2f;
            EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsCoin, pos, 1);
        }
        UIManager.Instance.SetEffectText(UIManager.TextEventType.combo);
        for (int i = 0; i < LifeList.Count; i++)
        {
            LifeList[i].gameObject.SetActive(false);
        }
        EffectController.Instance.ShowEffect(EffectController.EffectType.Death, transform.position);
        m_rigidbody.constraints = RigidbodyConstraints2D.None;
        m_boxcolider2d.enabled = false;
        float randx = Random.Range(max_x/2, max_x);
        float randy = Random.Range(max_y/2, max_y);
        float randTorque = Random.Range(torque/2, torque);
        m_rigidbody.AddForce(new Vector2(randx, randy));
        m_rigidbody.AddTorque(randTorque);
        
        //transform.localScale = new Vector3(1, -1, 1);
        StartCoroutine(DisableRoutine());
    }
    private void OnDisable()
    {
        if (m_zombieType == zombieType.Rope)
        {
            RopeObject.SetActive(false);
        }
    }
    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (m_zombieType == zombieType.Rope)
        {
            RopeObject.SetActive(false);
        }
        this.gameObject.SetActive(false);

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag =="Attack")
    //    {
    //        Hit();
    //    }
    //}
 
    public void CheckAttack()
    {
        float rand = Random.Range(0, 2f);
        if(rand < 1.2f)
        {
            m_animator.Play("ATTACK");
        }
    }
    public void RangeAttack()
    {
        Vector2 direction = transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 1);
        GameObject temp = Instantiate(Bullet);
        GameObject p= GameObject.Find("Bullet_Monster");
        temp.name = "bullet_monster";
        temp.transform.SetParent(p.transform);
        temp.transform.position = BulltetPos.transform.position;
        temp.SetActive(true);    
    }
    //private void OnBecameInvisible()
    //{
    //    this.gameObject.SetActive(false);
    //}
}
