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
    public float shiftV;

    //Distance list etc.
    public List<Vector3> distances;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public float away;

    //Test Scanner Beep
    public Text indicator;
    public Text itemName;
    public Text desc;
    public GameObject descContainer;
    public GameObject textBoxContainer;
    public GameObject clicked;
    private Vector3 oldPos;
    private Vector3 newPos;

    public float speed;
    private float startTime;
    public float length;

    private Quaternion oldRot;

    void Start()
    {
        cam = Camera.main.transform;
        distances.Add(one.transform.position);
        distances.Add(two.transform.position);
        distances.Add(three.transform.position);
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

                
                newPos = Camera.main.transform.position + Camera.main.transform.forward * distance 
                                            + Camera.main.transform.right * shift 
                                            + Camera.main.transform.up * shiftV;
                /*
                startTime = Time.time;
                length = Vector3.Distance(oldPos, newPos);
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / length;
                clicked.transform.position = Vector3.Lerp(clicked.transform.position, newPos, fracJourney);
                */

                clicked.transform.position = newPos;
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
            //Not currently working
            //foreach(var item in distances)
            //{
                if(Vector3.Distance(distances[0], ray.GetPoint(25)) < 3f || Vector3.Distance(distances[1], ray.GetPoint(25)) < 3f || Vector3.Distance(distances[2], ray.GetPoint(25)) < 3f)
                {   
                    //away = Vector3.Distance(distances[0], ray.GetPoint(25));
                    indicator.color = Color.red;
                } 
                else
                {
                    //away = Vector3.Distance(distances[0], ray.GetPoint(25));
                    indicator.color = Color.white;
                }
            //}
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

    private void MoveToFront()
    {
    
    }
}
