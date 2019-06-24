using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRaycast : MonoBehaviour
{
    public GameObject obj;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Fatberg")
        {
            Debug.Log("Entered");
            hit.gameObject.layer = 2;
            obj = hit.gameObject;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Fatberg")
        {
            hit.gameObject.layer = 0;
            obj = hit.gameObject;
        }
    }
}
