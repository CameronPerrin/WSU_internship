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
    // Start is called before the first frame update
    void Start()
    {
        timeForFill = time * 0.01f; // Seconds...Since its 0 - 1 if we go by time .delta time
       // arrow.transform.eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y, 245);
    }

    // Update is called once per frame
    void Update()
    {
        timeInc += Time.deltaTime;
        float filled = meter.GetComponent<Image>().fillAmount;
        float percentage = timeInc / time;
        timeForFill = percentage;
        //float fillIncrease = timeForFill * Time.deltaTime;
        meter.GetComponent<Image>().fillAmount = timeForFill;
       
        float zPer = 100f - percentage;
        arrow.GetComponent<Transform>().eulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y,
            zPer * 240f);

    }
}
