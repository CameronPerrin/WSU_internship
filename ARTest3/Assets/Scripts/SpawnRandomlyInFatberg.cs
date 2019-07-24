using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnRandomlyInFatberg : MonoBehaviour
{
    public int spawnedAmount;
    public GameObject spawnedPrefab;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        //List<InventoryInfo> inven = inventory.inventory.OrderBy(s => s.objInfo.name).ToList();
        
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            SpawnAtRandomPointsInsideGameObject(i);
        }
    }
    void SpawnAtRandomPointsInsideGameObject(int i)
    {

        float maxX = GetComponent<MeshRenderer>().bounds.extents.x * .75f;
        float maxY = GetComponent<MeshRenderer>().bounds.extents.y * .75f;
        float maxZ = GetComponent<MeshRenderer>().bounds.extents.z * .75f;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY,maxY), Random.Range(-maxZ, maxZ));
        vec += GetComponent<MeshRenderer>().bounds.center; // the random position
        InventoryInfo invenTemp = inventory.inventory[i];
        string objectName = invenTemp.objInfo.name;
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + objectName));
        GameObject prefabTemp = Instantiate(spawnedPrefab);
        prefabTemp.GetComponent<ItemInfo>().desc = invenTemp.objInfo.description;
        prefabTemp.GetComponent<ItemInfo>().itemName = invenTemp.objInfo.name;
        prefabTemp.transform.position = vec;
        temp.transform.position = vec;
        temp.transform.parent = prefabTemp.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
