using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCont : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    public float speed;
    public float rotSpeed;
    Vector2 rotation;
    void Start()
    {
        //camera.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //camera.transform.position = transform.position;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 vec = new Vector3(h * speed, 0, v * speed);
        vec = Vector3.ProjectOnPlane(transform.forward * speed * v, Vector3.up);
        Vector3 movement = Vector3.ProjectOnPlane(transform.right * speed * h, Vector3.up);
        vec.y = 0;
        transform.position += (vec + movement);
        Debug.Log(Input.mousePosition);
        rotation.y += Input.mousePosition.x;
        rotation.x += -Input.mousePosition.y;
        camera.transform.eulerAngles = (Vector2)rotation * rotSpeed;
        transform.eulerAngles = (Vector2)rotation * rotSpeed;

    }
}
