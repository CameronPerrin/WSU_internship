using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInside : MonoBehaviour
{
    public bool isWall;
    public bool Contains(Vector3 vec)
    {
        if(GetComponent<MeshRenderer>().bounds.Contains(vec))
        {
            return true;
        }
        return false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(isWall)
        {
         //   Debug.Log("hit");
        //    ScavengerHunt sca = collision.gameObject.GetComponent<ObjectInformation>().plane.GetComponent<ScavengerHunt>();
        //    float maxX = sca.gameObject.GetComponent<MeshRenderer>().bounds.extents.x;
        //    float maxZ = sca.gameObject.GetComponent<MeshRenderer>().bounds.extents.z;
        //    Vector3 vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
       //  /  vec += (sca.gameObject.GetComponent<MeshRenderer>().bounds.center * 75f); // the random position
         //   collision.transform.position = vec;
        }
    }
}
