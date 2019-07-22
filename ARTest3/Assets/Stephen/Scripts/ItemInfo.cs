using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    MeshRenderer rend;
    public Color32 origColor;
    public string itemName;
    [TextArea (3,10)]
    public string desc;

    void Awake()
    {
        //rend = this.GetComponent<MeshRenderer>();
        //origColor = rend.material.color;
    }
}
