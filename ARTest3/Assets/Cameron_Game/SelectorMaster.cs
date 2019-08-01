using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMaster : MonoBehaviour
{
    //private Vector3 moveDirection = Vector3.zero;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float moveUD = Input.GetAxisRaw("Vertical")*speed;
        float moveLR = Input.GetAxisRaw("Horizontal")*speed;

        transform.position += new Vector3(moveLR, moveUD, 0) * Time.deltaTime;


       //Vector3 movement = new Vector3(moveUD, 0, 0);


    }

}
