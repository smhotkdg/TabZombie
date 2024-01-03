using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MapController : MonoBehaviour
{
    public enum MapType
    {
        Normal,
        Disable,
        ZombieHand,
        Rod,
        Sign,
        Trap_1,
        Trap_2,
        Wood,
        DisableNormal
    }
    [SerializeField]
    float disableTime = 0.7f;
    public Sprite NoramlSprite;
    public Sprite DestorySprite;
    public Sprite Noraml_ChangeTile;
    BoxCollider2D m_boxColider;
    public SpriteRenderer m_spriteRender;
    
    public MapType m_maptype = MapType.Normal;
    IEnumerator disableCoroutine;
    IEnumerator CheckAttackRoutine;
    private void Awake()
    {
        m_boxColider = GetComponent<BoxCollider2D>();
        m_spriteRender = GetComponent<SpriteRenderer>();
        
        disableCoroutine = DisableRoutine();
        CheckAttackRoutine = AttackRoutine();
    }
    private void OnDisable()
    {
        isIn = false;
        StopCoroutine(disableCoroutine);
        StopCoroutine(CheckAttackRoutine);
    }
    private void OnEnable()
    {
        isDestory = false;
        m_spriteRender.sprite = NoramlSprite;
        if(m_maptype == MapType.DisableNormal)
        {
            m_maptype = MapType.Disable;
            m_spriteRender.sprite = NoramlSprite;
        }
        if (m_maptype == MapType.ZombieHand)
        {
            transform.Find("hand").gameObject.SetActive(true);
        }
        else if (m_maptype == MapType.Trap_1)
        {
            transform.Find("hand").gameObject.SetActive(true);
        }
        if (m_maptype == MapType.Rod)
        {
            transform.Find("Rod").gameObject.SetActive(true);
            
        }
        if(m_maptype == MapType.Sign)
        {
            transform.Find("hand").gameObject.SetActive(true);
        }
        if(m_maptype == MapType.Sign)
        {
            transform.Find("hand").gameObject.GetComponent<Animator>().Play("singInit");
            StartCoroutine(singRoutine());
        }
        DOTween.Kill(gameObject);
        disableCoroutine = DisableRoutine();
        CheckAttackRoutine = AttackRoutine();
        StartCoroutine(CheckAttackRoutine);
        if(m_maptype == MapType.Disable)
        {
            m_spriteRender.color = new Color(1, 1, 1, 1);
        }        
    }
    bool isIn = false;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GroundSensor")
        {
            if (m_maptype == MapType.Disable)
            {
                if (isIn == false)
                {

                    //m_spriteRender.DOColor(new Color(1, 1, 1, 0.5f), 0.5f).SetEase(Ease.Linear).SetLoops(5, LoopType.Yoyo);
                    if (GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].GetComponent<Bandit>().isGood)
                        return;
                    m_spriteRender.DOColor(new Color(1, 1, 1, 0.6f), 0.2f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo);
                    transform.DOShakePosition(0.2f, new Vector2(0.2f, 0), 30).SetLoops(4, LoopType.Yoyo);
                    StartCoroutine(disableCoroutine);
                    isIn = true;                
                }
            }
            else
            {
             
            }
        }        

    }
    bool isDestory = false;
    public bool Hit()
    {
        if (isDestory)
            return false;
        isDestory = true;
        m_spriteRender.sprite = DestorySprite;
        if (m_maptype == MapType.ZombieHand)
        {
            transform.Find("hand").gameObject.SetActive(false);
        }
        else if (m_maptype == MapType.Trap_1)
        {
            transform.Find("hand").gameObject.SetActive(false);
        }
        return true;
    }
    public void CheckAttack()
    {
        if(m_maptype == MapType.ZombieHand)
        {
            transform.Find("hand").gameObject.GetComponent<Animator>().Play("hand_event");                       
        }
        else if(m_maptype == MapType.Trap_1)
        {
            transform.Find("hand").gameObject.GetComponent<Animator>().Play("trap_1_TRAP");
        }
    }
    IEnumerator singRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0, 4f));
        transform.Find("hand").gameObject.GetComponent<Animator>().Play("ON");
    }
    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(disableTime);        
        
        if(m_maptype == MapType.DisableNormal)
        {
            StopCoroutine(disableCoroutine);
        }
        else
        {
            if (Mathf.RoundToInt(transform.position.x) <= Mathf.RoundToInt(GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position.x))
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                disableCoroutine = DisableRoutine();
                isIn = false;
            }
        }        
    }
    IEnumerator AttackRoutine()
    {
        if(m_maptype == MapType.ZombieHand)
        {
            float random = Random.Range(1f, 4f);
            yield return new WaitForSeconds(random);
            CheckAttack();
            CheckAttackRoutine = AttackRoutine();
            StartCoroutine(CheckAttackRoutine);
        }
        else if(m_maptype == MapType.Trap_1)
        {
            float random = Random.Range(1f, 4f);
            yield return new WaitForSeconds(random);
            CheckAttack();
            CheckAttackRoutine = AttackRoutine();
            StartCoroutine(CheckAttackRoutine);
        }
      
    }
    private void OnBecameInvisible()
    {        
       
    }
}
