using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    public SpawnedInformation objectInfo;
    public string name;
    public string description;
    public bool flushable; 
    void Awake()
    {
        objectInfo.name = name;
        objectInfo.information = description;
        objectInfo.flushable = flushable;
    }
}
