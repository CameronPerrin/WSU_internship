using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaster : MonoBehaviour
{
    private bool hovered = false;
    public int speed;


    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("OnCollisionEnter2D");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
