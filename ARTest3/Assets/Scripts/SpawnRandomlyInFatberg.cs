using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            SpawnAtRandomPointsInsideGameObject(i);
        }

        totalItems = inventory.inventory.Count;

    }
    void SpawnAtRandomPointsInsideGameObject(int i)
    {
        //random positions in the FatBerg
        float maxX = GetComponent<MeshRenderer>().bounds.extents.x * .65f;
        float maxY = GetComponent<MeshRenderer>().bounds.extents.y * .75f;
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
        //Possibly NEED TO ADD BOTTOM SHELL
        if (objectName == "Flushable Wipes")
        {
            //Prefect
            temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.1f);
        }
        else if(objectName == "bleach")
        {
            //Perfect
            temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.25f);
        }
        else if(objectName == "Car")
        {
            //Prefect
            temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, 0.1f);
        }
        else if(objectName == "oil")
        {
            //Perfect
            temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.15f);
        }
        else if (objectName == "FryingPan" || objectName == "Coffee Grounds" || objectName == "Pill")
        {
            //Prefect
            temp.transform.localPosition = temp.transform.localPosition + new Vector3(0f, 0f, -0.05f);
        }

        //Various cehcks to make sure items are proper size for fatberg
        if(objectName == "FryingPan")
        {
            //Perfect
            prefabTemp.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        else
        {
            //Perfect: Maybe adjust some smaller objects
            prefabTemp.transform.localScale = new Vector3(5f, 5f, 5f);
        }

        //Various checks to make items proper rotation when in fatberg/UI
        if(objectName == "Ketchup" || objectName == "Syrn")
        {
            prefabTemp.transform.rotation = new Quaternion(90f, 0f ,0f , 0f);
        }
        else if(objectName == "Car")
        {
            prefabTemp.transform.rotation = new Quaternion(180f, 0f, -90f, 0f);
        }

        ScannerMove move = Camera.main.gameObject.GetComponent<ScannerMove>();
        move.items.Add(temp);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
