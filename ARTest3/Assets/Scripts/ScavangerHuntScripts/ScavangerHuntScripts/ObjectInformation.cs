using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    public SpawnedInformation objectInfo;
    public string name;
    public string description;
    public bool flushable;
    [HideInInspector]
    public GameObject plane;
    public bool hasCollided;
    [HideInInspector]
    public ScavengerHunt hunt;
    void Awake()
    {
        objectInfo.name = name;
        objectInfo.information = description;
        objectInfo.flushable = flushable;
    }
    // If spawned inside a wall
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "NoSpawn")
        {
            Debug.Log("hit");
            hunt.SpawnItems();
            hunt.spawnInfo.Remove(objectInfo);
            Destroy(this.gameObject);                
        }
    }
}
