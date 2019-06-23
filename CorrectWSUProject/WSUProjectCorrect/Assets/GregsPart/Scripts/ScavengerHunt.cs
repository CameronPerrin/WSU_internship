using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SpawnedInformation
{
   public string name;
   public string information;
    public bool flushable;
   public Vector3 pos;
    public GameObject obj;
}
public class ScavengerHunt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plane;
    public int amountToSpawn;
    public GameObject[] spawns;
    public GameObject testSpawn;
    [HideInInspector]
    public bool readyToSpawn;
    List<SpawnedInformation> spawnInfo;
    bool isReady;
    // Update is called once per frame
    void Start()
    {
        spawnInfo = new List<SpawnedInformation>();
        SpawnItems();
    }
    void Update()
    {
        if(!isReady)
            SpawnItems();
    }
    void SpawnItems()
    {
        SpawnedInformation info = new SpawnedInformation();
        float maxX = plane.GetComponent<MeshRenderer>().bounds.extents.x;
        float maxZ = plane.GetComponent<MeshRenderer>().bounds.extents.z;
        Vector3 vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
        //Debug.Log(plane.GetComponent<MeshRenderer>().bounds);
        vec += plane.GetComponent<MeshRenderer>().bounds.center; // the random position
        if(spawnInfo.Count > 0)
        for (int i = 0; i < spawnInfo.Count; i++)
        {
            while (Vector3.Distance(vec, spawnInfo[i].pos) < Vector3.Magnitude(spawnInfo[i].obj.GetComponent<MeshRenderer>().bounds.extents))
            {
                vec = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
            }
        }
        info.pos = vec; // make a loop that checks for all the positions;
        spawnInfo.Add(info);
        //Just have to spawn them all in different positions;
        StartCoroutine(SpawnObject(vec, info));

    }
    IEnumerator SpawnObject(Vector3 pos, SpawnedInformation info)
    {
        isReady = true;
        yield return new WaitForSeconds(1);
        GameObject temp = Instantiate(testSpawn);
        info.obj = temp;
        temp.transform.position = pos;
        isReady = false;
    }
}
