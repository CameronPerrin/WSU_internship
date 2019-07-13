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
    bool hasWaited;
    void Awake()
    {
        objectInfo.name = transform.name;
        objectInfo.information = description;
        objectInfo.flushable = flushable;
        StartCoroutine(Delay());
    }
    // If spawned inside a wall
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "NoSpawn" && !hasWaited)
        {
            Debug.Log("hit");
            hunt.SpawnItems();
            hunt.spawnInfo.Remove(objectInfo);
            Destroy(this.gameObject);                
        }
    }
    IEnumerator Delay()
    {
        hasWaited = false;
        yield return new WaitForSeconds(2f);
        hasWaited = true;
    }
}
