using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType {
        Hero,
        Monster
    }
    public BulletType m_bulletType = BulletType.Hero;
    public float speed = 5;
    public float LifeTime = 3;
    private float DisableTime;
    public Animator animator;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_bulletType == BulletType.Hero)
        {
            if (collision.tag == "monster")
            {
                if (collision.gameObject.GetComponent<Monster>() != null)
                {
                    if(GameManager.Instance.instant_death)
                    {
                        collision.gameObject.GetComponent<Monster>().Hit(true);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Monster>().Hit();
                    }

                    
                }
                //this.gameObject.SetActive(false);
            }
        }     
        else if(m_bulletType == BulletType.Monster)
        {
            if(collision.tag == "Player")
            {
                animator.Play("2_poison");
                moveStop = true;
            }
            else if(collision.gameObject.name == "Bullet")
            {
                if(collision.gameObject.GetComponent<Bullet>().m_bulletType== BulletType.Hero)
                {
                    animator.Play("2_poison");
                    moveStop = true;
                }
            }
        }
      
        if (collision.tag == "ground")
        {
            if (m_bulletType == BulletType.Hero)
            {
                if (!GameManager.Instance.WaveShoot)
                {
                    this.gameObject.SetActive(false);
                } 
                    
            }
            else
            {
                //Destroy(this.gameObject);
                animator.Play("2_poison");
                moveStop = true;
            }

        }
    }
    public void hitMonster()
    {
        animator.Play("2_poison");
        moveStop = true;
    }
    private void OnEnable()
    {
        moveStop = false;
        if (m_bulletType == BulletType.Monster)
        {
            animator.Play("1_poison");

        }
        DisableTime = LifeTime;
        pivot = transform.position;
        height /= 2;
        timeSinceStart = (3 * timePeriod) / 4;
    }
    public float timePeriod = 2;
    public float height = 30f;
    private float timeSinceStart;
    private Vector3 pivot;
    bool moveStop = false;
    private void Update()
    {
        DisableTime -= Time.deltaTime;
        if(DisableTime<=0)
        {
            if(m_bulletType == BulletType.Hero)
            {
                gameObject.SetActive(false);
            }
            else
            {
                animator.Play("2_poison");
                moveStop = true;
            }
            
        }
        if(m_bulletType == BulletType.Hero)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if(GameManager.Instance.WaveShoot)
            {
                Vector3 nextPos = transform.position;
                nextPos.y = pivot.y + height + height * Mathf.Sin(((Mathf.PI * 2) / timePeriod) * timeSinceStart);
                timeSinceStart += Time.deltaTime;
                transform.position = nextPos;
            }
        }
        else
        {
            if(moveStop)
            {
                return;
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
    }
}
