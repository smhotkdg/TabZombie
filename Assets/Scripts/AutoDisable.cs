using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }
    public void DestoryAnim()
    {
        Destroy(this.gameObject);
    }
}
