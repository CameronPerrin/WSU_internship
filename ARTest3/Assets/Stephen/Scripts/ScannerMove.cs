
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
    public GameObject GamePanel1;
    public GameObject GamePanel2;

    //Scanner objects: reticle and arrows
    public GameObject indicator;
    public List<GameObject> arrows;
    public GameObject arrowPrefab;
    public GameObject scanner;
    public Quaternion scannerDown;
    Vector3 startRotScanner = Vector3.zero;
    //Variable for currently selected item
    public GameObject clicked;
    //Used to Place Object into the correct Position
    public Transform uiPosition;

    //Lerp variables for moving objects
    private Quaternion oldRot;
    private Vector3 oldPos;
    public Vector3 newPos;
    public float speed;
    public float framesleft;

    //Raycast
    private Ray ray;
    public RaycastHit hit;

    //Variables for turning off info when minigame finished
    public GameObject minigame1;
    public GameObject minigame2;
    public int timePassed;
    public GameObject oldArrow;
    public GameObject arrowTarget;
    public GameObject A2continue;
    public bool inMiniGame;


    //test
    public Vector3 testRot;
    public AudioSource scannerFind;


    void Start()
    {
        cam = Camera.main.transform;
        camStartRot = cam.rotation;
       // newPos = camStartPos.position + camStartPos.forward * distance
                                      //      + camStartPos.right * shift
                                         //   + camStartPos.up * shiftV;
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
        //Change to "PRESS A TO CONTINUE"
        //Timer to leave information open before destroying object and moving to next minigame
        if (minigame1.GetComponent<NewJoystick>().Successes == 3)
        {
            A2continue.SetActive(true);
            timePassed = timePassed + 1;
            if (timePassed >= 100 && Input.GetButtonDown("Submit"))
            {
                timePassed = 0;
                minigame1.GetComponent<NewJoystick>().Successes = 0;
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
                itemName.text = "";
                desc1.text = "";
                desc2.text = "";
                desc3.text = "";
                descContainer.SetActive(false);
                A2continue.SetActive(false);
                inMiniGame = false;
            }
        }
        if (descContainer.activeInHierarchy)
        {
            inMiniGame = true;
        }
        if(!inMiniGame)
        {
            //Vector3 newRot = new Vector3(0, scanner.transform.localEulerAngles.y, scanner.transform.localEulerAngles.z);            
            scanner.transform.localEulerAngles = Vector3.Lerp(scanner.transform.localEulerAngles, new Vector3(scanner.transform.localEulerAngles.x,
                0f,scanner.transform.localEulerAngles.z), Time.deltaTime * 3f);
            //Debug.Log(scanner.transform.localEulerAngles);
        }
        
        
    }
    void Update()
    {
        Debug.Log(inMiniGame);
        if (!clicked)
        {
            //Camera rotation input
            y = Input.GetAxisRaw("Horizontal");
            x = Input.GetAxisRaw("Vertical");
            rotate = new Vector3(x * 1, y * -1, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;
            testRot = transform.localEulerAngles;
            //Constraints for camera rotation - PERFECT
            if (transform.localEulerAngles.x < 17f && transform.localEulerAngles.x > 15f)
            {
                transform.localEulerAngles = new Vector3(15, transform.localEulerAngles.y, 0);
            }
            if (transform.localEulerAngles.x > 343f && transform.localEulerAngles.x < 345f)
            {
                transform.localEulerAngles = new Vector3(345, transform.localEulerAngles.y, 0);
            }
            if (transform.localEulerAngles.y > 210f)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 210, 0);
            }
            if (transform.localEulerAngles.y < 150f)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 150, 0);
            }
            
            
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




                //play noise
                scannerFind.Play();







                framesleft = 15 * speed;

                int x = Random.Range(0, 2);

                clicked = hit.transform.parent.gameObject;
                oldPos = clicked.transform.position;
                oldRot = clicked.transform.rotation;
                //clicked.transform.parent = cam;

                //CHANGE FOR RANDOMIZE GAMES
                GamePanel1.SetActive(true);
               
                /* 
                if (x == 0)
                {
                    GamePanel1.SetActive(true);
                }
                else if (x == 1)
                {
                    GamePanel2.SetActive(true);
                }
                */

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
                desc1.GetComponent<TW_RandomText>().ORIGINAL_TEXT = clicked.GetComponent<ItemInfo>().desc1;
                desc2.text = clicked.GetComponent<ItemInfo>().desc2;
                desc2.GetComponent<TW_RandomText>().ORIGINAL_TEXT = clicked.GetComponent<ItemInfo>().desc2;
                desc3.text = clicked.GetComponent<ItemInfo>().desc3;
                desc3.GetComponent<TW_RandomText>().ORIGINAL_TEXT = clicked.GetComponent<ItemInfo>().desc3;
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
            Canvas canvas;
            scanner.transform.rotation = Quaternion.Lerp(scanner.transform.rotation, scannerDown, Time.deltaTime * 2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, camStartRot, Time.deltaTime * speed);
            Vector2 ViewportPosition = Camera.main.ViewportToWorldPoint(uiPosition.position);
            //Vector2 WorldObject_ScreenPosition = new Vector2(
            //    ((ViewportPosition.x * uiPosition.GetComponentInParent<Canvas>().pixelRect.x) - (uiPosition.GetComponentInParent<Canvas>().pixelRect.x * 0.5f)),
            //    ((ViewportPosition.y * uiPosition.GetComponentInParent<Canvas>().pixelRect.y) - (uiPosition.GetComponentInParent<Canvas>().pixelRect.y * 0.5f)));

            clicked.transform.localPosition = new Vector3(uiPosition.position.x, uiPosition.position.y, uiPosition.position.z);
            clicked.transform.GetChild(0).transform.localPosition = Vector3.Lerp(clicked.transform.GetChild(0).transform.localPosition,
                new Vector3(0f, clicked.transform.GetChild(0).transform.localPosition.y, clicked.transform.GetChild(0).transform.localPosition.z), Time.deltaTime * 1.5f);

            clicked.transform.rotation = oldRot;
            framesleft--;
            Debug.Log(ViewportPosition);
        }     
        if (clicked && (clicked.GetComponent<ItemInfo>().itemName == "Syringe" || clicked.GetComponent<ItemInfo>().itemName == "Sauce Packets" || clicked.GetComponent<ItemInfo>().itemName == "Toy Car"))
        {
            clicked.transform.GetChild(0).Rotate (Vector3.up * rotSpeed * Time.deltaTime);
        }
        else if (clicked && clicked.GetComponent<ItemInfo>().itemName != "Syringe")
        {
            clicked.transform.GetChild(0).Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
            //clicked.transform.localRotation
        }
        
        /* 
        //REMOVED BECAUSE NO NEED TO CANCEL GAME
        //Puts item back when circle is pressed
        if (Input.GetButtonDown("Cancel") && clicked && framesleft <= 0)
        {
            GamePanel1.SetActive(false);
            GamePanel2.SetActive(false);
            clicked.transform.parent = null;
            clicked.transform.position = oldPos;
            clicked.transform.rotation = oldRot;
            scanner.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            textBoxContainer.SetActive(true);
            clicked = null;
            descContainer.SetActive(false);
            itemName.text = "";
            desc1.text = "";
            desc2.text = "";
            desc3.text = "";
        }
        */


        
    }
}