using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int score;
    public Text text;
    public int phaseThreeSceneNum;
    int count;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        ObjectInformation[] obj = GameObject.FindObjectsOfType(typeof(ObjectInformation)) as ObjectInformation[];
        count = obj.Length;
    }
    void Update()
    {
        text.text = "You have removed: " + score.ToString() + " / " + count;
        if(score >= count || GetComponent<Timer>().GetSliderVal() >= GetComponent<Timer>().slider.maxValue)
        {
            SceneManager.LoadScene(phaseThreeSceneNum);
        }
    }
}
