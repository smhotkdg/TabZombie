using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    [SerializeField]
    float torque = 5;
    [SerializeField]
    float max_x = 5f; 
    [SerializeField]
    float max_y = 5f;

    public GameObject DestoryP;
    // Start is called before the first frame update
    List<GameObject> DestoryList = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < DestoryP.transform.childCount; i++)
        {
            DestoryList.Add(DestoryP.transform.GetChild(i).gameObject);
        }
    }  
    private void OnEnable()
    {

        for(int i =0; i< DestoryList.Count; i++)
        {
            DestoryList[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            DestoryList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            DestoryList[i].transform.localPosition = new Vector3(0, 0, 0);
            DestoryList[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            DestoryList[i].SetActive(true);
        }
        DestoryP.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Attack")
        {
            GameManager.Instance.CheckBulletGet(collision.transform.position);
            GameManager.Instance.CheckHpsteel(collision.transform.position);
            StartDestory();
            UIManager.Instance.SetEffectText(UIManager.TextEventType.combo);
            
        }
    }
    public void StartDestory()
    {
        SoundManager.Instance.PlayFx(SoundManager.FxType.Wood);
        if (GameManager.Instance.Combo >= 10)
        {
            //int count =  Mathf.RoundToInt((float)GameManager.Instance.Combo / 10f);
            Vector2 pos = transform.position;
            pos.y = pos.y + 2f;
            EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsCoin, transform.position, 1);
        }
        DestoryP.SetActive(true);
        for (int i = 0; i < DestoryList.Count; i++)
        {
            DestoryList[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            float randx = Random.Range(-max_x, max_x);
            float randy = Random.Range(0, max_y);
            float randTorque = Random.Range(0, torque);
            DestoryList[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy));
            DestoryList[i].GetComponent<Rigidbody2D>().AddTorque(randTorque);
        }
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
