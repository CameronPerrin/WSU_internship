using System.Collections;
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
    //public bool 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cellPhone && !isDarkening)
        {
            if(cellPhone.cellPhoneHasBeenDisabled)
            {
                isDarkening = true;
            }
        }
        if(isDarkening && !isWaiting)
        {
            StartCoroutine(ScreenToBlack());
        }
    }
    public void Transition(CellPhone cell)
    {
        //Grab the cell phone
        cellPhone = cell;
        ShowCellPhone();
    }
    void ShowCellPhone()
    {
        cellPhone.CallPhone(textToText);
    }
    IEnumerator ScreenToBlack()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayForScreenToBlack);
        if(image.color.a <= .9)
        {
            Color color = image.color;
            color.a += amountToAdd;
            image.color = color;
            isWaiting = false;
        }
        else
        {
            yield return new WaitForSeconds(5f);
            image.gameObject.SetActive(false);
            //Activate Timeline
           // timeline.SetActive(true);
        }
    }
    
    
}
