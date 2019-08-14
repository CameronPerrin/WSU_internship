using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCenter : MonoBehaviour
{

    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        center = GetComponent<Renderer>().bounds.center;
        GetComponent<Renderer>().material.SetVector("Center Location", center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
