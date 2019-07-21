using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    public float speed;
    public float rotSpeed;
    public bool hasCollided;
    Vector2 rotation;
    Vector3 currentPos;
    Vector3 movePos;
    bool hasMoved;
    bool cannotMoveForward;
    void Start()
    {
        //camera.transform.position = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        hasMoved = true;
        currentPos = transform.position;
        camera.transform.position = transform.position;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 vec = new Vector3(h * speed, 0, v * speed);
        vec = Vector3.ProjectOnPlane(transform.forward * speed * v, Vector3.up);
        Vector3 movement = Vector3.ProjectOnPlane(transform.right * speed * h, Vector3.up);
        if (cannotMoveForward)
        {
            vec.z = 0;
            Debug.Log(vec);
        }
        vec.y = 0;
        movePos = (vec + movement + transform.position); // So i know where we want to move if we are colliding
        transform.position += (vec + movement);
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        camera.transform.eulerAngles = (Vector2)rotation * rotSpeed;
        transform.eulerAngles = (Vector2)rotation * rotSpeed;
        cannotMoveForward = false;
        //Debug.DrawRay(transform.position, transform.forward);
        //Debug.Log(Vector3.Distance(currentPos, transform.position));
        if (Vector3.Distance(currentPos, transform.position) < .01f)
        {
            hasMoved = false;
        }
        if (!hasMoved && hasCollided)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.name == "Walls")
                {
                    //Debug.Log(Vector3.Distance(hit.point, movePos));
                    if (Vector3.Distance(hit.point, movePos) < .8f)
                    {
                        cannotMoveForward = true;
                        Debug.Log("Cant move forward");
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.tag != "Plane" && collision.transform.name == "Walls")
        {
            hasCollided = true;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag != "Plane" && collision.transform.name == "Walls")
        {
            hasCollided = false;
        }
    }

}
