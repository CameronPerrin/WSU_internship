using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerMove : MonoBehaviour
{
    //Camera Rotation Variables
    private float x;
    private float y;
    private Vector3 rotate;
    public Transform cam;
    //Variables to position object in proper place in front of camera
    public float distance;
    public float shift;
    public float shiftV;

    //List and varaibles used to house locations of objects in fatberg
    public List<Vector3> distances;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    //Variable used for testing 
    public float away;

    //Text boxes and variables for text itself
    public Text itemName;
    public Text desc;
    public GameObject descContainer;
    public GameObject textBoxContainer;

    //Test Scanner Beep for reticles and arrows
    public GameObject indicator;

    //Variable for currently selected item
    public GameObject clicked;


    //Lerp variables for moving objects
    private Quaternion oldRot;
    private Vector3 oldPos;
    private Vector3 newPos;
    public float speed;
    public float framesleft;


    void Start()
    {
        cam = Camera.main.transform;
        distances.Add(one.transform.position);
        distances.Add(two.transform.position);
        distances.Add(three.transform.position);
    }

    void Update()
    {
        //Camera rotation input
        y = Input.GetAxisRaw("PS X");
        x = Input.GetAxisRaw("PS Y");
        rotate = new Vector3(x * -1, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;

        Ray ray = new Ray(cam.position, cam.forward);
        Debug.DrawRay(ray.origin,ray.direction,Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            //Debug.Log("Hit Sum'n");
            //Debug.Log(hit.transform.gameObject);
            indicator.GetComponent<SpriteRenderer>().color = Color.red;

            if (Input.GetButtonDown("Submit") && !clicked)
            {
                framesleft = 15 * speed;

                clicked = hit.transform.gameObject;
                oldPos = clicked.transform.position;
                oldRot = clicked.transform.rotation;
                clicked.transform.parent = cam;

                //Position to move selected object to in front of camera
                newPos = Camera.main.transform.position + Camera.main.transform.forward * distance 
                                            + Camera.main.transform.right * shift 
                                            + Camera.main.transform.up * shiftV;
                

                //clicked.transform.position = newPos;
                clicked.transform.rotation = new Quaternion(0,0,0,0);
                textBoxContainer.SetActive(false);
                descContainer.SetActive(true);
                itemName.text = clicked.GetComponent<ItemInfo>().itemName;
                desc.text = clicked.GetComponent<ItemInfo>().desc;
            }
        }
        else
        {
            //Distance check for changing reticle color when searching FB
            indicator.GetComponent<SpriteRenderer>().color = Color.white;
            if(Vector3.Distance(distances[0], ray.GetPoint(25)) < 3f || Vector3.Distance(distances[1], ray.GetPoint(25)) < 3f || Vector3.Distance(distances[2], ray.GetPoint(25)) < 3f)
            {   
                //away = Vector3.Distance(distances[0], ray.GetPoint(25));
                indicator.GetComponent<SpriteRenderer>().color = Color.yellow;
            } 
            else
            {
                //away = Vector3.Distance(distances[0], ray.GetPoint(25));
                indicator.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        //Moves item from fatberg to correct location in the "UI"
        if(framesleft > 0)
        {
            clicked.transform.position = Vector3.Lerp(clicked.transform.position, newPos, Time.deltaTime * speed);
            framesleft--;
        }
        
        //Puts item back when circle is pressed
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
