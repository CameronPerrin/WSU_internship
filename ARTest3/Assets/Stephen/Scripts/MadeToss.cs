using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadeToss : MonoBehaviour
{
    public GameObject item;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
