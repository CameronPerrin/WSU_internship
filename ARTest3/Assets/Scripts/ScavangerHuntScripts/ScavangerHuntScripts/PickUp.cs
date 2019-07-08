using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inHandObject;
    public GameObject pickUpPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider[] coll = Physics.OverlapSphere(transform.position - new Vector3(0, .5f, 0), 1f);
            foreach(Collider c in coll)
            {
                if(coll.Length <= 1 && coll.Length != 0)
                {
                    inHandObject = coll[0].gameObject;
                    inHandObject.transform.position = pickUpPos.transform.position;
                    inHandObject.transform.parent = gameObject.transform;
                    break;
                }
                float[] dist = DistanceCheck(coll);
                
            }
        }
        float[] DistanceCheck(Collider[] colls)
        {
            List<float> arr = new List<float>();
            for (int i = 0; i < colls.Length; i++)
            {
                
            }
            return arr.ToArray();
        }
    }
}
