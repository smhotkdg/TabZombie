using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSensor : MonoBehaviour
{
    [SerializeField]
    private GameObject detectObject;
    [SerializeField]
    private float Y_margin = 0.45f;
    [SerializeField]
    private float xMargin = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="ground")
        {
            if(collision.gameObject.activeSelf)
                detectObject = collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            if (collision.gameObject.activeSelf)
                detectObject = collision.gameObject;
        }
    }

    public float GetY()
    {
        if(detectObject!=null)
        {
            return detectObject.transform.position.y+ Y_margin;
        }
        else
        {
            return 0;
        }
    }
    public Vector2 GetPos()
    {
        if (detectObject != null)
        {            
            return new Vector2(detectObject.transform.position.x+xMargin, detectObject.transform.position.y + Y_margin);
        }
        else
        {
            return new Vector2(0,0);
        }
    }
}
