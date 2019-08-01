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
    public float lerpTime;
    Vector2 rotation;
    Vector3 currentPos;
    Vector3 movePos;
    bool hasMoved;
    bool cannotMoveForward;
    bool isLerping;
    float timer;
    float yPos;
    float mouseRotation;
    float verticalLookRotation = 0f;
    void Start()
    {
        yPos = transform.position.y;
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
        //mouse movement 
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotSpeed);
        verticalLookRotation += Input.GetAxis("Mouse Y") * rotSpeed;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        camera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        //ControllerMovment
        transform.Rotate(Vector3.up * Input.GetAxis("RightJoyStickHor") * rotSpeed);
        verticalLookRotation += Input.GetAxis("RightJoyStickVert") * rotSpeed;
        Debug.Log(Input.GetAxis("RightJoyStickVert") + "  " + Input.GetAxis("RightJoyStickHor"));
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        camera.transform.localEulerAngles = Vector3.left * verticalLookRotation;


        //apply camera rotation

        /*   if (!isLerping)
           {
               timer = 0.0f;
               //Vector3 newPos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
               rotation.y += Input.GetAxis("Mouse X");
               rotation.x += -Input.GetAxis("Mouse Y");
               isLerping = true;
           }
           timer += Time.deltaTime;
           if (isLerping)
           {
               Vector3 deltaMovement = camera.transform.eulerAngles;
               deltaMovement.z = 0;
               Vector3 inputMovement = Quaternion.Euler(rotation.x * rotSpeed, rotation.y * rotSpeed, 0).eulerAngles;
               Debug.Log(deltaMovement - inputMovement);
               camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, Quaternion.Euler(rotation.x * rotSpeed,rotation.y * rotSpeed,0), timer);
               transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation.x * rotSpeed, rotation.y * rotSpeed, 0), timer);
               camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y, 0);
               transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
               transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
               if (timer >= lerpTime)
                   isLerping = false;
           }*/
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
