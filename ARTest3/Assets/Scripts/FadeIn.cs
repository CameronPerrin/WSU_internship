using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public float delay;
    public float subtractBy;
    public GameObject activate;
    public bool isWaiting = false;
    public bool firstScene = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting && !firstScene)
            StartCoroutine(Delay());
        else if (!isWaiting && firstScene)
            StartCoroutine(FadeOutDelay());

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
    IEnumerator FadeOutDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        Color color;
        color = image.color;
        color.a += subtractBy;
        image.color = color;
        if (color.a >= 1f)
        {
            SceneManager.LoadScene(2);
        }
        else
            isWaiting = false;
    }
    public void FadeOut()
    {
        isWaiting = false;
    }
    public void FadeOut(Image i)
    {
        image = i;
        Color color = image.color;
        color.a = 0;
        image.color = color;
        
        if(!image.gameObject.activeInHierarchy)
        {
            image.gameObject.SetActive(true);
        }
        isWaiting = false;
    }
}
