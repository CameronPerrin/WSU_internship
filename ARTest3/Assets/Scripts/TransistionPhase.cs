﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TransistionPhase : MonoBehaviour
{
    public float delayForScreenToBlack;
    public float amountToAdd;
    bool isDarkening;
    bool isWaiting;
    public Image image;
    [HideInInspector]
    public CellPhone cellPhone;
    public string textToText;
    public GameObject timeline;
    public AutoTypeText auto;
    public Transform trans;
    public GameObject player;
    public ArtifactController controller;
    //public bool 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cellPhone && !isWaiting)
        {
            StartCoroutine(ScreenToBlack());
        }
    }
    public void Transition(CellPhone cell)
    {
        //Grab the cell phone
        cellPhone = cell;        
       // ShowCellPhone();
    }
    void ShowCellPhone()
    {
        cellPhone.fadeImage = image;
        cellPhone.CallPhone(textToText);
    }
    IEnumerator ScreenToBlack()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayForScreenToBlack);
        if(image.color.a <= 1)
        {
            Color color = image.color;
            color.a += amountToAdd;
            image.color = color;
            isWaiting = false;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            controller.cannotPickUp = true;
            yield return new WaitForSeconds(1f);
            ShowCellPhone();
            
            image.gameObject.SetActive(false);
            //Activate Timeline
            player.transform.position = new Vector3(trans.position.x, player.transform.position.y, trans.position.z);
            player.transform.GetChild(0).transform.eulerAngles = trans.eulerAngles;
            player.GetComponent<FPController>().enabled = false;
            // timeline.SetActive(true);
        }
    }
    
    
}
