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
    [HideInInspector]
    public float percentage;
    float additive = 0;
    bool isStarting = false;
    // Start is called before the first frame update
    void Awake()
    {
        time = GameObject.Find("GameController").GetComponent<Timer>().endTime;
        timeForFill = time * 0.01f; // Seconds...Since its 0 - 1 if we go by time .delta time
        meter.GetComponent<Image>().fillAmount = 0;
        // arrow.transform.eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y, 245);
    }

    // Update is called once per frame
    void Update()
    {
        //  float filled//After the text message we can start
        if (isStarting)
        {
            //Debug.Log(percentage);
            if (percentage < 1)
            {
                timeInc += Time.deltaTime;
                //  filled = meter.GetComponent<Image>().fillAmount + 0.15f;
                percentage = (timeInc + additive) / time;
                if (percentage > 1)
                    percentage = .98f;
                //timeForFill = percentage + offset;
                //float fillIncrease = timeForFill * Time.deltaTime;
                //if(offset != 0)
                //    meter.GetComponent<Image>().fillAmount = timeForFill + .15f;
                //  else
                //meter.GetComponent<Image>().fillAmount = (percentage);
                float zPer = 100 - (percentage * 100f);
                //Debug.Log(zPer/100 + " : " + meter.GetComponent<Image>().fillAmount);

                arrow.GetComponent<Transform>().eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y,
                  (zPer / 100f) * 243f);
            }
            
        }
    }
    public void AddToPercent(float amount)
    {
        additive += amount;
    }
    public void CanStart()
    {
        isStarting = true;
    }
}
