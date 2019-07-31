using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMaster : MonoBehaviour
{
    
    public Transform dropable;
    public Transform selector;
    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(dropable, new Vector3(-7,0,0), Quaternion.identity);
        Instantiate(selector, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
