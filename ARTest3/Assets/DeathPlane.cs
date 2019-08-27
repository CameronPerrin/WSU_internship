using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPos;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = spawnPos.position;
    }
}
