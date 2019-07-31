using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{   
    private EventSystem es;
    private void Start()
    {
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Phase1");
    }

    void Update()
    {
        if (es.firstSelectedGameObject.tag == "Button" && Input.GetButtonDown("Submit"))
        {
            StartGame();
        }
    }

}
