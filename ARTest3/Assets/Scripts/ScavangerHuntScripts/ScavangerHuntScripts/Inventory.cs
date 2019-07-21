using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InventoryInfo // Have to make sure this stays between scenes etc.
{
    public InventoryInfo(SpawnedInformation spawned, ObjectInformation objects)
    {
        spawn = spawned;
        objInfo = objects;
    }
    public SpawnedInformation spawn;
    public ObjectInformation objInfo;
}

public class Inventory : MonoBehaviour
{
    static bool doesInvenExist;
    public List<InventoryInfo> inventory;
    private void Awake()
    {
        if (!doesInvenExist)
        {
            inventory = new List<InventoryInfo>();
            doesInvenExist = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void AddToList(InventoryInfo inven)
    {
        if(inventory != null)
            inventory.Add(inven);
    }
    
    public void ShowList()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log(inventory[i].spawn.name);
        }
    }
}
