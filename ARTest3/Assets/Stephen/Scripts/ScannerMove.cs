
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScannerMove : MonoBehaviour
{
    //Camera Rotation Variables
    public float x;
    public float y;
    private Vector3 rotate;
    public Transform cam;
    public Quaternion camStartRot;
    public Transform camStartPos;

    //Variables to position object in proper place in front of camera
    public float distance;
    public float shift;
    public float shiftV;

    //Item rotation speed
    public float rotSpeed;

    //List to house Fatberg objects
    public List<GameObject> items;

    //Variable used for testing 
    public float away;

    //Text boxes and variables for text itself
    public Text itemName;
    public Text desc1;
    public Text desc2;
    public Text desc3;
    public GameObject descContainer;
    public GameObject textBoxContainer;
    public GameObject GamePanel;

    //Scanner objects: reticle and arrows
    public GameObject indicator;
    public List<GameObject> arrows;
    public GameObject arrowPrefab;

    //Variable for currently selected item
    public GameObject clicked;


    //Lerp variables for moving objects
    private Quaternion oldRot;
    private Vector3 oldPos;
    private Vector3 newPos;
    public float speed;
    public float framesleft;

    //Raycast
    private Ray ray;
    public RaycastHit hit;

    //Variables for turning off info when minigame finished
    public GameObject minigame;
    public int timePassed;
    public GameObject oldArrow;
    public GameObject arrowTarget;

    void Start()
    {
        cam = Camera.main.transform;
        camStartRot = cam.rotation;
        newPos = camStartPos.position + camStartPos.forward * distance
                                            + camStartPos.right * shift
                                            + camStartPos.up * shiftV;
        SetUp();
    }

    void SetUp()
    {
        arrows = new List<GameObject>();
        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp = Instantiate(arrowPrefab);
            temp.transform.parent = GameObject.FindGameObjectWithTag("Reticles").transform;
            temp.GetComponent<ArrowPoint>().target = items[i];
            temp.transform.position = arrowPrefab.transform.position;
            temp.transform.localScale = new Vector3(1f, 1f, 1f);
            arrows.Add(temp);
            
        }
        arrowPrefab.SetActive(false);
    }

    void FixedUpdate()
    {
        //Timer to leave information open before destroying object and moving to next minigame
        if (minigame.GetComponent<NewJoystick>().Successes == 3)
        {
            timePassed = timePassed + 1;
            if (timePassed >= 100)
            {
                timePassed = 0;
                minigame.GetComponent<NewJoystick>().Successes = 0;
                //Removes arrow associated with clicked object
                foreach (var arrow in arrows)
                {
                    arrowTarget = clicked.transform.GetChild(0).gameObject;
                    if(arrow.GetComponent<ArrowPoint>().target == arrowTarget)
                    {
                        Destroy(arrow);
                    }
                }
                items.Remove(clicked);
                Destroy(clicked.gameObject);
                textBoxContainer.SetActive(true);
                clicked = null;
                descContainer.SetActive(false);
                itemName.text = "";
                desc1.text = "";
                desc2.text = "";
                desc3.text = "";
            }
        }
        
        
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

            //Loads EndScreen when all objects have been removed from the fatberg
            if(items.Count == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }
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

                clicked = hit.transform.parent.gameObject;
                oldPos = clicked.transform.position;
                oldRot = clicked.transform.rotation;
                clicked.transform.parent = cam;
                GamePanel.SetActive(true);

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
                desc1.text = clicked.GetComponent<ItemInfo>().desc1;
                desc2.text = clicked.GetComponent<ItemInfo>().desc2;
                desc3.text = clicked.GetComponent<ItemInfo>().desc3;
            }
        }
        else
        {
            //Distance check for changing reticle color when searching FB
            indicator.GetComponent<SpriteRenderer>().color = Color.white;

            items.RemoveAll(GameObject => GameObject == null);
            arrows.RemoveAll(GameObject => GameObject == null);

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
            clicked.transform.rotation = oldRot;
            framesleft--;
        }

        if (clicked && framesleft == 0 && (clicked.GetComponent<ItemInfo>().itemName == "Toy Car" || clicked.GetComponent<ItemInfo>().itemName == "Syringe" || clicked.GetComponent<ItemInfo>().itemName == "Sauce Packets"))
        {
            clicked.transform.Rotate(Vector3.down * rotSpeed * Time.deltaTime);
        }
        else if (clicked && framesleft == 0 && clicked.GetComponent<ItemInfo>().itemName != "Toy Car")
        {
            clicked.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
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
            desc1.text = "";
            desc2.text = "";
            desc3.text = "";
        }

        
    }
}