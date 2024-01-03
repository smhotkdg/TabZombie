using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectType
    {
        Coin,
        Bullet,
        Life,
        BoundsCoin,
        goldLife,
        goldStar
    }
    public CollectType m_collectType = CollectType.Coin;
   
    public List<GameObject> m_EffectList;
    private void OnEnable()
    {
        if(m_collectType == CollectType.Coin)
        {
            //transform.position = new Vector3(0, 1, 0);
            //this.GetComponent<DG.Tweening.DOTweenAnimation>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Respawn")
        {
            switch (m_collectType)
            {
                case CollectType.BoundsCoin:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.Life:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultLife > GameManager.Instance.Hp)
                    {
                        GameManager.Instance.Hp += 1;
                    }                    
                    UIManager.Instance.AddLife();
                    EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, transform.position);
                    break;
                case CollectType.Bullet:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetBullet);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultBulletCount > GameManager.Instance.BulletCount)
                    {
                        GameManager.Instance.BulletCount += 1;
                    }

                    UIManager.Instance.AddBullet();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;

            }
            if (m_EffectList.Count > 0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        if(collision.tag =="Player")
        {
            switch(m_collectType)
            {
                case CollectType.Coin:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect,transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;

            }
            if(m_EffectList.Count >0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            switch (m_collectType)
            {
                case CollectType.BoundsCoin:

                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.Life:
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultLife > GameManager.Instance.Hp)
                    {
                        GameManager.Instance.Hp += 1;
                    }
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetLife);
                    //GameManager.Instance.Hp += 1;
                    UIManager.Instance.AddLife();
                    EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, transform.position);
                    break;
                case CollectType.Bullet:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetBullet);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultBulletCount > GameManager.Instance.BulletCount)
                    {
                        GameManager.Instance.BulletCount += 1;
                    }

                    UIManager.Instance.AddBullet();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;
            }
            if (m_EffectList.Count > 0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        if (collision.tag == "Player")
        {
            switch (m_collectType)
            {
                case CollectType.Coin:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.Life:
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultLife > GameManager.Instance.Hp)
                    {
                        GameManager.Instance.Hp += 1;
                    }
                    UIManager.Instance.AddLife();
                    EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, transform.position);
                    break;
                case CollectType.Bullet:
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultBulletCount > GameManager.Instance.BulletCount)
                    {
                        GameManager.Instance.BulletCount += 1;
                    }
                    UIManager.Instance.AddBullet();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;
            }
            if (m_EffectList.Count > 0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            switch (m_collectType)
            {
                case CollectType.BoundsCoin:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.Life:
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetLife);
                    if (GameManager.Instance.defaultLife > GameManager.Instance.Hp)
                    {
                        GameManager.Instance.Hp += 1;
                    }
                    //GameManager.Instance.Hp += 1;
                    UIManager.Instance.AddLife();
                    EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, transform.position);
                    break;
                case CollectType.Bullet:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetBullet);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultBulletCount > GameManager.Instance.BulletCount)
                    {
                        GameManager.Instance.BulletCount += 1;
                    }

                    UIManager.Instance.AddBullet();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;
            }
            if (m_EffectList.Count > 0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Player")
        {
            switch (m_collectType)
            {
                case CollectType.Coin:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.Coin);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.totalCoinCount += 1;
                    UIManager.Instance.SetCoinText();
                    //this.GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();
                    break;
                case CollectType.Life:
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetLife);
                    if (GameManager.Instance.defaultLife > GameManager.Instance.Hp)
                    {
                        GameManager.Instance.Hp += 1;
                    }
                    UIManager.Instance.AddLife();
                    EffectController.Instance.ShowEffect(EffectController.EffectType.Heal, transform.position);
                    break;
                case CollectType.Bullet:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GetBullet);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    if (GameManager.Instance.defaultBulletCount > GameManager.Instance.BulletCount)
                    {
                        GameManager.Instance.BulletCount += 1;
                    }
                    UIManager.Instance.AddBullet();
                    break;
                case CollectType.goldLife:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldLife);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(25,0);
                    break;
                case CollectType.goldStar:
                    SoundManager.Instance.PlayFx(SoundManager.FxType.GoldStar);
                    EffectController.Instance.ShowEffect(EffectController.EffectType.o_Effect, transform.position);
                    GameManager.Instance.MoveFast(70,1);
                    break;
            }
            if (m_EffectList.Count > 0)
            {
                int effectRandom = Random.Range(0, m_EffectList.Count);
                m_EffectList[effectRandom].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
    private void OnBecameInvisible()
    {
        if(m_collectType == CollectType.Bullet)
        {
            //this.gameObject.SetActive(false);
        }
            
    }
}
