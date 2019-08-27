using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellPhone : MonoBehaviour
{
    public GameObject textMessage;
    public GameObject player;
    public float closeEnoughDist;
    bool rotate;
    [HideInInspector]
    public bool playerHasSeenTextMessage;
    Vector2 normSize;
    public bool cellPhoneHasBeenDisabled;
    AutoTypeText auto;
    string transText;
    bool secondRun;
    GuageTimer guage;
    bool isStopping = false;
    //public AudioSource clip;
    //Alright so the reason the object keeps disappearing is because the renderer is turned off when moving off screen.
    // Update is called once per frame
    private void Start()
    {
        guage = GameObject.Find("Guage").GetComponent<GuageTimer>();
        StartCoroutine(work());
    }
    void Update()
    {
        Debug.Log(playerHasSeenTextMessage);
        if (rotate)
        {
            SwitchRotate();
        }
        if (CheckIfPlayerIsClose() && !playerHasSeenTextMessage && !isStopping)
        {
            //play ringtone
            
           // GetComponent<AudioSource>().Play();
                rotate = false;
                SwitchRotate();
                textMessage.SetActive(!textMessage.activeInHierarchy);
                //playerHasSeenTextMessage = true;
                StartCoroutine(StopScreenAni());
            isStopping = true;
                if (!auto && secondRun)
                {
                    auto = textMessage.GetComponentInChildren<AutoTypeText>();
                    auto.RestartAutoType(transText);
                

                }
        }
        else if(playerHasSeenTextMessage)
        {
                    guage.CanStart();
                //textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
                textMessage.SetActive(false);
                cellPhoneHasBeenDisabled = true;
                if (secondRun)
                    SceneManager.LoadScene(2);
            
                /*  if(textMessage.activeInHierarchy)
                      textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
                  if(!CheckIfPlayerIsClose())
                      textMessage.SetActive(false);*/
        }
        
        if (textMessage.activeInHierarchy)
        {
            cellPhoneHasBeenDisabled = false;
            Transform trans = player.transform;
            Vector3 vec = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            textMessage.transform.LookAt(trans);
            textMessage.transform.eulerAngles = new Vector3(textMessage.transform.eulerAngles.x - 10f, textMessage.transform.eulerAngles.y + 180f, textMessage.transform.eulerAngles.z);
            //textMessage.transform.localRotation.x = new Quaternion(0, 0, 0, 0);
            //textMessage.transform.rotation = new Quaternion(textMessage.transform.rotation.x, textMessage.transform.rotation.y + 180f,
            //textMessage.transform.rotation.z, textMessage.transform.rotation.w);
        }


        Debug.Log(cellPhoneHasBeenDisabled);

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
    public void CallPhone(string s)
    {
        secondRun = true;
      // textMessage.GetComponent<RectTransform>().sizeDelta = normSize;
        rotate = false;
        isStopping = false;
      // GetComponent<Animator>().enabled = true;
        playerHasSeenTextMessage = false;
        textMessage.GetComponentInChildren<Text>().text = "";
        GetComponent<Animator>().enabled = true;
        cellPhoneHasBeenDisabled = false;
        
        transText = s;
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
    IEnumerator work()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            // Do some work
            if (CheckIfPlayerIsClose() && !playerHasSeenTextMessage)
            {
                //play ringtone

                GetComponent<AudioSource>().Play();
            }
        }
    }
}
