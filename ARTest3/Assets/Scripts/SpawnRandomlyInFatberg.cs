using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnRandomlyInFatberg : MonoBehaviour
{
    public int spawnedAmount;
    public GameObject spawnedPrefab;
    public Inventory inventory;
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

    }
    void SpawnAtRandomPointsInsideGameObject(int i)
    {
        //random positions in the FatBerg
        float maxX = GetComponent<MeshRenderer>().bounds.extents.x * .75f;
        float maxY = GetComponent<MeshRenderer>().bounds.extents.y * .75f;
        float maxZ = GetComponent<MeshRenderer>().bounds.extents.z * .65f;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY,maxY), Random.Range(-maxZ, maxZ));
        vec += GetComponent<MeshRenderer>().bounds.center; // the random position
        Debug.Log(vec);
        InventoryInfo invenTemp = inventory.inventory[i];
        string objectName = invenTemp.objInfo.name;
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + objectName));
        GameObject prefabTemp = Instantiate(spawnedPrefab);
        temp.GetComponent<Rigidbody>().useGravity = false;
        temp.GetComponent<Rigidbody>().isKinematic = true;
        prefabTemp.GetComponent<ItemInfo>().desc = invenTemp.objInfo.description;
        prefabTemp.GetComponent<ItemInfo>().itemName = invenTemp.objInfo.name;
        prefabTemp.transform.position = vec;
        temp.transform.position = vec;
        temp.transform.parent = prefabTemp.transform;
        ScannerMove move = Camera.main.gameObject.GetComponent<ScannerMove>();
        move.items.Add(temp); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
