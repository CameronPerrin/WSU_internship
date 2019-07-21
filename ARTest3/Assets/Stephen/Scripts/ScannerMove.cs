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
    public Quaternion camStartRot;
    public Transform camStartPos;

    //Variables to position object in proper place in front of camera
    public float distance;
    public float shift;
    public float shiftV;

    //List and varaibles used to house locations of objects in fatberg
    public List<GameObject> items;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;

    //Variable used for testing 
    public float away;

    //Text boxes and variables for text itself
    public Text itemName;
    public Text desc;
    public GameObject descContainer;
    public GameObject textBoxContainer;

    //Scanner objects: reticle and arrows
    public GameObject indicator;
    public List<GameObject> arrows;

    //Variable for currently selected item
    public GameObject clicked;


    //Lerp variables for moving objects
    private Quaternion oldRot;
    private Vector3 oldPos;
    private Vector3 newPos;
    public float speed;
    public float framesleft;

    private Ray ray;
    public RaycastHit hit;


    void Start()
    {
        cam = Camera.main.transform;
        camStartRot = cam.rotation;
        newPos = camStartPos.position + camStartPos.forward * distance 
                                            + camStartPos.right * shift 
                                            + camStartPos.up * shiftV;
        items.Add(one);
        items.Add(two);
        items.Add(three);
        items.Add(four);
        items.Add(five);
    }

    void Update()
    {
        if (!clicked)
        {
            //Camera rotation input
            y = Input.GetAxisRaw("PS X");
            x = Input.GetAxisRaw("PS Y");
            rotate = new Vector3(x * -1, y * -1, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;
        }

        ray = new Ray(cam.position, cam.forward);
        Debug.DrawRay(ray.origin,ray.direction,Color.blue);
        

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

                /* 
                //Position to move selected object to in front of camera position when clicked
                newPos = Camera.main.transform.position + Camera.main.transform.forward * distance 
                                            + Camera.main.transform.right * shift 
                                            + Camera.main.transform.up * shiftV;
                */
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
            /* 
            if(Vector3.Distance(items[0].transform.position, ray.GetPoint(25)) < 3f)
            {   
                indicator.GetComponent<SpriteRenderer>().color = Color.yellow;
            } 
            else if(Vector3.Distance(items[1].transform.position, ray.GetPoint(25)) < 3f)
            {
                indicator.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if(Vector3.Distance(items[2].transform.position, ray.GetPoint(25)) < 3f)
            {
                indicator.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if(Vector3.Distance(items[3].transform.position, ray.GetPoint(25)) < 3f)
            {
                indicator.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if(Vector3.Distance(items[4].transform.position, ray.GetPoint(25)) < 3f)
            {
                indicator.GetComponent<SpriteRenderer>().color = Color.white;
            }
            */

            foreach (var item in items)
            {
                if (Vector3.Distance(item.transform.position, ray.GetPoint(25)) < 3f)
                {
                    foreach(var arrow in arrows)
                    {
                        if (item == arrow.GetComponent<ArrowPoint>().target)
                        {
                            arrow.GetComponent<ArrowPoint>().arrow.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                        else
                        {
                            arrow.GetComponent<ArrowPoint>().arrow.GetComponent<SpriteRenderer>().color = new Color32(0, 214, 255, 255);
                        }
                    }
                }
                else if (Vector3.Distance(item.transform.position, ray.GetPoint(25)) > 3f)
                {
                    foreach(var arrow in arrows)
                    {
                        if (item == arrow.GetComponent<ArrowPoint>().target)
                        {
                            arrow.GetComponent<ArrowPoint>().arrow.GetComponent<SpriteRenderer>().color = new Color32(0, 214, 255, 255);
                        }
                        
                    }
                }
            }
        }

        //Moves item from fatberg to correct location in the "UI" as well as resets camera to original position
        if(framesleft > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, camStartRot, Time.deltaTime * speed);
            clicked.transform.position = Vector3.Lerp(clicked.transform.position, newPos, Time.deltaTime * speed);
            framesleft--;
        }
        
        //Puts item back when circle is pressed
        if (Input.GetButtonDown("Cancel") && clicked && framesleft == 0)
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
