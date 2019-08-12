using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTypeText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cellPhone;
    public float typeDelay;
    public string textToType;
    bool isWaiting = false;
    int length = 0;

    // Update is called once per frame
    void Update()
    {
        if(!isWaiting)
        {
            if (length <= textToType.Length - 1)
                StartCoroutine(Delay());
            else
            {
                cellPhone.GetComponent<CellPhone>().playerHasSeenTextMessage = true;
                isWaiting = true;
            }
            //else do nothing its done
        }
    }
    IEnumerator Delay()
    {
        isWaiting = true;
        GetComponent<Text>().text += textToType[length];
        length++;
        yield return new WaitForSeconds(typeDelay);
        isWaiting = false;
    }
    public void RestartAutoType(string text)
    {
        textToType = text;
        isWaiting = false;
        length = 0;
    }
}
