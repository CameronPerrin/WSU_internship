using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public float delay;
    public float subtractBy;
    public GameObject activate;
    bool isWaiting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
            StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        Color color;
        color = image.color;
        color.a -= subtractBy;
        image.color = color;      
        if(color.a <= 0.1f)
        {
            activate.SetActive(true);
        }
        else
            isWaiting = false;
    }
}
