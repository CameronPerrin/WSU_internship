using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactController : MonoBehaviour
{

    public Color32 color = new Color32(244, 250, 107, 255);
    public Color32 origColor;
    public GameObject clicked;
    public bool other;

    public Text textBox;
	public Text nameBox;
	public GameObject textBoxContainer;

    void Start()
    {
        other = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (other && hit.transform.gameObject != clicked)
                {
                    clicked.GetComponent<MeshRenderer>().material.color = origColor;
                }
                clicked = hit.transform.gameObject;

                if (hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != color)
                {
                    origColor = hit.transform.gameObject.GetComponent<ItemInfo>().origColor;
                    other = true;
                }

                if (hit.transform != null && hit.transform.gameObject.GetComponent<MeshRenderer>().material.color == origColor)
                {     
                    //Debug statement to check click is working               
                    //PrintName(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = color;
                    textBoxContainer.SetActive(true);
                    nameBox.text = hit.transform.gameObject.GetComponent<ItemInfo>().itemName;
                    textBox.text = hit.transform.gameObject.GetComponent<ItemInfo>().desc;
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
                        textBoxContainer.SetActive(false);
                        nameBox.text = " ";
                        textBox.text = " ";
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
                clicked.GetComponent<MeshRenderer>().material.color = origColor;
                textBoxContainer.SetActive(false);
                nameBox.text = " ";
                textBox.text = " ";
            }  
        }
    }

    void PrintName(GameObject go)
    {
        print(go.name);
    }
  
}
