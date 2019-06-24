using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;
public class FunctionCaller : MonoBehaviour
{
    // Start is called before the first frame update
    //Only usable on the detectedplanegenerator
    public GameObject func;
    public void CallFunction()
    {
       // func.GetComponent<DetectedPlaneGenerator>().DetectedPlanePrefab.GetComponent<ScavengerHunt>().StartSpawning();
    }
}
