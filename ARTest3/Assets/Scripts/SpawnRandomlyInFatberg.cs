﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnRandomlyInFatberg : MonoBehaviour
{
    public int spawnedAmount;
    public GameObject spawnedPrefab;
    public Inventory inventory;
    public int totalItems;
    public List<GameObject> spawnedObjects;
    // Start is called before the first frame update
    void Awake()
    {
        // We are getting the inventory by finding the tag
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        //List<InventoryInfo> inven = inventory.inventory.OrderBy(s => s.objInfo.name).ToList();
        int[] randomNumbers = new int[6];
        if (inventory.inventory.Count > 6)
        {
            for (int i = 0; i < 6; i++)
            {
                int num = Random.Range(0, inventory.inventory.Count);
                for (int k = 0; k < 6; k++)
                {
                    while (randomNumbers[k] == num)
                    {
                        num = Random.Range(0, inventory.inventory.Count);
                    }
                }
                randomNumbers[i] = num;

            }
        }
        else
        {
            for (int i = 0; i < inventory.inventory.Count - 1; i++)
            {
                randomNumbers[i] = i;
                Debug.Log(i);
            }
        }
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            SpawnAtRandomPointsInsideGameObject(randomNumbers[i]);
            Debug.Log(inventory.inventory[i].spawn);
        }
        

    }
    void SpawnAtRandomPointsInsideGameObject(int i)
    {
        //random positions in the FatBerg
        float maxX = GetComponent<MeshRenderer>().bounds.extents.x * .65f;
        float maxY = GetComponent<MeshRenderer>().bounds.extents.y * .55f;
        float maxZ = GetComponent<MeshRenderer>().bounds.extents.z * .3f;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY,maxY), Random.Range(-maxZ, maxZ));
        vec += GetComponent<MeshRenderer>().bounds.center; // the random position
        Debug.Log(vec);
        InventoryInfo invenTemp = inventory.inventory[i];
        string objectName = invenTemp.objInfo.name;
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + objectName));
        GameObject prefabTemp = Instantiate(spawnedPrefab);
        temp.GetComponent<Rigidbody>().useGravity = false;
        temp.GetComponent<Rigidbody>().isKinematic = true;
        prefabTemp.GetComponent<ItemInfo>().desc1 = temp.GetComponent<ItemInfo>().desc1;
        prefabTemp.GetComponent<ItemInfo>().desc2 = temp.GetComponent<ItemInfo>().desc2;
        prefabTemp.GetComponent<ItemInfo>().desc3 = temp.GetComponent<ItemInfo>().desc3;
        prefabTemp.GetComponent<ItemInfo>().itemName = temp.GetComponent<ItemInfo>().itemName;
        prefabTemp.transform.position = vec;
        temp.transform.position = vec;
        temp.transform.parent = prefabTemp.transform;

        //Various item checks to change item locations for better placement in UI when clicked
        if (objectName == "Flushable Wipes")
        {
            //Prefect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.13f);
        }
        else if(objectName == "Bottom Shell")
        {
            //Prefect
          //  temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, 0.05f);
        }
        else if (objectName == "Top Egg")
        {
            //Prefect
           // temp.transform.localPosition = temp.transform.localPosition + new Vector3(-0.06f, 0.35f, -0.09f);
        }
        else if(objectName == "bleach")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(-.15f, -.3f, -0.28f);
        }
        else if(objectName == "Car")
        {
            //Prefect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(-0.38f, 0, 0);
        }
        else if(objectName == "oil")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(-0.353f, 0, -0.131f);
        }
        else if (objectName == "Pill")
        {
            //Prefect
           // temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.05f);
        }
        else if (objectName == "lego")
        {
            //Prefect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(.2f, 0f, 0f);
        }
        else if (objectName == "Coffee Grounds")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.06f);
        }
        else if (objectName =="FryingPan")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(-0.1f, 0f, -0.1f);
        }
        else if(objectName == "Ketchup")
        {
            //Perfect // -3.1 // 1.787 //11.46
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(-0.6f, -0.025f, 0f);
        }
        else if (objectName == "Syrn")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(0f, -0.05f, 0f);
        }
        else if (objectName == "Floss" || objectName == "PillBottle")
        {
            //Perfect
            temp.transform.parent.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.025f);
        }

        //Various cehcks to make sure items are proper size for fatberg
        if(objectName == "FryingPan" || objectName == "bleach")
        {
            //Perfect
            prefabTemp.transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (objectName == "Pill" || objectName == "lego")
        {
            prefabTemp.transform.localScale = new Vector3(6f, 6f, 6f);
        }
        else if (objectName == "Car")
        {
            prefabTemp.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        else
        {
            //Perfect: MAYBE ADJUST CAR SIZE TO BE SMALLER
            prefabTemp.transform.localScale = new Vector3(5f, 5f, 5f);
        }

        //Various checks to make items proper rotation when in fatberg/UI
        if(objectName == "Syrn" || objectName == "Ketchup")
        {
            prefabTemp.transform.rotation = new Quaternion(0f, 0f ,0f , 0f);
        }

        ScannerMove move = Camera.main.gameObject.GetComponent<ScannerMove>();
        move.items.Add(temp);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
