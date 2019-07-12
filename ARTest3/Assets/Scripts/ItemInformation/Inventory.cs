using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InventoryInformation
{
    public string name;
    public MiniGame miniGameType;
    public ObjectInformation objectInformation;
}

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public List<InventoryInformation> inventoryInformation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MiniGame
{
    public enum MiniGameType
    {
        PUZZLE,
        OLD, // Will change soon
        SOMETHING // Will change soon
    }
}

