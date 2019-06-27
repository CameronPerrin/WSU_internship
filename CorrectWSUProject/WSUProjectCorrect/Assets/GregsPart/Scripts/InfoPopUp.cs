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
        spawn.name = "tissue";
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.touchCount >= 1 && !isDelay)
            {
                isDelay = true;
                StartCoroutine(Delay());
                Debug.Log("Clicked");
            }
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == transform.name)
                {
                    //  text.GetComponent<TextMesh>().text = spawn.name + " Object";
                    //  text.transform.position = transform.position + new Vector3(0, .5f, 0);
                    text.SetActive(true);
                    text.GetComponentInChildren<Text>().text = spawn.name + " Object";
                }
            }
        }     
    }
    private void OnMouseDown()
    {
        if (!isDisplaying)
        {
            text.SetActive(true);
            text.GetComponentInChildren<Text>().text = spawn.name + " Object";
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
