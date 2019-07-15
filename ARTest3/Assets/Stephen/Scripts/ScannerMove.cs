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
    public float distance;
    public float shift;

    //Test Scanner Beep
    public Text indicator;
    public Text itemName;
    public Text desc;
    public GameObject descContainer;
    public GameObject textBoxContainer;
    public GameObject clicked;
    private Vector3 oldPos;
    private Quaternion oldRot;

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
            indicator.text = "!";

            if (Input.GetButtonDown("Submit") && !clicked)
            {
                clicked = hit.transform.gameObject;
                oldPos = clicked.transform.position;
                oldRot = clicked.transform.rotation;
                clicked.transform.parent = cam;
                clicked.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance  + Camera.main.transform.right * shift;
                clicked.transform.rotation = new Quaternion(0,0,0,0);
                textBoxContainer.SetActive(false);
                descContainer.SetActive(true);
                itemName.text = clicked.GetComponent<ItemInfo>().itemName;
                desc.text = clicked.GetComponent<ItemInfo>().desc;
            }
        }
        else
        {
            indicator.text = ".";
        }

        if (Input.GetButtonDown("Cancel") && clicked)
            {
                clicked.transform.parent = null;
                clicked.transform.position = oldPos;
                clicked.transform.rotation = oldRot;
                textBoxContainer.SetActive(true);
                clicked = null;
                descContainer.SetActive(false);
                itemName.text = "";
                desc.text = "";
            }
    }
}
