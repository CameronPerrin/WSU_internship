using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuageTimer : MonoBehaviour
{
    public Image meter;
    public Image arrow;
    public float time;
    float timeForFill;
    float timeInc;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        timeForFill = time * 0.01f; // Seconds...Since its 0 - 1 if we go by time .delta time
        // arrow.transform.eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y, 245);
    }

    // Update is called once per frame
    void Update()
    {
      //  float filled;
         timeInc += Time.deltaTime;  
      //  filled = meter.GetComponent<Image>().fillAmount + 0.15f;
        float percentage = timeInc / time;
        timeForFill = percentage;
        //float fillIncrease = timeForFill * Time.deltaTime;
        //if(offset != 0)
        //    meter.GetComponent<Image>().fillAmount = timeForFill + .15f;
        //  else
        if (offset != 0)
        {
            meter.GetComponent<Image>().fillAmount += (Time.deltaTime + 0.15f);
            offset = 0;
        }
        else
            meter.GetComponent<Image>().fillAmount += (Time.deltaTime);
        float zPer = 100 - (percentage * 100f);
        Debug.Log(zPer/100 + " : " + meter.GetComponent<Image>().fillAmount);
        arrow.GetComponent<Transform>().eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y,
            (zPer/100f) * 245f);
    }
}
