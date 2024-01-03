using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSensor : MonoBehaviour
{
    public bool itDeath =false;
    float hitTimer = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="monster")
        {           
            if(collision.gameObject.GetComponent<Monster>() !=null)
            {
                if(itDeath)
                {
                    collision.gameObject.GetComponent<Monster>().Hit(true);
                    hitTimer = 0.1f;
                }
                else
                {
                    collision.gameObject.GetComponent<Monster>().Hit();
                }
                
            }            
            else if(collision.name =="bullet_monster")
            {
                collision.gameObject.GetComponent<Bullet>().hitMonster();
                UIManager.Instance.SetEffectText(UIManager.TextEventType.combo);
            }
        }
        if(collision.tag == "ground")
        {
            if (GameManager.Instance.brokenObject)
            {
                if(collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.ZombieHand || 
                    collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.Trap_1 ||
                    collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.Rod ||
                    collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.Wood)
                {
                    if(collision.gameObject.GetComponent<MapController>().Hit())
                    {
                        if(collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.Trap_1 ||
                           collision.gameObject.GetComponent<MapController>().m_maptype == MapController.MapType.ZombieHand)
                        {
                            UIManager.Instance.SetEffectText(UIManager.TextEventType.combo);
                            SoundManager.Instance.PlayFx(SoundManager.FxType.ObejctImpact);
                        }                        
                    }                    
                }

            }
        }
        
    }
    private void FixedUpdate()
    {
        if(hitTimer>0)
        {
            hitTimer -= Time.deltaTime;
        }
        else
        {
            hitTimer = 0;
        }
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
