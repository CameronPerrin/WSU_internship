using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public GameObject player;
    public float glowDistance = 1;
    public Material glowMaterial;

    private Material origMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        origMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= glowDistance)
        {
            GetComponent<Renderer>().material = glowMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = origMaterial;
        }
    }
}
