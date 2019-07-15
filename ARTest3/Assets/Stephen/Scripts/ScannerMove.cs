using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerMove : MonoBehaviour
{
    private float x;
    private float y;
    private Vector3 rotate;
    public Transform cam;

    //Test Scanner Beep
    public Text textBox;
    public Text itemName;

    void Start()
    {
        cam = Camera.main.transform;    
    }

    void Update()
    {
        y = Input.GetAxisRaw("PS X");
        x = Input.GetAxisRaw("PS Y");
        rotate = new Vector3(x * -1, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;

        Ray ray = new Ray(cam.position, cam.forward);
        Debug.DrawRay(ray.origin,ray.direction,Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log("Hit Sum'n");
            Debug.Log(hit.transform.gameObject);
            textBox.text = "!";

            if (Input.GetButtonDown("Submit"))
            {
                itemName.text = hit.transform.gameObject.name;
            }
        }
        else
        {
            textBox.text = ".";
            itemName.text = "";
        }
    }
}
