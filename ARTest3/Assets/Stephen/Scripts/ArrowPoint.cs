using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public Vector3 dir;
    public GameObject target;


    void Update()
    {
        //Simple arrow rotation for searching for objects in FB
        dir = target.transform.position;
        transform.LookAt(transform.position + transform.forward, dir - transform.position);
    }
}
