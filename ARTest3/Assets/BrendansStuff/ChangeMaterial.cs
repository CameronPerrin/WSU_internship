using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    private GameObject player;
    public float glowDistance = 3;
    public Material glowMaterial;

    private Material origMaterial;
    private ArtifactController ac;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origMaterial = GetComponent<Renderer>().material;
        ac = player.transform.GetChild(0).gameObject.GetComponent<ArtifactController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= glowDistance && !ac.currentlyHolding)
        {
            GetComponent<Renderer>().material = glowMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = origMaterial;
        }
    }
}
