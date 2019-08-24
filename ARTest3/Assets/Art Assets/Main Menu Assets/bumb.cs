using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumb : MonoBehaviour
{
    public float speed;
    public bool shrink = false;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.localScale;

        if ((temp.x < 72) && !shrink)
        {
            temp.x += speed;
            temp.y += speed;
        }
        else
        {
            shrink = true;
        }

        if ((temp.x >= 68) && shrink)
        {
            temp.x -= (speed * 2f);
            temp.y -= (speed * 2f);
        }
        else
        {
            shrink = false;
        }



        transform.localScale = temp;
    }
}
