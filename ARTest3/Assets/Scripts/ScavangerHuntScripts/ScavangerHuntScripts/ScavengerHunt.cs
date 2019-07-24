using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnedInformation
{
    public string name;
    public string information;
    public bool flushable;
    public Vector3 pos;
    public GameObject obj;
}
/* So I will make this class hella dynamic actually. You will type the name of the 
 * objects that can be spawned inside the game (tissue, markers, babywipes,etc) and the script
 * will automatically instantiate the prefab and create the gameobject with the correct information on it.
 * alternatively I could make it easy by creating the prefab but this will be faster for future DLC!
*/
public class ScavengerHunt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textObject;
    GameObject inventoryObject;
    GameObject plane;
    public int amountToSpawn;
    [Tooltip("Every Spawned Object Requires Script Type Object Information")]
    public string[] spawns;
    //public GameObject testSpawn;
    [HideInInspector]
    public bool readyToSpawn;
    public List<SpawnedInformation> spawnInfo;
    public bool hasClicked;
    // Update is called once per frame
     void Awake()
     {
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        spawnInfo = new List<SpawnedInformation>();
       // textObject = GameObject.FindGameObjectWithTag("UIBar");
        textObject.SetActive(false);
        plane = gameObject;
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnItems();
        }
        GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().score = 0;
     }
    private void Update()
    {
        if(hasClicked)
        {
            inventoryObject.GetComponent<Inventory>().ShowList();
            hasClicked = false;
        }
    }
    public void SpawnItems()
    {
        SpawnedInformation info = new SpawnedInformation();
        float maxX = plane.GetComponent<MeshRenderer>().bounds.extents.x * .86f;
        float maxZ = plane.GetComponent<MeshRenderer>().bounds.extents.z * .86f;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
        vec += plane.GetComponent<MeshRenderer>().bounds.center; // the random position
        if (spawnInfo.Count > 0)
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                if (i > 1000)
                    break;
                //if the distance of the vector to the spawn pos is less then the size of the spawned object then randomize its pos
                while (spawnInfo[i].obj != null && Vector3.Distance(vec, spawnInfo[i].pos) < Vector3.Magnitude(spawnInfo[i].obj.GetComponent<MeshRenderer>().bounds.extents * 2.5f))
                {
                    vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
                    vec += plane.GetComponent<MeshRenderer>().bounds.center;
                    if(spawnInfo[i +1].obj == null)
                    {
                        spawnInfo.RemoveAt(i + 1);
                    }
                }
            }
        vec = new Vector3(vec.x, vec.y + plane.GetComponent<MeshRenderer>().bounds.extents.y, vec.z);
        info.pos = vec; // make a loop that checks for all the positions;
                        //Just have to spawn them all in different positions;
                        //  Debug.Log(vec);
        Spawn(vec, info);

    }
    void Spawn(Vector3 pos, SpawnedInformation info)
    {
        string objectName = spawns[Random.Range(0, spawns.Length)];
        //Debug.Log(objectName);
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + objectName));

        info = temp.GetComponent<ObjectInformation>().objectInfo;
        //info.name = objectName;
        pos = new Vector3(pos.x, pos.y + temp.GetComponent<MeshRenderer>().bounds.extents.y, pos.z);
        info.obj = temp;
        CheckIfInside[] check = GameObject.FindObjectsOfType(typeof(CheckIfInside)) as CheckIfInside[];
        foreach (CheckIfInside c in check) // Check if we spawned inside an object and if so spawn ontop of it
        {          
            if (c.Contains(pos) && !c.isWall)
            {
                pos = new Vector3(pos.x, pos.y + (c.gameObject.GetComponent<MeshRenderer>().bounds.extents.y * 2), pos.z);
            }

        }
        //Check if we spawned inside a wall, and if so we will just restart the process
        temp.transform.position = pos;
        temp.GetComponent<ObjectInformation>().plane = this.gameObject;
        temp.GetComponent<ObjectInformation>().hunt = this;
        temp.GetComponent<ObjectInformation>().name = objectName;
        temp.AddComponent<InfoPopUp>();
        temp.GetComponent<InfoPopUp>().SetSpawnInfo(info);
        temp.GetComponent<InfoPopUp>().text = Instantiate(textObject);
        temp.GetComponent<InfoPopUp>().text.transform.parent = textObject.transform.parent;
        //InventoryInfo inventoryInfo = new InventoryInfo(info, temp.GetComponent<ObjectInformation>());

        //inventoryObject.GetComponent<Inventory>().AddToList(inventoryInfo);
        spawnInfo.Add(info);
       // isReady = false;
    }
}
