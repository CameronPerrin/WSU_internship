using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPhone : MonoBehaviour
{
    public GameObject textMessage;
    public GameObject player;
    public float closeEnoughDist;
    bool rotate;
    [HideInInspector]
    public bool playerHasSeenTextMessage;
    Vector2 normSize;
    // Update is called once per frame
    void Update()
    {
        if(rotate)
        {
            SwitchRotate();
        }
        if (CheckIfPlayerIsClose() && !playerHasSeenTextMessage)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                rotate = false;
                SwitchRotate();
                textMessage.SetActive(!textMessage.activeInHierarchy);
                //playerHasSeenTextMessage = true;
                StartCoroutine(StopScreenAni());
            }
        }
        else if(playerHasSeenTextMessage)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
                textMessage.SetActive(false);
            }
                /*  if(textMessage.activeInHierarchy)
                      textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
                  if(!CheckIfPlayerIsClose())
                      textMessage.SetActive(false);*/
        }


    }
    bool CheckIfPlayerIsClose()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (closeEnoughDist <= dist)
            return true;
        else
        {
            //textMessage.SetActive(false);
            return false;
        }
    }
    void SwitchRotate()
    {
        GetComponent<Animator>().enabled = false;
    }
    //Used to call the cell phone after you called it the first time
    public void CallPhone()
    {
      // textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
        rotate = false;
      // GetComponent<Animator>().enabled = true;
        playerHasSeenTextMessage = false;
    }
    IEnumerator StopScreenAni()
    {
       // normSize = textMessage.GetComponent<RectTransform>().sizeDelta;
       // Vector3 pos = new Vector2(-1.22f, 2.54f);
       // Debug.Log(pos);
       // textMessage.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(.3f);
       // textMessage.GetComponent<Animator>().enabled = false;
       // textMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(132.51f, 111.61f);
       // textMessage.GetComponent<RectTransform>().localPosition = pos;
    }
}
