using UnityEngine;
using System.Collections;

public class Sensor_Bandit : MonoBehaviour {
    [SerializeField]
    private int m_ColCount = 0;

    private float m_DisableTimer;
    public bool bGround;
    Bandit bandit;
    Vector2 pos;
    private void Awake()
    {
        pos = transform.localPosition;
        bandit = transform.parent.GetComponent<Bandit>();
    }
    private void OnEnable()
    {
        m_ColCount = 0;
        bGround = true;
    }

    public bool State()
    {
        //if (m_DisableTimer > 0)
        //    return false;
        return m_ColCount > 0;
        
        //return bGround;
        //return bGround;
    }
    private void FixedUpdate()
    {
        //transform.localPosition = pos;
    }
    public void StartGroundRoutine(float time)
    {
        bGround = false;
        //StartCoroutine(groundRoutine(time));
    }
    IEnumerator groundRoutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        bGround = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="ground")
        {
//            if(other.gameObject.activeSelf)
            m_ColCount++;   
            if(bandit.isGood)
            {
                if(other.name == "DisableMap")
                {
                    //other.gameObject.SetActive(false);
                    other.GetComponent<MapController>().m_maptype = MapController.MapType.DisableNormal;
                    other.GetComponent<MapController>().m_spriteRender.sprite = other.GetComponent<MapController>().Noraml_ChangeTile;
                }
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ground")
        {
            //if (other.gameObject.activeSelf)
            m_ColCount--;
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
