using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour, IPointerClickHandler
{
    SpawnedInformation spawn;
    public GameObject text;
    public bool isDisplaying = false;
    static bool isDelay;
    // Update is called once per frame
    public void SetSpawnInfo(SpawnedInformation spawnedInfo)
    {
        spawn = spawnedInfo;
    }
    private void Update()
    {
    
    }
    private void OnMouseDown()
    {
        if (!isDisplaying)
        {
            text.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            text.SetActive(true);
            text.GetComponent<Text>().text = spawn.name + " Object";
           // text.GetComponent<TextMesh>().text = spawn.name + " Object";
          //  text.transform.position = transform.position + new Vector3(0, .5f, 0);
        }
        isDisplaying = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("touched");
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        isDelay = false;
    }
}
