using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraFilterPack_Drawing_Manga_Flash Manga_flash;

    public CameraFilterPack_Blizzard Blizzard;
    public CameraFilterPack_Atmosphere_Rain Rain;

    public CameraFilterPack_AAA_Blood_Hit bloodHit;
    public CameraFilterPack_OldFilm_Cutting2 cutting;
    public CameraFilterPack_FX_EarthQuake earthQuake;
    private void Awake()
    {
        BloodRoutine = bloodEffectRoutine();
    }
    public void EarthQuake(bool flag)
    {
        earthQuake.enabled = flag;
    }
    public void Shake(float _time)
    {
        CameraPlay.EarthQuakeShake(_time);
    }
    public void MangaEffect(bool isEnable)
    {
        if(!Manga_flash.enabled == isEnable)
        {
            Manga_flash.enabled = isEnable;
        }       
    }
    public void SetWeather()
    {
        Blizzard.enabled = false;
        Rain.enabled = false;
        int rand = Random.Range(0, 100);
        if(rand <10)
        {
            Blizzard.enabled = true;
        }
        else if(rand <30)
        {
            Rain.enabled = true;
        }
    }
    public void disableWeather()
    {
        Blizzard.enabled = false;
        Rain.enabled = false;
    }
    IEnumerator BloodRoutine;
    bool bStartBlood = false;
    public void ShowBloodEffect(bool flag)
    {       
        
        //bloodHit.Blood_Hit_Full_3 = 0;
        if (flag)
        {            
            if(bStartBlood ==false)
            {
                bloodHit.Blood_Hit_Full_1 = 0.7f;
                StartCoroutine(BloodRoutine);
                bStartBlood = true;
            }
            
        }
        else
        {
            if(bStartBlood)
            {
                bloodHit.Blood_Hit_Full_1 = 0;
                StopCoroutine(BloodRoutine);
                bStartBlood = false;
            }
            
        }
    }
    IEnumerator bloodEffectRoutine()
    {
        while(true)
        {
            bloodHit.Blood_Hit_Full_1 = 0.7f;
            for (int i = 0; i < 10; i++)
            {
                bloodHit.Blood_Hit_Full_1 += 0.03f;
                yield return new WaitForSeconds(0.05f);
                
            }
            bloodHit.Blood_Hit_Full_1 = 1f;
            for (int i = 0; i < 10; i++)
            {
                bloodHit.Blood_Hit_Full_1 -= 0.03f;
                yield return new WaitForSeconds(0.05f);
                
            }
        }
    }
    public void ShowCutting(bool flag)
    {
        cutting.enabled = flag;
    }
}
