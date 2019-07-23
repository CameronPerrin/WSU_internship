using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadeToss : MonoBehaviour
{
    public GameObject item;
    bool hasWaited;
    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(Delay());
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NoSpawn")
        {
            Destroy(gameObject);
            if (hasWaited)
            {
                GameController controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>();
                controller.GetComponent<GameController>().score++;
                GameObject.FindGameObjectWithTag("Controller").GetComponent<Timer>().AddSliderVal(5f);
            }
        }
    }
    IEnumerator Delay()
    {
        hasWaited = false;
        yield return new WaitForSeconds(3f);
        hasWaited = true;
    }
}
