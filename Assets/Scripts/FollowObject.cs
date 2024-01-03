using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    float x_margin;
    [SerializeField]
    float y_margin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target !=null)
        {
            Vector2 pos = target.transform.position;
            pos.x = pos.x + x_margin;
            pos.y = pos.y + y_margin;
            transform.position = pos;
        }
    }
}
