using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Sprite OffChest;
    public Sprite OnChest;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = OffChest;
        boxCollider.enabled = true;
        GameManager.Instance.chestCount++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            boxCollider.enabled = false;
            spriteRenderer.sprite = OnChest;
            ShowSomething();
            SoundManager.Instance.PlayFx(SoundManager.FxType.Chest);
        }
    }
    
    public void ShowSomething()
    {
        int rand = Random.Range(0, 10);
        Vector2 pos = transform.position;
        if (rand < 2)
        {
            pos.y = pos.y + 1f;
            EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsLife, pos, 1);
            EffectController.Instance.ShowEffect(EffectController.EffectType.Death, transform.position);
        }
        else if (rand < 4)
        {
            pos.y = pos.y + 1f;
            int bulletRand = Random.Range(1, 3);
            EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsBullet, pos, bulletRand);
            EffectController.Instance.ShowEffect(EffectController.EffectType.Death, transform.position);
        }
        else
        {
            pos.y = pos.y + 1f;
            int counRand = Random.Range(2, 10);
            EffectController.Instance.ShowEffect(EffectController.EffectType.BoundsCoin, pos, counRand);
            EffectController.Instance.ShowEffect(EffectController.EffectType.Death, transform.position);
        }
        int randGold = Random.Range(0, 100);
        if (GameManager.Instance.GameCount <4)
        {            
            if(GameManager.Instance.chestCount == 3)
            {
                EffectController.Instance.ShowEffect(EffectController.EffectType.goldStar, pos, 1);
            }
        }
        else if(GameManager.Instance.GameCount <10)
        {
            if(GameManager.Instance.chestCount ==2)
            {
                EffectController.Instance.ShowEffect(EffectController.EffectType.goldLife, pos, 1);
            }
        }
        else
        {
            if (randGold < 7)
            {
                EffectController.Instance.ShowEffect(EffectController.EffectType.goldStar, pos, 1);

            }
            else if (randGold < 15)
            {
                EffectController.Instance.ShowEffect(EffectController.EffectType.goldLife, pos, 1);
            }
        }
    }
    private void OnBecameInvisible()
    {
        //if (transform.position.x < GameManager.Instance.HeroList[GameManager.Instance.SelectHeroIndex].transform.position.x - 10)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }
}
