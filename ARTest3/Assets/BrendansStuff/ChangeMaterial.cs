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
    private GameObject[] glowEffects;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origMaterial = GetComponent<Renderer>().material;
        ac = player.transform.GetChild(0).gameObject.GetComponent<ArtifactController>();
        glowEffects = GameObject.FindGameObjectsWithTag("Glow");

        foreach (GameObject obj in glowEffects)
        {
            if (obj.GetComponent<ParticleSystem>().isPlaying)
                obj.GetComponent<ParticleSystem>().Stop();
        }
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

        if (ac.currentlyHolding)
        {
            foreach (GameObject obj in glowEffects)
            {
                if (!obj.GetComponent<ParticleSystem>().isPlaying)
                {
                    obj.GetComponent<ParticleSystem>().Play();
                }
                    
            }
        }
        else
        {
            foreach (GameObject obj in glowEffects)
            {
                if (obj.GetComponent<ParticleSystem>().isPlaying)
                {
                    obj.GetComponent<ParticleSystem>().Stop();
                    obj.GetComponent<ParticleSystem>().Clear();
                }
                    
            }
        }
    }
}
