using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    MeshRenderer rend;
    public Color32 origColor;
    public string itemName;
    [TextArea (3,10)]
    public string desc1;
    [TextArea (3,10)]
    public string desc2;
    [TextArea (3,10)]
    public string desc3;
    private Vector3 trueScale;

    void Awake()
    {
        //rend = this.GetComponent<MeshRenderer>();
        //origColor = rend.material.color;
        //Used to check item was proper scale for Sewer scene
        trueScale = this.transform.localScale;

    }
}
