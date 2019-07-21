using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public Vector3 dir;
    public GameObject target;
    public GameObject reticle;
    public GameObject arrow;
    private GameObject cam;

    void Start()
    {
        arrow = transform.GetChild(0).gameObject;
        cam = Camera.main.gameObject;
    }
    void Update()
    {
        //Simple arrow rotation for searching for objects in FB
        dir = target.transform.position;
        transform.LookAt(transform.position + transform.forward, dir - reticle.transform.position);
        if (reticle.GetComponent<SpriteRenderer>().color == Color.red && cam.GetComponent<ScannerMove>().hit.transform.gameObject == target)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
