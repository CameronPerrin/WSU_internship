using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaster : MonoBehaviour
{
    private bool hovering = false;
    private bool grabbed = false;
    public GameObject target;
    public int speed;
    public bool movable;
    private GameObject replacer;

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("HOVERING!!!!!!!!!!!!!!!");
        hovering = true;
        if(collision.gameObject.tag == "Board")
        {
            //Debug.Log("hit board");
           //GameObject replacer = GameObject.Find(this.name);
           grabbed = false;
           this.transform.position = collision.gameObject.transform.position;
           this.movable = false;
            collision.gameObject.tag = "Filled";
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        Debug.Log("leaving :(");
        hovering = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)
        {
            if (hovering && (!grabbed))
            {
                if (Input.GetKeyDown("joystick button 0"))
                {
                    Debug.Log("You picked it up");
                    grabbed = true;
                }

            }
            if (grabbed)
            {
                float moveUD = Input.GetAxisRaw("Vertical") * speed;
                float moveLR = Input.GetAxisRaw("Horizontal") * speed;

                transform.position += new Vector3(moveLR, moveUD, 0) * (Time.deltaTime);

                if (Input.GetKeyUp("joystick button 0"))
                {
                    Debug.Log("You dropped it");
                    grabbed = false;
                }
            }
        }
    }


}
