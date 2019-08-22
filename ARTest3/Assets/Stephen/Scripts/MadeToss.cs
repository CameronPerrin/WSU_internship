﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().guage.GetComponent<GuageTimer>().AddToPercent(.5f);
                Inventory inven = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
                InventoryInfo inventoryInfo = new InventoryInfo(GetComponent<ObjectInformation>().objectInfo,GetComponent<ObjectInformation>());
                inven.GetComponent<Inventory>().AddToList(inventoryInfo);

                if (other.gameObject.GetComponentInChildren<ParticleSystem>() != null)
                {
                    other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                }


            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator Delay()
    {
        hasWaited = false;
        yield return new WaitForSeconds(1.5f);
        hasWaited = true;
    }
}
