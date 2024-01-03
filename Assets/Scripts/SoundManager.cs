using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public delegate void SoundChangeEvent();
    public event SoundChangeEvent SoundChangeEventHandler;
    //public AudioMixerGroup mainMixer;
    public AudioMixer MasterMixer;
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton SoundManager == null");
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
    private void Start()
    {
        
    }
    public void SetSound()
    {
        if (GameManager.Instance.isMute)
        {
            MasterMixer.SetFloat("BGM_FX", -80f);
        }
        else
        {
            MasterMixer.SetFloat("BGM_FX", 0f);
        }
    }
    public void MuteSound(bool flag)
    {
        if(GameManager.Instance.isMute)
        {
            return;
        }
        else
        {
            if(flag)
            {
                MasterMixer.SetFloat("BGM_FX", -80f);
            }
            else
            {
                MasterMixer.SetFloat("BGM_FX", 0);
            }
        }
    }
    public AudioSource GoldLife;
    public AudioSource GoldStar;
    public AudioSource GoldChange;
    public AudioSource GunInit;
    public AudioSource NewGame;
    public List<AudioSource> ChestOpenList;
    public List<AudioSource> WoodList;
    public AudioSource StartButton;
    public List<AudioSource> BGM;
    public List<AudioSource> coins;
    public List<AudioSource> jumps;
    public List<AudioSource> hurts;
    public List<AudioSource> impacts;
    public List<AudioSource> impact_Monster;
    public List<AudioSource> ObjectImpact;
    public AudioSource Gun;
    public AudioSource Life;
    public AudioSource GetBullet;
    public AudioSource Death;
    public AudioSource Button;
    public AudioSource Popup;
    public enum FxType
    {
        Coin,
        Jump,
        Hurt,
        Impact,
        Impact_Monster,
        ObejctImpact,
        Gun,
        GetLife,
        GetBullet,
        Death,
        Button,
        Popup,
        Chest,
        Wood,
        StartGame,
        NewGame,
        GunInit,
        GoldStar,
        GoldLife,
        GoldChange

    }
    public void PlayLobby()
    {
        //BGM[1].Stop();
        StartCoroutine(FadeOut(BGM[1]));
        StartCoroutine(FadeIn(BGM[0],0.5f));
        //BGM[0].Play();
    }
    public void PlayGame()
    {
        StartCoroutine(FadeOut(BGM[0]));
        StartCoroutine(FadeIn(BGM[1],0.3f));
    }
    public void MuteSound()
    {
        GameManager.Instance.isMute = !GameManager.Instance.isMute;
        if(GameManager.Instance.isMute)
        {
            MasterMixer.SetFloat("BGM_FX", -80f);
        }
        else
        {
            MasterMixer.SetFloat("BGM_FX", 0f);
        }
        SoundChangeEventHandler?.Invoke();


    }
    public void PlayFx(FxType fxType)
    {
        int randPos = 0;
        switch(fxType)
        {
            case FxType.GoldStar:
                GoldStar.Play();
                break;
            case FxType.GoldLife:
                GoldLife.Play();
                break;
            case FxType.GoldChange:
                GoldChange.Play();
                break;
            case FxType.GunInit:
                GunInit.Play();
                break;
            case FxType.NewGame:
                NewGame.Play();
                break;
            case FxType.Chest:
                if (ChestOpenList.Count > 0)
                {
                    randPos = Random.Range(0, ChestOpenList.Count);
                    ChestOpenList[randPos].Play();
                }
                break;
            case FxType.Wood:
                if (WoodList.Count > 0)
                {
                    randPos = Random.Range(0, WoodList.Count);
                    WoodList[randPos].Play();
                }
                break;
            case FxType.StartGame:
                StartButton.Play();
                break;
            case FxType.Button:
                Button.Play();
                break;
            case FxType.Popup:
                Popup.Play();
                break;
            case FxType.Death:
                Death.Play();
                break;
            case FxType.GetBullet:
                GetBullet.Play();
                break;
            case FxType.GetLife:
                Life.Play();
                break;
            case FxType.Gun:
                Gun.Play();
                break;
            case FxType.ObejctImpact:
                if (ObjectImpact.Count > 0)
                {
                    randPos = Random.Range(0, ObjectImpact.Count);
                    ObjectImpact[randPos].Play();
                }
                break;
            case FxType.Coin:
                if(coins.Count >0)
                {
                    randPos = Random.Range(0, coins.Count);
                    coins[randPos].Play();
                }
                break;
            case FxType.Jump:
                if (jumps.Count > 0)
                {
                    randPos = Random.Range(0, jumps.Count);
                    jumps[randPos].Play();
                }
                break;
            case FxType.Hurt:
                if (hurts.Count > 0)
                {
                    randPos = Random.Range(0, hurts.Count);
                    hurts[randPos].Play();
                }
                break;
            case FxType.Impact:
                if (impacts.Count > 0)
                {
                    randPos = Random.Range(0, impacts.Count);
                    impacts[randPos].Play();
                }
                break;
            case FxType.Impact_Monster:
                if (impact_Monster.Count > 0)
                {
                    randPos = Random.Range(0, impact_Monster.Count);
                    impact_Monster[randPos].Play();
                }
                break;
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume / 10;

            yield return new WaitForSeconds(0.01f);
        }

        audioSource.Stop();
        audioSource.volume = 0;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float targetVolume)
    {
        audioSource.gameObject.SetActive(true);
        //float startVolume = 0f;
        audioSource.volume = 0;
        audioSource.Play();
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume / 10;

            yield return new WaitForSeconds(0.01f);
        }

        audioSource.volume = targetVolume;
    }

}
