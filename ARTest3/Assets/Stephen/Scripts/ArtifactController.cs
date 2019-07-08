using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactController : MonoBehaviour
{

    public Color32 color = new Color32(244, 250, 107, 255);
    public Color32 origColor;
    private GameObject clicked;
    public bool other;

    public float distance;
    public float throwForce;
    public Transform cam;

    public Text textBox;
	public Text nameBox;
	public GameObject textBoxContainer;

    void Start()
    {
        other = false;
    }


    void Update()
    {
        if (Input.GetKeyDown("space") && clicked && clicked.transform.parent != null)
        {
            clicked.GetComponent<Rigidbody>().isKinematic = false;
            clicked.transform.parent = null;
            clicked.GetComponent<Rigidbody>().AddForce(cam.forward * throwForce);
            clicked.GetComponent<MeshRenderer>().material.color = origColor;
            clicked = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //Check if other item has been clicked, drop current item and replace with new item
                if (other && hit.transform.gameObject != clicked && clicked != null)
                {
                    clicked.GetComponent<MeshRenderer>().material.color = origColor;
                    clicked.transform.parent = null;
                    clicked.GetComponent<Rigidbody>().isKinematic = false;
                }
                clicked = hit.transform.gameObject;

                //If item not "highlighted" save it original color to revert back when needed (also note that an item has been clicked)
                if (hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != color)
                {
                    origColor = hit.transform.gameObject.GetComponent<ItemInfo>().origColor;
                    other = true;
                }

                //Check if something is hit and is not "highlighted" then change its color to "highlighted" and parent to camera
                //Else if clicked off current item, revert color to it's original, unparent, and change isKinematic back to false
                if (hit.transform != null && hit.transform.gameObject.GetComponent<MeshRenderer>().material.color == origColor)
                {     
                    //Debug statement to check click is working               
                    //PrintName(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = color;
                    clicked.GetComponent<Rigidbody>().isKinematic = true;
                    clicked.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
                    clicked.transform.parent = Camera.main.transform;
                    
                    //textBoxContainer.SetActive(true);
                    //nameBox.text = hit.transform.gameObject.GetComponent<ItemInfo>().itemName;
                    //textBox.text = hit.transform.gameObject.GetComponent<ItemInfo>().desc;
                }
                else
                {
                    /* Allow multiple items to be clicked
                    foreach (GameObject go in clicked)
                    {
                        go.GetComponent<MeshRenderer>().material.color = go.GetComponent<ItemInfo>().origColor;
                        textBoxContainer.SetActive(false);
                        nameBox.text = " ";
                        textBox.text = " ";
                    }
                    */
                    clicked.GetComponent<MeshRenderer>().material.color = origColor;
                    clicked.transform.parent = null;
                    clicked.GetComponent<Rigidbody>().isKinematic = false;
                    //textBoxContainer.SetActive(false);
                    //nameBox.text = " ";
                    //textBox.text = " ";
                }
            }
            else
            {
                /* Allow multiple items to be clicked
                foreach (GameObject go in clicked)
                {
                    go.GetComponent<MeshRenderer>().material.color = go.GetComponent<ItemInfo>().origColor;
                    textBoxContainer.SetActive(false);
                    nameBox.text = " ";
                    textBox.text = " ";
                }
                */
                //If no item is clicked, but one was previously clicked, revert to original color, unparent, and set isKinematic back to false
                if(clicked)
                {
                    clicked.GetComponent<MeshRenderer>().material.color = origColor;
                    clicked.transform.parent = null;
                    clicked.GetComponent<Rigidbody>().isKinematic = false;
                }
                //textBoxContainer.SetActive(false);
                //nameBox.text = " ";
                //textBox.text = " ";
            }  
        }
    }

    void PrintName(GameObject go)
    {
        print(go.name);
    }
  
}
