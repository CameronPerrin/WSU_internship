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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Instantiate(dropable, new Vector3((j*2)-7, i*2, 0), Quaternion.identity);
            }
        }
 
        Instantiate(selector, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
