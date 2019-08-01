using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMaster : MonoBehaviour
{
    public GameObject[] solution;
    public Transform dropable;
    public Transform selector;
    public Transform board;
    public int rows, columns;
    GameObject dropables;
    
    // Start is called before the first frame update
    void Start()
    {
        //solution = new GameObject[rows * columns];
        for (int i = 0; i < rows; i++) //rows
        {
            for (int j = 0; j < columns; j++) //columns
            {

                Instantiate(dropable, new Vector3((j*2)-7, i*2, 0), Quaternion.identity);

                Instantiate(board, new Vector3((j*2.3f)+4, i *2.3f, 0), Quaternion.identity);
            }
        }

        Instantiate(selector, new Vector3(0, 0, 0), Quaternion.identity);
        //for (int k = 0; k < GameObject.FindGameObjectsWithTag("pickUp").Length; k++)
       // {
            //Debug.Log(this.name);
            //solution[] = ;
        //}
        

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Filled").Length > 1)
        {
            //first line revealed
        }
        if (GameObject.FindGameObjectsWithTag("Filled").Length > 3)
        {
            //first line revealed
        }
        if (GameObject.FindGameObjectsWithTag("Filled").Length > 5)
        {
            //first line revealed
        }
    }
}
