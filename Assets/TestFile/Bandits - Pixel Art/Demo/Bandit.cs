using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class Bandit : MonoBehaviour {
    [SerializeField]
    float groundTriggerTime = 0.05f;
    public List<GameObject> WeaponList;
    public List<GameObject> GunList;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] float      m_speed = 4.0f;
    public float      m_jumpForce = 7.5f;
    [SerializeField] float m_rightForce = 2f;
    public float flightSpeed = 2;
    public NextSensor m_nextSensor;
    float yMove;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    public bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    int jumpCount;
    public bool isGood = false;
    private SpriteRenderer m_spriteRenderer;
    IEnumerator MoveRoutine;
    IEnumerator FastMoveRoutine;
    IEnumerator FastMoveRoutine_event;
    int damageCount = 0;
    // Use this for initialization
    bool isBack = false;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_nextSensor = transform.Find("NextSensor").GetComponent<NextSensor>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start () {
        


    }
    public void SetWeapon()
    {
        for(int i=0; i< WeaponList.Count;i++)
        {
            WeaponList[i].SetActive(false);
        }
        for (int i = 0; i < GunList.Count; i++)
        {
            GunList[i].SetActive(false);
        }
        WeaponList[GameManager.Instance.WeaponIndex].SetActive(true);
        GunList[GameManager.Instance.GunIndex].SetActive(true);
        posInit = transform.position;
    }
    Vector2 posInit;
    private void OnEnable()
    {
        currentAnimName = "idle";
        m_animator.Play("IDLE");
        bDeath = false;
        jumpCount = 0;


    }
    public void SetPos()
    {

    }
    public IEnumerator IEFlight(Vector2 pos)
    {
        float duration = flightSpeed;
        float time = 0.0f;
        Vector3 start = transform.position;

        Vector3 end = pos;

     
        while (time < duration)
        {
            time += Time.deltaTime;
            
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, m_jumpForce, heightT);
         
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
        prevVec = pos;
        SetNext();
    }
    public IEnumerator IEFlight_Fast(Vector2 pos,Vector2 effectPos)
    {
        float duration = 0.1f;
        float time = 0.0f;
        Vector3 start = transform.position;

        Vector3 end = pos;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, m_jumpForce, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
        prevVec = pos; 
        int randDust = Random.Range(0, 2);    
        if(randDust <1)
        {
            EffectController.Instance.ShowEffect(EffectController.EffectType.Dust_1, start);
        }
        else
        {
            EffectController.Instance.ShowEffect(EffectController.EffectType.Dust_2, start);
        }        
    }
    public IEnumerator IEFlight_Fast_Event(Vector2 pos, Vector2 effectPos)
    {
        float duration = 0.1f;
        float time = 0.0f;
        Vector3 start = transform.position;

        Vector3 end = pos;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, m_jumpForce, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
        prevVec = pos;
        int randDust = Random.Range(0, 2);
        if (randDust < 1)
        {
            EffectController.Instance.ShowEffect(EffectController.EffectType.Dust_1, start);
        }
        else
        {
            EffectController.Instance.ShowEffect(EffectController.EffectType.Dust_2, start);
        }

    }
    private IEnumerator IEFlight_prev()
    {
        isBack = true;        
        float duration = flightSpeed;
        float time = 0.0f;
        Vector3 start = transform.position;

        Vector3 end_pos = prevVec;




        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, m_jumpForce, heightT);

            transform.position = Vector2.Lerp(start, end_pos, linearT) + new Vector2(0.0f, height);

            yield return null;
        }
        m_groundSensor.bGround = true;
        isBack = false;
        start = end_pos;     

    }
    [SerializeField]
    Vector2 prevVec;
    bool addMove;
 
    bool m_jump = false;
    public void Move()
    {
        if (bDeath)
        {
            return;
        }
        if (m_grounded && m_jump==false  && isBack ==false)
        {
            movePos = m_nextSensor.GetPos();
            start = transform.position;
            m_jump = true;
            
            m_animator.Play("JUMP");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            WeaponList[GameManager.Instance.WeaponIndex].GetComponent<Animator>().Play("IDLE");

            //m_body2d.velocity = Vector2.up * m_jumpForce;
            //m_body2d.velocity = Vector2.right * m_rightForce;
            
            MoveRoutine = IEFlight(m_nextSensor.GetPos());            
            
            //StartCoroutine(MoveRoutine);
            m_groundSensor.Disable(groundTriggerTime);
            m_groundSensor.StartGroundRoutine(groundTriggerTime);
            SoundManager.Instance.PlayFx(SoundManager.FxType.Jump);
            bAttack = false;
            m_nextSensor.enabled = false;
            
        }
     
       
    }
    
    bool bAttack = false;
    public bool AvoidCheck()
    {
        if(GameManager.Instance.avoidChance)
        {
            int rand = Random.Range(0, 100);
            if(rand <5)
            {
                UIManager.Instance.SetEffectText(UIManager.TextEventType.effect,UIManager.TextType.dodge);
                Debug.Log("회피!");
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bDeath)
            return;
        if (isGood)
            return;
        if (collision.gameObject.tag == "monster")
        {
            
            if(AvoidCheck())
            {
                return;
            }
            if (isHit)
                return;
            
            StopCoroutine(MoveRoutine);
            //prevVec = m_nextSensor.GetPos();
            //m_animator.SetTrigger("Hurt");
            m_animator.Play("HIT");
            m_grounded = false;
            //if (!m_groundSensor.State())
            {                
                StartCoroutine(IEFlight_prev());
            }            
            //damageCount++;
            bAttack = true;
            SoundManager.Instance.PlayFx(SoundManager.FxType.Hurt);
            GameManager.Instance.ShakeCamera(0.1f);
            isHit = true;
            HitEffect();   
            
            if (collision.gameObject.name == "AttackTrigger")
            {
                switch (collision.gameObject.transform.parent.name)
                {
                    case "NormalZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.NoramlZombie;
                        break;
                    case "Hp2Zombie":
                        GameManager.Instance.deathType = GameManager.DeathType.Hp2Zombie;
                        break;
                    case "PowerZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.PowerZombie;
                        break;
                    case "RangeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RangeZombie;
                        break;
                    case "RopeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RopeZombie;
                        break;
                }
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "NormalZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.NoramlZombie;
                        break;
                    case "Hp2Zombie":
                        GameManager.Instance.deathType = GameManager.DeathType.Hp2Zombie;
                        break;
                    case "PowerZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.PowerZombie;
                        break;
                    case "RangeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RangeZombie;
                        break;
                    case "RopeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RopeZombie;
                        break;
                }
            }

            if (collision.gameObject.name == "hand")
            {
                switch (collision.gameObject.transform.parent.name)
                {
                    case "ZombieHand":
                        GameManager.Instance.deathType = GameManager.DeathType.ZombieHand;
                        break;
                    case "Sign":
                        GameManager.Instance.deathType = GameManager.DeathType.Sign;
                        break;
                    case "Trap_1":
                        GameManager.Instance.deathType = GameManager.DeathType.Trap;
                        break;
                    case "TrapRod":
                        GameManager.Instance.deathType = GameManager.DeathType.Rod;
                        break;
                    case "Wood":
                        GameManager.Instance.deathType = GameManager.DeathType.Wood;
                        break;
                }
            }
            SetDamage();
        }
        if(collision.gameObject.tag =="Block")
        {
            if (AvoidCheck())
            {
                return;
            }
            if (isHit)
                return;
            
            StopCoroutine(MoveRoutine);
            //prevVec = m_nextSensor.GetPos();
            //m_animator.SetTrigger("Hurt");
            m_animator.Play("HIT");
            m_grounded = false;
            //if (!m_groundSensor.State())
            {
                StartCoroutine(IEFlight_prev());
            }
            //damageCount++;
            bAttack = true;
            SoundManager.Instance.PlayFx(SoundManager.FxType.Hurt);
            GameManager.Instance.ShakeCamera(0.1f);
            isHit = true;
            HitEffect();
            
            
            switch (collision.gameObject.transform.parent.name)
            {
                case "ZombieHand":
                    GameManager.Instance.deathType = GameManager.DeathType.ZombieHand;
                    break;
                case "Sign":
                    GameManager.Instance.deathType = GameManager.DeathType.Sign;
                    break;
                case "Trap_1":
                    GameManager.Instance.deathType = GameManager.DeathType.Trap;
                    break;
                case "TrapRod":
                    GameManager.Instance.deathType = GameManager.DeathType.Rod;
                    break;
                case "Wood":
                    GameManager.Instance.deathType = GameManager.DeathType.Wood;
                    break;
            }
            SetDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bDeath)
        {
            return;
        }
        if (isGood)
            return;
        if (collision.tag == "monster")
        {
            if (AvoidCheck())
            {
                return;
            }
            if (isHit)
                return;
            
            StopCoroutine(MoveRoutine);
            //prevVec = m_nextSensor.GetPos();
            //m_animator.SetTrigger("Hurt");
            m_animator.Play("HIT");
            m_grounded = false;
            //if (!m_groundSensor.State())
            {
                StartCoroutine(IEFlight_prev());
            }
            //damageCount++;
            bAttack = true;
            SoundManager.Instance.PlayFx(SoundManager.FxType.Hurt);
            GameManager.Instance.ShakeCamera(0.1f);
            isHit = true;
            HitEffect();
            

            if (collision.gameObject.name == "AttackTrigger")
            {
                switch (collision.gameObject.transform.parent.name)
                {
                    case "NormalZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.NoramlZombie;
                        break;
                    case "Hp2Zombie":
                        GameManager.Instance.deathType = GameManager.DeathType.Hp2Zombie;
                        break;
                    case "PowerZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.PowerZombie;
                        break;
                    case "RangeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RangeZombie;
                        break;
                    case "RopeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RopeZombie;
                        break;
                }
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "NormalZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.NoramlZombie;
                        break;
                    case "Hp2Zombie":
                        GameManager.Instance.deathType = GameManager.DeathType.Hp2Zombie;
                        break;
                    case "PowerZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.PowerZombie;
                        break;
                    case "RangeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RangeZombie;
                        break;
                    case "RopeZombie":
                        GameManager.Instance.deathType = GameManager.DeathType.RopeZombie;
                        break;
                }
            }

            if (collision.gameObject.name == "hand")
            {
                switch (collision.gameObject.transform.parent.name)
                {
                    case "ZombieHand":
                        GameManager.Instance.deathType = GameManager.DeathType.ZombieHand;
                        break;
                    case "Sign":
                        GameManager.Instance.deathType = GameManager.DeathType.Sign;
                        break;
                    case "Trap_1":
                        GameManager.Instance.deathType = GameManager.DeathType.Trap;
                        break;
                    case "TrapRod":
                        GameManager.Instance.deathType = GameManager.DeathType.Rod;
                        break;
                    case "Wood":
                        GameManager.Instance.deathType = GameManager.DeathType.Wood;
                        break;
                }
            }
            if (collision.gameObject.name == "bullet_monster")
            {
                GameManager.Instance.deathType = GameManager.DeathType.RangeZombie;
            }
            SetDamage();
        }
        if(collision.tag=="death")
        {
            GameManager.Instance.deathType = GameManager.DeathType.Drop;
            GameManager.Instance.DeathPlayer();
            StopRecoding();


        }
    }
    public bool bDeath = false;
    void SetDamage()
    {
        GameManager.Instance.Hp -= 1;
        if(GameManager.Instance.Hp <=0)
        {
            GameManager.Instance.Hp = 0;
        }
        UIManager.Instance.SetDamage();
        if(GameManager.Instance.Hp <=0)
        {
            if(bDeath ==false)
            {                
                m_grounded = true;
                
                bDeath = true;
                m_animator.Play("DEAD");
                currentAnimName = "death";
            }
            
        }
    }
    public void DeathEndAnim()
    {
        GameManager.Instance.DeathPlayer();
    }
    public void StopRecoding()
    {
        UIManager.Instance.Stop();
    }
    string currentAnimName = "";
    public void Attack()
    {
        if (bDeath)
        {
            return;
        }
        if (isGun)
        {
            return;
        }
        //m_animator.SetTrigger("Attack");        
        //if()
        
        if(currentAnimName != "attack")
        {
            SoundManager.Instance.PlayFx(SoundManager.FxType.Impact);
        }        
        currentAnimName = "attack";
        m_animator.Play("MELEE_WEAPON");
        WeaponList[GameManager.Instance.WeaponIndex].GetComponent<Animator>().Play("ATTACK");
        
    }
    public void SetIdle()
    {
        currentAnimName = "idle";
    }
    public void Shoot()
    {
        if (bDeath)
        {
            return;
        }
        if (isGun == false)
        {
            //m_animator.SetTrigger("Attack");
            GunList[GameManager.Instance.GunIndex].transform.parent.gameObject.SetActive(false);
            m_animator.Play("GUN");
            GunList[GameManager.Instance.GunIndex].GetComponent<Animator>().Play("ATTACK");
            SoundManager.Instance.PlayFx(SoundManager.FxType.GunInit);
            isGun = true;
            StartCoroutine(GunDelayRoutine());
        }
    }
    bool isHit = false;
    public void HitEffect()
    {
        m_spriteRenderer.DOColor(new Color(0.6f, 0, 0, 1), 0.05f).SetEase(Ease.Linear).SetLoops(10, LoopType.Yoyo).OnComplete(CompleteHit);
    }
    void CompleteHit()
    {
        m_spriteRenderer.color = new Color(1, 1, 1, 1);
        isHit = false;
    }

    public void GetDamage()
    {

    }
    // Update is called once per frame
    bool bInit = false;
    public void SetNext()
    {
        if (isGood)
            return;
        if (bAttack == false)
        {
            MapMaker.Instance.GenerateMap();
            int randDust = Random.Range(2, 6);
            for (int i = 0; i < randDust; i++)
            {
                float initX = transform.position.x;
                float initY = transform.position.y - 0.7f;
                float randX = Random.Range(-0.4f, 0.4f);
                float randY = Random.Range(-0.1f, 0.1f);
                EffectController.Instance.ShowEffect(EffectController.EffectType.Dust, new Vector3(initX + randX, initY + randY));
            }
            //float initX = transform.position.x;
            //float initY = transform.position.y;
            //float randX = Random.Range(-0.644f, -0.1f);
            //float randY = Random.Range(-0.6f, -0.4f);
            //EffectController.Instance.ShowEffect(EffectController.EffectType.Dust, new Vector3(initX + randX, initY + randY));
            //if (bInit)
            {
                if (bDeath ==false)
                {
                    GameManager.Instance.NowScore++;
                    UIManager.Instance.SetScore();
                    MapMaker.Instance.SetWeight();
                }                
            }
            m_groundSensor.bGround = true;
       
            bInit = true;
            yMove = transform.position.y;
        }
    }
    Vector2 movePos = new Vector2();
    Vector3 start;
    float time = 0.0f;    
   
    public void MoveEvent(Vector2 pos,Vector2 effectPos)
    {
        FastMoveRoutine = IEFlight_Fast(pos, effectPos);
        
        StartCoroutine(FastMoveRoutine);
    }
    public void MoveFastEvent(Vector2 pos, Vector2 effectPos)
    {
        FastMoveRoutine_event = IEFlight_Fast_Event(pos,effectPos);

        StartCoroutine(FastMoveRoutine_event);
    }
    public void setReset()
    {
        m_jump = false;
    }
    void Update() {

        if (bDeath)
        {
            return;
        }

        if (m_jump )
        {
            float duration = flightSpeed;


            prevVec = start;
            time += Time.deltaTime;
            Vector3 end = movePos;
            //Vector3 modifyEnd = movePos;
            //modifyEnd.y = modifyEnd.y + 0.2f;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);
           
            float height = Mathf.Lerp(0.0f, m_jumpForce, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            if (time >= duration)
            {
                if(m_jump ==true)
                {
                    m_jump = false;
                    time = 0;
                    if(!isGood)
                    {
                        SetNext();
                        prevVec = end;
                    }                       
                    
                }                
            }
        }

        if (!m_grounded && m_groundSensor.State())
        {
            float x = transform.position.x;
            float initx = posInit.x;
            m_nextSensor.enabled = true;
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            if (x - initx >= 0.9f)
            {                
                
                posInit = transform.position;
            }          
            
        }
        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            currentAnimName = "idle";
        }
        if (Input.GetKeyDown("space") && m_grounded && m_jump ==false && isBack ==false)
        //if (Input.GetKeyDown("space") )
        {
            if (isGood)
                return;
            movePos = m_nextSensor.GetPos();
            start = transform.position;
            m_jump = true;

            m_animator.Play("JUMP");
            currentAnimName = "jump";
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            WeaponList[GameManager.Instance.WeaponIndex].GetComponent<Animator>().Play("IDLE");

            //m_body2d.velocity = Vector2.up * m_jumpForce;
            //m_body2d.velocity = Vector2.right * m_rightForce;

            MoveRoutine = IEFlight(m_nextSensor.GetPos());

            //StartCoroutine(MoveRoutine);
            m_groundSensor.Disable(groundTriggerTime);
            m_groundSensor.StartGroundRoutine(groundTriggerTime);
            SoundManager.Instance.PlayFx(SoundManager.FxType.Jump);
            bAttack = false;
            m_nextSensor.enabled = false;
        }
    
        if (Input.GetKeyDown("a"))
        {
            //m_animator.SetTrigger("Attack");
            if (isGun)
            {
                return;
            }
            if (isGood)
                return;
            jumpCount = 0;
            m_animator.Play("MELEE_WEAPON");
            WeaponList[GameManager.Instance.WeaponIndex].GetComponent<Animator>().Play("ATTACK");
            SoundManager.Instance.PlayFx(SoundManager.FxType.Impact);
        }
        if (Input.GetKeyDown("s"))
        {
            if (isGood)
                return;
            if (isGun == false)
            {
                jumpCount = 0;
                //m_animator.SetTrigger("Attack");
                GunList[GameManager.Instance.GunIndex].transform.parent.gameObject.SetActive(false);
                m_animator.Play("GUN");                                
                GunList[GameManager.Instance.GunIndex].GetComponent<Animator>().Play("ATTACK");
                SoundManager.Instance.PlayFx(SoundManager.FxType.GunInit);
                isGun = true;
                StartCoroutine(GunDelayRoutine());
            }
            
        }
    }
    IEnumerator GunDelayRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isGun = false;
    }
    bool isGun = false;
 
}

