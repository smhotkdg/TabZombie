using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private void OnDisable()
    {
        if (GameManager.Instance.SpecialOfferTime > 0)
        {
            UIManager.Instance.SpeacialOfferPanel.SetActive(true);
        }
    }
}
