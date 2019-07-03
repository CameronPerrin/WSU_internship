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
    GameObject textObject;
    GameObject plane;
    public int amountToSpawn;
    [Tooltip("Every Spawned Object Requires Script Type Object Information")]
    public string[] spawns;
    //public GameObject testSpawn;
    [HideInInspector]
    public bool readyToSpawn;
    List<SpawnedInformation> spawnInfo;
    bool isReady;
    // Update is called once per frame
     void Start()
    {
        spawnInfo = new List<SpawnedInformation>();
        textObject = GameObject.FindGameObjectWithTag("UIBar");
        textObject.SetActive(false);
        plane = gameObject;
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnItems();
        }
    }
    void SpawnItems()
    {
        SpawnedInformation info = new SpawnedInformation();
        float maxX = plane.GetComponent<MeshRenderer>().bounds.extents.x;
        float maxZ = plane.GetComponent<MeshRenderer>().bounds.extents.z;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
        //Debug.Log(plane.GetComponent<MeshRenderer>().bounds);
        vec += plane.GetComponent<MeshRenderer>().bounds.center; // the random position
        if (spawnInfo.Count > 0)
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                if (i > 1000)
                    break;
                while (Vector3.Distance(vec, spawnInfo[i].pos) < Vector3.Magnitude(spawnInfo[i].obj.GetComponent<MeshRenderer>().bounds.extents))
                {
                    vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
                    vec += plane.GetComponent<MeshRenderer>().bounds.center;
                }
            }
        info.pos = vec; // make a loop that checks for all the positions;
                        //Just have to spawn them all in different positions;
                        //  Debug.Log(vec);
        Spawn(vec, info);

    }
    IEnumerator SpawnObject(Vector3 pos, SpawnedInformation info)
    {
        isReady = true;
        yield return new WaitForSeconds(1);
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + spawns[Random.Range(0,spawns.Length)])); // will not have to create it
        info = temp.GetComponent<ObjectInformation>().objectInfo;
       // info.name = "Something";
        info.obj = temp; 
        temp.transform.position = pos;
        temp.AddComponent<InfoPopUp>();
        temp.GetComponent<InfoPopUp>().SetSpawnInfo(info);
        temp.GetComponent<InfoPopUp>().text = Instantiate(textObject);
        temp.GetComponent<InfoPopUp>().text.transform.parent = textObject.transform.parent;
        spawnInfo.Add(info);
        isReady = false;
    }
    void Spawn(Vector3 pos, SpawnedInformation info)
    {
        string objectName = spawns[Random.Range(0, spawns.Length)];
        GameObject temp = Instantiate(Resources.Load<GameObject>("ObjectsToFind/" + objectName));
        info = temp.GetComponent<ObjectInformation>().objectInfo;
        //info.name = objectName;
        info.obj = temp;
        temp.transform.position = pos;
        temp.AddComponent<InfoPopUp>();
        temp.GetComponent<InfoPopUp>().SetSpawnInfo(info);
        temp.GetComponent<InfoPopUp>().text = Instantiate(textObject);
        temp.GetComponent<InfoPopUp>().text.transform.parent = textObject.transform.parent;

        spawnInfo.Add(info);
        isReady = false;
    }
}
