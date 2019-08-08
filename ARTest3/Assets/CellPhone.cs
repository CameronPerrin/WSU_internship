using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPhone : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public float closeEnoughDist;
    public Animation aniClip;
    bool rotate;
    // Update is called once per frame
    void Update()
    {
        if(rotate)
        {
            SwitchRotate();
        }
        if(CheckIfPlayerIsClose())
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {              
                rotate = false;
                SwitchRotate();
                //canvas.SetActive(true);
            }
        }
    }
    bool CheckIfPlayerIsClose()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (closeEnoughDist <= dist)
            return true;
        else
            return false;
    }
    void SwitchRotate()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void CallPhone()
    {
        GetComponent<Animator>().enabled = true;
    }
}
