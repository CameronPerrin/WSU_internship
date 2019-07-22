using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float endTime;
    [HideInInspector]
    public Text text;
    public Slider slider;

    void Start()
    {
        slider.maxValue = endTime;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += Time.deltaTime;
    }
    public float GetSliderVal()
    {
        return slider.value;
    }
    public void AddSliderVal(float val)
    {
        slider.value += val;
    }
    
}
